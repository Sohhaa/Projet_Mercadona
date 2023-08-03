using Mercadona.Repository.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System;
using Mercadona.Models;
using System.Security.Cryptography;
using System.Text;
using Mercadona.Utilities;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<ActionResult> ConnectUser(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }




            // Vérifier l'authentification de l'utilisateur
            UserModel user = _userRepository.GetUserByEmail(model.Email);
            if (user != null) { 
                var passwordHasher = new PasswordHasher();

                // Concaténer le sel avec le mot de passe entré par l'utilisateur
                byte[] saltBytes = Convert.FromBase64String(user.Salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(model.Password);
                byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Array.Copy(saltBytes, combinedBytes, saltBytes.Length);
                Array.Copy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                // Appliquer la même fonction de hachage utilisée lors du hachage du mot de passe initial
                using (var sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                    string hashedPassword = Convert.ToBase64String(hashBytes);

                    // Comparer le hachage obtenu avec le hachage stocké en base de données
                    if (hashedPassword == user.Password)
                    {
                        UserModel CurrentUser = _userRepository.GetUserById(user.IdUser);
                        HttpContext.Session.SetObject("CurrentUser", CurrentUser);

                        // Maintenant, authentifions l'utilisateur et créons un cookie d'authentification
                        var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, CurrentUser.FirstName), // Utilisez ici le nom d'utilisateur approprié de votre modèle UserModel
                    // Ajoutez d'autres revendications si nécessaire
                };

                        var identity = new ClaimsIdentity(claims, "CookieAuthentication");
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync("CookieAuthentication", principal);

                        TempData["MessageValidation"] = "Bienvenue, "+CurrentUser.FirstName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Identifiants invalides.");
                        return View("Index");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Identifiants invalides.");
                return View("Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Déconnectez l'utilisateur actuellement authentifié
            await HttpContext.SignOutAsync("CookieAuthentication");

            // Redirigez l'utilisateur vers la page d'accueil ou toute autre page de votre choix
            return RedirectToAction("Index", "Home");
        }


    }
}