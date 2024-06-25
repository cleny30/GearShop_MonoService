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
        public OrderService()
        {
            _repository = new OrderRepository();
        }

        public List<OrderModel> GetOrderList()
        {
            return _repository.GetOrderList();
        }
        public OrderModel GetOrderByID(string ID)
        {
            return _repository.GetOrderByID(ID);
        }
        public bool ChangeOrderStatus(OrderModel _order, int Status)
        {
            return _repository.ChangeOrderStatus(_order, Status);
        }
        public List<OrderDataModel> GetOrdersByCustomer(string username) { 
        return _repository.GetOrderListByUser(username);
        }
    }
}
