using BusinessObject.Model.Page;
using DataAccess.IRepository;
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
            foreach (var order in orderList)
            {
                AccountModel account = _accountService.getAccount(order.Username);
                order.Fullname = account.Fullname;
                order.Phone = account.Phone;
                order.Email = account.Email;
            }
            return orderList;
        }
        public OrderModel GetOrderByID(string ID)
        {
            OrderModel order = _repository.GetOrderByID(ID);
            AccountModel account = _accountService.getAccount(order.Username);
            order.Fullname = account.Fullname;
            order.Phone = account.Phone;
            order.Email = account.Email;

            return order;
        }
        public bool ChangeOrderStatus(OrderModel _order, int Status)
        {
            return _repository.ChangeOrderStatus(_order, Status);
        }
    }
}
