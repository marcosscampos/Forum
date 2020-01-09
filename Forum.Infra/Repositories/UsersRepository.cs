using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Infra.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task<User> AddAsync(User entity)
        {
            await _users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Contains(long id)
        {
            var user = await GetByIdAsync(id);

            return (user != null);
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await Task.FromResult(_users);

        public async Task<IEnumerable<User>> GetByAsync(string keyword) => throw new NotImplementedException();

        public async Task<User> GetByIdAsync(long id) => await _users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> GetCurrentUser(ClaimsPrincipal user)
        {
            var currentUser = await _users.FirstOrDefaultAsync(u => u.UserName == user.Identity.Name);

            if (currentUser != null)
                return currentUser;

            var newUser = new User
            {
                UserName = user.Identity.Name,
                Email = user.Claims.ToList().FirstOrDefault(c => c.Type == "email").Value
            };

            await AddAsync(newUser);

            return await GetCurrentUser(user);
        }

        public async Task RemoveAsync(User entity) => throw new NotImplementedException();

        public async Task<User> UpdateAsync(User entity) => throw new NotImplementedException();
    }
}
