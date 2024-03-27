using JWTBearer.Domain.Entities;


namespace JWTBearer.Application.Interface
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetByUserNameAsync(string username);
    }
}
