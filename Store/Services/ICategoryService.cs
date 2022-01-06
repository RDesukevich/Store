using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> Get();
        public Task<Category> GetAsync(Guid categoryId);
        public Task<Guid> EditAsync(Category category);
        public Task DeleteAsync(Guid categoryId);
        public Task<Guid> CreateAsync(Category category);
    }

    class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        
        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> GetAsync (Guid categoryId)
        {
            return await _db.Category.FirstOrDefaultAsync(p => p.Id == categoryId);
        }

        public async Task<Guid> EditAsync(Category category)
        {
            _db.Category.Update(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }

        public async Task DeleteAsync(Guid categoryId)
        {
            _db.Category.Remove(await _db.Category.FirstOrDefaultAsync(p => p.Id == categoryId));
            await _db.SaveChangesAsync();
        }

        public async Task<Guid> CreateAsync(Category category)
        {
            _db.Category.Add(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }
    }
}
