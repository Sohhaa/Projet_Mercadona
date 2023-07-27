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
            Message= message;
        }
        [Required]
        public string FirstName { get; set; }    
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
