using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class ProductRepository : MemRepository<Product>, IProductRepository
    {

        public ProductRepository() : base(new List<Product>())
        {
        }

        public IEnumerable<Product> Get()
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