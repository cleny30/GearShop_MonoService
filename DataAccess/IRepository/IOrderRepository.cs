using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderRepository
    {
        public List<OrderModel> GetOrderList();
        public OrderModel GetOrderByID(string ID);
        public bool ChangeOrderStatus(OrderModel _order, int Status);
        public int GetCompletedOrder();
        public List<Tuple<string, double>> GetTop10Customer();
        public List<OrderDataModel> GetOrderListByUser(string username);
        public string GetNewOrderID();
        public bool AddOrderDetail(OrderDetailModel orderDetailModel);
        public bool AddNewOrder(OrderModel orderModel);


    }
}
