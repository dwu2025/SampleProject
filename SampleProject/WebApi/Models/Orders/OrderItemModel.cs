using BusinessEntities;
using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderItemModel
    {
        public string productId { get; set; }
        public int quantity { get; set; }
    }
}