﻿using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _repo;

        public OrderDetailService(IOrderDetailRepository repo)
        {
            _repo = repo;
        }
        public List<OrderDetailModel> GetOrderDetailList(OrderModel order)
        {
            return _repo.GetOrderDetailList(order); 
        }
    }
}
