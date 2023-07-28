using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.User
{
    public interface IUserRepository
    {
        public List<UserModel> GetAllUsers();
        public UserModel GetUserById(int id);
        public bool DeleteUser(int id);
        public bool EditUser(UserModel User);
        public bool CreateUser(UserModel User);
        public UserModel GetUserByEmail(string email);
    }
}
