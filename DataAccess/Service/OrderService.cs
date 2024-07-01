using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly AccountService _accountService;
        private readonly ImportReceiptService _importReceiptService;
        private readonly OrderDetailService _orderDetailService;
        private readonly ProductService _productService;
        private DateTime currentDate = DateTime.Now;


        public OrderService(IOrderRepository repo, AccountService accountService, ImportReceiptService importReceiptService, OrderDetailService orderDetailService, ProductService productService)
        {
            _repository = repo;
            _accountService = accountService;
            _importReceiptService = importReceiptService;
            _orderDetailService = orderDetailService;
            _productService = productService;
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
            var orders = _repository.GetOrderList().ToList();
            var IncomeList = orders.Where(o => o.Status == 4 && o.EndDate != null &&
               o.EndDate.Value.Year == currentYear && o.EndDate.Value.Month == currentMonth).Select(o => o.TotalPrice).ToList();
            return IncomeList.Sum();
        }

        public double GetRevenue()
        {
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;
            var income = _repository.GetOrderList().ToList();
            var IncomeList = income.Where(o => o.Status == 4 && o.EndDate != null &&
               o.EndDate.Value.Year == currentYear && o.EndDate.Value.Month == currentMonth).Select(o => o.TotalPrice).ToList();

            var spent = _importReceiptService.GetImportProductsList().ToList();
            var SpentList = spent.Where(IR => IR.DateImport.Year == currentYear && IR.DateImport != null
                                                   && IR.DateImport.Month == currentMonth).Select(IR => IR.Payment).ToList();

            double Revenue = IncomeList.Sum() - SpentList.Sum();

            return Revenue;
        }

        public List<Tuple<string, double>> GetTop10Customer()
        {
            return _repository.GetTop10Customer().ToList();
        }
        public List<OrderDataModel> GetOrdersByCustomer(string username)
        {
            return _repository.GetOrderListByUser(username);
        }
    }
}
