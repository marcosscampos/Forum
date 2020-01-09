using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class TopicsRepository : ITopicsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Topic> _topics;

        public TopicsRepository(ApplicationDbContext context)
        {
            _context = context;
            _topics = context.Topics;
        }

        public async Task<Topic> AddAsync(Topic entity)
        {
            await _topics.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Contains(long id)
        {
            var topic = await GetByIdAsync(id);

            return (topic != null);
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await Task.FromResult(_topics);
        }

        public async Task<Topic> GetByIdAsync(long id)
        {
            return await _topics.Include(t => t.Replies)
                                .ThenInclude(r => (r as Reply).User)
                                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<IEnumerable<Topic>> GetByAsync(string keyword) => throw new NotImplementedException();

        public async Task RemoveAsync(Topic entity)
        {
            _topics.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<Topic> UpdateAsync(Topic entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
