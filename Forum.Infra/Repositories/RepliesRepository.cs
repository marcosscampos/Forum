using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class RepliesRepository : IRepliesRepository
    {
        public readonly ApplicationDbContext _context;
        public readonly DbSet<Reply> _replies;

        public RepliesRepository(ApplicationDbContext context)
        {
            _context = context;
            _replies = context.Replies;
        }

        public async Task<Reply> AddAsync(Reply entity)
        {
            await _replies.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Contains(long id)
        {
            var reply = await GetByIdAsync(id);

            return (reply != null);
        }

        public async Task<IEnumerable<Reply>> GetAllAsync()
        {
            return await Task.FromResult(_replies.Include(r => r.Topic)
                                                 .Include(r => r.User));
        }

        public Task<IEnumerable<Reply>> GetByAsync(string keyword) => throw new NotImplementedException();
        public async Task<Reply> GetByIdAsync(long id)
        {
            return await _replies.Include(r => r.Topic)
                                    .Include(r => r.User)
                                    .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task RemoveAsync(Reply entity)
        {
            _replies.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<Reply> UpdateAsync(Reply entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
