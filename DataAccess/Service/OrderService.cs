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

        public OrderService(IOrderRepository repo, AccountService accountService)
        {
            _repository = repo;
            _accountService = accountService;
        }
        public List<OrderModel> GetOrderList()
        {
            List<OrderModel> orderList = _repository.GetOrderList();
            return orderList;
        }
        public OrderModel GetOrderByID(string ID)
        {
            OrderModel order = _repository.GetOrderByID(ID);
            

            return order;
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
