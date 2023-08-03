using Mercadona.Repository.ContactMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Mercadona.Repository.ContactMessage
{
    public class ContactMessageModel
    {
        public ContactMessageModel()
        {
        }

        public ContactMessageModel(string firstName, string lastName, string email, string message)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Message = message;
        }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse e-mail valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le message est obligatoire.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Le message doit faire au moins 20 caractères.")]
        public string Message { get; set; }
    }
}