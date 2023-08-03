using Mercadona.Repository.User;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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
        [Required(ErrorMessage = "Le prénom de l'utilisateur est obligatoire.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Le nom de l'utilisateur doit faire au moins 3 caractères.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Le prénom de l'utilisateur est obligatoire.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Le prénom de l'utilisateur doit faire au moins 3 caractères.")]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Le password de l'utilisateur est obligatoire.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "L'e-mail de l'utilisateur est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse e-mail valide.")]
        public string Email { get; set; }
        public string Salt { get; set; }
        public ClaimsIdentity UserName { get; set; }
    }


}
