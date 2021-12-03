using Microsoft.EntityFrameworkCore;
using User.Domain.Contracts.Repositories;
using User.Infrastructure.Context;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<Domain.Entities.User>, IUserRepository
    {
        public UserRepository(UserContext db) : base(db)
        {
        }

        public async Task<Domain.Entities.User> GetByUserNameOrEmail(string input)
        {
            var user = _db.Users.Where(u => u.UserName == input || u.Email == input).AsNoTracking();
            return await user.FirstOrDefaultAsync();
        }
    }
}
