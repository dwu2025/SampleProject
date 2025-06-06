using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class OrderRepository : MemRepository<Order>, IOrderRepository
    {

        public OrderRepository() : base(new List<Order>())
        {
        }

        public IEnumerable<Order> Get()
        {
            var query = (from p in _mem
                         select p);
            return query.ToList();
        }

        public void DeleteAll()
        {
        }
    }
}