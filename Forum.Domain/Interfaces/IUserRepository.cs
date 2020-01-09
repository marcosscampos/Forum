using Forum.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Domain.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetCurrentUser(ClaimsPrincipal user);
    }
}
