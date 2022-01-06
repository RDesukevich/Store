using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Services

{
    public interface IApplicationTypeService
    {
        public Task<IEnumerable<ApplicationType>> Get();
        public Task<ApplicationType> GetAsync(Guid applicationTypeId);
        public Task<Guid> EditAsync(ApplicationType applicationType);
        public Task DeleteAsync(Guid applicationTypeId);
        public Task<Guid> CreateAsync(ApplicationType applicationType);
    }

    class ApplicationTypeService : IApplicationTypeService
    {
        private readonly AppDbContext _db;

        public ApplicationTypeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ApplicationType>> Get()
        {
            return await _db.ApplicationType.ToListAsync();
        }

        public async Task<ApplicationType> GetAsync(Guid applicationTypeId)
        {
            return await _db.ApplicationType.FirstOrDefaultAsync(a => a.Id == applicationTypeId);
        }

        public async Task<Guid> EditAsync(ApplicationType applicationType)
        {
            _db.ApplicationType.Update(applicationType);
            await _db.SaveChangesAsync();
            return applicationType.Id;
        }

        public async Task DeleteAsync(Guid applicationTypeId)
        {
            _db.ApplicationType.Remove(await _db.ApplicationType.FirstOrDefaultAsync(a=> a.Id == applicationTypeId));
            await _db.SaveChangesAsync();
        }

        public async Task<Guid> CreateAsync(ApplicationType applicationType)
        {
            _db.ApplicationType.Add(applicationType);
            await _db.SaveChangesAsync();
            return applicationType.Id;
        }
    }
}
