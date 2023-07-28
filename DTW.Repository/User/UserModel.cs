using Mercadona.Repository.User;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mercadona.Repository.User
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(int idUser, string firstName, string lastName, string email, string password, string salt)
        {
            IdUser = idUser;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Salt = salt;
        }

        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
    }


}
