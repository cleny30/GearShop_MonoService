using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetailModel> GetOrderDetailList(OrderModel order);

        public List<OrderDetailModel> GetAllOrderDetailList();
      
        public List<OrderDetailModel> GetOrdersDetailByCustomer(string orderId);
    }
}
