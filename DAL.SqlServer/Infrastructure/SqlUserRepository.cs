using Domain.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SqlServer.Infrastructure
{
    public class SqlUserRepository : IUserRepository
    {
        public Task<IQueryable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Login(User user)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
