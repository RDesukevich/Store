using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public Task<Product> GetEdit(Product product);
        public Task<Product> GetDelete(Guid productId);
    }

    class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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
            await _db.SaveChangesAsync();
        }

        public async Task<Guid> CreateAsync(Product product)
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Product> GetEdit(Product product)
        {
            return await _db.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
        }

        public async Task<Product> GetDelete(Guid productId)
        {
            return await _db.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}
