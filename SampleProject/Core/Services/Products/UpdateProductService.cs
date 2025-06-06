using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, decimal price)
        {
            product.SetName(name);
            product.SetPrice(price);
        }
    }
}