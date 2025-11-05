using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using InventoryHubAPI.Models;

namespace InventoryHubAPI.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<Guid, Product> _store = new();

        public IEnumerable<Product> GetAll() => _store.Values.OrderBy(p => p.Name);

        public Product? Get(Guid id) => _store.TryGetValue(id, out var p) ? p : null;

        public void Create(Product product)
        {
            _store[product.Id] = product;
        }

        public void Update(Product product)
        {
            _store[product.Id] = product;
        }

        public bool Delete(Guid id) => _store.TryRemove(id, out _);
    }
}
