using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }
}