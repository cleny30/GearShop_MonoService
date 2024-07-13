using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static NuGet.Packaging.PackagingConstants;

namespace DataAccess.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly AccountService _accountService;
        private readonly ImportReceiptService _importReceiptService;
        private readonly OrderDetailService _orderDetailService;
        private readonly ProductService _productService;
        private readonly EmailService _emailService;
        private readonly CartService _cartService;
        private DateTime currentDate = DateTime.Now;

        public OrderService(IOrderRepository repository, AccountService accountService, ImportReceiptService importReceiptService, OrderDetailService orderDetailService, ProductService productService, EmailService emailService, CartService cartService)
        {
            _repository = repository;
            _accountService = accountService;
            _importReceiptService = importReceiptService;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _emailService = emailService;
            _cartService = cartService;
        }

        public List<OrderModel> GetOrderList()
        {
            List<OrderModel> orderList = _repository.GetOrderList();
            return orderList;
        }
        public OrderModel GetOrderByID(string ID)
        {
            OrderModel order = _repository.GetOrderByID(ID);
            order.Email = _accountService.getAccount(order.Username).Email;

            return order;
        }
        public async Task<bool> ChangeOrderStatus(OrderModel order, int status)
        {
            bool isStatusChanged = _repository.ChangeOrderStatus(order, status);

            if (status == 0 && isStatusChanged)
            {
                var orderProducts = _orderDetailService.GetOrderDetailsById(order.OrderId);
                await _productService.AddQuantityFromProductAsync(orderProducts);
            }

            return isStatusChanged;
        }

        public int GetCompletedOrder()
        {
            return _repository.GetCompletedOrder();
        }

        public double GetIncome()
        {
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;
            var orders = _repository.GetOrderList()?.ToList() ?? new List<OrderModel>();

            var incomeList = orders
                .Where(o => o.Status == 4 && o.EndDate != null &&
                            o.EndDate.Value.Year == currentYear && o.EndDate.Value.Month == currentMonth)
                .Select(o => o.TotalPrice)
                .ToList();

            return incomeList.Any() ? incomeList.Sum() : 0;

        }

        public double GetRevenue()
        {
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;

            // Get and filter orders
            var orders = _repository.GetOrderList()?.ToList() ?? new List<OrderModel>();
            var incomeList = orders
                .Where(o => o.Status == 4 && o.EndDate != null &&
                            o.EndDate.Value.Year == currentYear && o.EndDate.Value.Month == currentMonth)
                .Select(o => o.TotalPrice)
                .ToList();

            // Get and filter import receipts
            var receipts = _importReceiptService.GetImportProductsList()?.ToList() ?? new List<ImportProductModel>();
            var spentList = receipts
                .Where(ir => ir.DateImport != null && ir.DateImport.Year == currentYear && ir.DateImport.Month == currentMonth)
                .Select(ir => ir.Payment)
                .ToList();

       
            double revenue = incomeList.Sum() - spentList.Sum();

            return revenue;

        }

        public List<Tuple<string, double>> GetTop10Customer()
        {
            return _repository.GetTop10Customer().ToList();
        }
        public List<OrderDataModel> GetOrdersByCustomer(string username)
        {
            var orders = _repository.GetOrderListByUser(username);

 
            orders = orders.OrderByDescending(o => o.StartDate).ToList();

            return orders; ;
        }

        public bool Checkout(OrderModel orderModel)
        {
            var proId = orderModel.proId.Split(',');

            var userCartData = _cartService.GetCartsByUserName(orderModel.Username).Where(c => proId.Contains(c.Product.ProId));

            if (userCartData != null && userCartData.Any())
            {
                foreach (var item in userCartData)
                {
                    if (item.Product.ProQuan <= 0 || item.Product.ProQuan < item.model.Quantity)
                    {
                        return false;
                    }
                }

                string orderId = _repository.GetNewOrderID();
                string resxFilePath = "DataAccess.Resource.Template";

                ResourceManager resourceManager = new ResourceManager(resxFilePath, Assembly.GetExecutingAssembly());
                string tr_tag = resourceManager.GetString("tr_tag");
                int index = 1;
                string table_content = "";

                List<OrderDetailModel> orderDetail = new List<OrderDetailModel>();

                foreach (var cartItem in userCartData)
                {
                    var product = _productService.GetProduct(cartItem.Product.ProId);
                    var updateQuantity = product.ProQuan - cartItem.model.Quantity;
                    product.ProQuan = updateQuantity;
                    _productService.UpdateQuantityProduct(product);

                    string tmp = tr_tag;

                    string formattedIndex = index.ToString("D2");

                    tmp = tmp.Replace("@param01", product.ProName);
                    tmp = tmp.Replace("@param02", cartItem.model.Quantity.ToString());
                    tmp = tmp.Replace("@param03", cartItem.model.Price.ToString());

                    table_content += tmp;
                    index++;

                    OrderDetailModel orderDetailModel = new OrderDetailModel
                    {
                        OrderId = orderId,
                        ProId = cartItem.Product.ProId,
                        ProName = cartItem.Product.ProName,
                        Quantity = cartItem.model.Quantity,
                        Price = cartItem.model.Price  
                    };
                    orderDetail.Add(orderDetailModel);
                }

                orderModel.OrderId = orderId;
                orderModel.EndDate = null; // time will be set when order come to customer
                _repository.AddNewOrder(orderModel);

                foreach(var item in orderDetail)
                {
                    _repository.AddOrderDetail(item);
                }
                _cartService.DeleteCartById(orderModel.proId,orderModel.Username);

                _emailService.Invoice(orderModel, table_content);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
