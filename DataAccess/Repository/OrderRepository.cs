using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
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
                        ManagerId = order.ManagerId,
                        Address = order.Address,
                        EndDate = order.EndDate,
                        OrderDes = order.OrderDes,
                        OrderId = order.OrderId,
                        StartDate = order.StartDate,
                        Status = order.Status,
                        TotalPrice = order.TotalPrice,
                        Username = order.Username
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
        public bool ChangeOrderStatus(OrderModel _order, int Status)
        {
            Order order;
            try
            {
                var dbContext = new PrndatabaseContext();
                if (_order != null)
                {
                    order = new Order
                    {
                        OrderId = _order.OrderId,
                        Address = _order.Address,
                        EndDate= _order.EndDate,
                        StartDate= _order.StartDate,
                        ManagerId = _order.ManagerId,
                        OrderDes= _order.OrderDes,
                        Username = _order.Username,
                        Fullname = _order.Fullname,
                        Phone = _order.Phone,
                        TotalPrice= _order.TotalPrice,
                        Status = Status,
                    };

                    if(Status == 4)
                    {
                        order.EndDate = DateOnly.FromDateTime(DateTime.Now);
                    }
                    dbContext.Entry<Order>(order).State = EntityState.Modified;
                    int check = dbContext.SaveChanges();

                    if(check > 0)
                    {
                        return true;
                        /*
                         * Add SignalR Here
                         */
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int GetCompletedOrder()
        {
            var OrderList = GetOrderList();
            var CompletedOrder = OrderList.Where(o => o.Status == 4).ToList();
            return CompletedOrder.Count;
        }

        public List<Tuple<string, double>> GetTop10Customer()
        {
            using (var context = new PrndatabaseContext())
            {
                var topCustomers = context.Orders
                    .Where(order => order.Status == 4)
                    .Join(context.Customers,
                          order => order.Username,
                          customer => customer.Username,
                          (order, customer) => new { customer.Fullname, order.TotalPrice })
                    .GroupBy(x => x.Fullname)
                    .Select(g => new { Fullname = g.Key, TotalPriceSum = g.Sum(x => x.TotalPrice) })
                    .OrderByDescending(x => x.TotalPriceSum)
                    .Take(10)
                    .ToList()
                    .Select(x => Tuple.Create(x.Fullname, (double)x.TotalPriceSum))
                    .ToList();

                return topCustomers;
            }
        }
    }
}
