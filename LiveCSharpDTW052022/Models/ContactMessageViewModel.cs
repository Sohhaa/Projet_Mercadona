using Microsoft.AspNetCore.Mvc;
using Mercadona.Repository.ContactMessage;

namespace Mercadona.Models
{
	public class ContactMessageViewModel
	{
        public ContactMessageModel ContactMessage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
