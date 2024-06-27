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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public List<OrderDetailModel> GetOrderDetailList(OrderModel order)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                var dbContext = new PrndatabaseContext();
                orderDetails = dbContext.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();
                List<OrderDetailModel> _orderDetails = new List<OrderDetailModel>();
                foreach (var orderDetail in orderDetails)
                {
                    OrderDetailModel _orderDetail = new OrderDetailModel();
                    _orderDetail.CopyProperties(orderDetail);
                    _orderDetails.Add(_orderDetail);
                }

                return _orderDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
