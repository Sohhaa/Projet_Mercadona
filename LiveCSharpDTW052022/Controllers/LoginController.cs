using Mercadona.Repository.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System;
using Mercadona.Models.Login;

namespace Mercadona.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserRepository _userRepository;


        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ConnectUser(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Vérifier l'authentification de l'utilisateur
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("", "Identifiants invalides.");
                return View(model);
            }

            // Vous pouvez stocker les informations de l'utilisateur dans la session ou utiliser d'autres mécanismes d'authentification
            // par exemple, vous pouvez utiliser Forms Authentication ou ASP.NET Core Identity.

            // Rediriger vers la page d'accueil après la connexion réussie.
            return RedirectToAction("Index", "Home");
        }


    }
}