using Studentio.Contracts.IRepositoryBase;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Contracts.IUser
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByEmailAndPassword(string Email, string password);

        List<User> GetAllUsers();
    }
}
