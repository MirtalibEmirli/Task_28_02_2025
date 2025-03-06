using Domain.Entities;

namespace Repository.Repositories;

public interface IUserRepository
{
    Task RegisterUser(User user);
    Task UpdateUser(User user);
    Task Remove(int id);
    Task Login(User user);
    Task<User> GetById(int id);
    Task<User> GetByEmailAsync(string name);
    Task<IEnumerable<User>> GetAll();                    

}
