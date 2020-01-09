using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _categories;
        private readonly DbSet<Section> _sections;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
            _categories = context.Categories;
            _sections = context.Sections;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            await _categories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Contains(long id)
        {
            var category = await GetByIdAsync(id);

            return (category != null);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await Task.FromResult(_categories.Include(c => c.Section)
                                                    .Include(c => c.Topics));
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            return await _categories.Include(c => c.Topics)
                                    .Include(c => c.Section)
                                    .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<IEnumerable<Category>> GetByAsync(string keyword) => throw new NotImplementedException();

        public async Task RemoveAsync(Category entity)
        {
            _categories.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
