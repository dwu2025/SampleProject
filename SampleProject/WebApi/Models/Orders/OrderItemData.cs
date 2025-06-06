using BusinessEntities;
using System.Collections.Generic;
using WebApi.Models.Products;

namespace WebApi.Models.Orders
{
    public class OrderItemData 
    {
        public OrderItemData(OrderItem orderItem) 
        {
            Quantity = orderItem.Quantity;
            Product = new ProductData(orderItem.Product);
        }

        public int Quantity { get; set; }
        public ProductData Product { get; set; }
    }
}