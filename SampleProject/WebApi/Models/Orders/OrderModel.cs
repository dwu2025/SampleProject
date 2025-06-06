using BusinessEntities;
using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string CustomerId { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}