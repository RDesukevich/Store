using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> Get();
        public Task<Product> GetAsync(Guid productId);
        public Task<Guid> EditAsync(Product product);
        public Task DeleteAsync(Guid productId);
        public Task<Guid> CreateAsync(Product product);
    }

    class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await _db.Product.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetAsync(Guid productId)
        {
            return await _db.Product.AsNoTracking().Include(p => p.Category)
                .FirstOrDefaultAsync(o => o.Id == productId);
        }

        public async Task<Guid> EditAsync(Product product)
        {
            _db.Product.Update(product);
            await _db.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteAsync(Guid productId)
        {
            _db.Product.Remove(await _db.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId));
        }

        public async Task<Guid> CreateAsync(Product product)
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
            return product.Id;
        }
    }
}
