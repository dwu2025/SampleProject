using BusinessEntities;
using System;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }
            OrderDate = order.OrderDate;
            CustomerId = order.CustomerId.ToString();
            OrderItems = Array.ConvertAll(order.OrderItems.ToArray(), item => new OrderItemData(item));
        }

        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderItemData[] OrderItems { get; set; } = Array.Empty<OrderItemData>();
    }
}