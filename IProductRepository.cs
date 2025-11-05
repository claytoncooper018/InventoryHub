using System;
using System.Collections.Generic;
using InventoryHubAPI.Models;

namespace InventoryHubAPI.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? Get(Guid id);
        void Create(Product product);
        void Update(Product product);
        bool Delete(Guid id);
    }
}
