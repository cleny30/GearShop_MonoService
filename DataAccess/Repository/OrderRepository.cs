using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {

        public List<OrderModel> GetOrderList()
        {
            List<Order> orders;
            try
            {
                var dbContext = new PrndatabaseContext();
                orders = dbContext.Orders.ToList();
                List<OrderModel> _orders = new List<OrderModel>();
                foreach (var order in orders)
                {
                    OrderModel _order = new OrderModel();
                    _order.CopyProperties(order);
                    _orders.Add(_order);
                }
                return _orders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public OrderModel GetOrderByID(string ID)
        {
            Order order;
            try
            {
                var dbContext = new PrndatabaseContext();
                order = dbContext.Orders.Where(o => o.OrderId == ID).SingleOrDefault();
                if(order != null)
                {
                    OrderModel _order = new OrderModel
                    {

                    };

                    return _order;
                } else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
