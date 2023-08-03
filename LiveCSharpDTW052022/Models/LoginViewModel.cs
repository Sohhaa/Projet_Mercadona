using Mercadona.Repository.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mercadona.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Veuillez saisir une adresse e-mail.")]
        [EmailAddress(ErrorMessage = "Veuillez saisir une adresse e-mail valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un mot de passe.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        UserModel User { get; set; }
    }
}