using Studentio.Contracts.IUser;
using Studentio.Entities.Context;
using Studentio.Entities.Models;
using Studentio.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Studentio.Repository.Users
{
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository (RepositoryContext context) : base(context) { }

        public List<User> GetAllUsers()
        {
            //return FindAll().Where(p => p.StatusId == 1);
            return FindAll().ToList();
        }

        public User GetUserByEmailAndPassword(string Email, string Password)
        {
            return FindByCondition(u => u.Email.Equals(Email) && u.Password.Equals(Password))
                    .DefaultIfEmpty(new User())
                    .FirstOrDefault();

         }
    }
}
