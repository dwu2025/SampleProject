using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid customerId, List<OrderItem> orderItems)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("Customer ID cannot be empty", nameof(customerId));
            }
            if (orderItems == null)
            {
                throw new ArgumentNullException(nameof(orderItems), "Order items cannot be null");
            }
            order.CustomerId = customerId;
            order.OrderItems = orderItems;
        }
    }
}