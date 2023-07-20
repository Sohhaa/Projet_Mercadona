using Mercadona.Repository.Links;
using Mercadona.Repository.User;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class EditLinkViewModel
    {
        public LinkModel monLien { get; set; }
        public List<UserModel> lstUsers { get; set; }
    }
}
