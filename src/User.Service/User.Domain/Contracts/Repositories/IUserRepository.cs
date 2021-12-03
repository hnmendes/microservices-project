

namespace User.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepositoryBase<Entities.User>
    {
        Task<Entities.User> GetByUserNameOrEmail(string input);
    }
}
