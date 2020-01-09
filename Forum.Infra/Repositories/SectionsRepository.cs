using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class SectionsRepository : ISectionsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Section> _sections;

        public SectionsRepository(ApplicationDbContext context)
        {
            _context = context;
            _sections = context.Sections;
        }

        public async Task<Section> AddAsync(Section entity)
        {
            await _sections.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Contains(long id)
        {
            var section = await GetByIdAsync(id);

            return (section != null);
        }

        public async Task<IEnumerable<Section>> GetAllAsync() => await Task.FromResult(_sections.Include(s => s.Categories));

        public async Task<Section> GetByIdAsync(long id) => await _sections.Include(s => s.Categories).FirstOrDefaultAsync(s => s.Id == id);

        public Task<IEnumerable<Section>> GetByAsync(string keyword) => throw new NotImplementedException();

        public async Task RemoveAsync(Section entity)
        {
            _sections.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<Section> UpdateAsync(Section entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
