using Mercadona.Repository.Produits;
using Mercadona.Repository.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class CreateUserViewModel
    {
        public UserModel User { get; set; }

        public List<UserModel> LstUsers { get; set; }

    }
}
