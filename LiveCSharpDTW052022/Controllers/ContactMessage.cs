using Mercadona.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Mercadona.Repository.ContactMessage;


namespace Mercadona.Controllers
{
    public class ContactMessageController : Controller
    {
        private readonly IContactMessageRepository _contactMessageRepository;

        public ContactMessageController(IContactMessageRepository contactMessageRepository)
        {
            _contactMessageRepository = contactMessageRepository;
        }

        public IActionResult Index()
        {

            return View("Index");
        }

        public IActionResult SendContactMessage(ContactMessageModel ContactMessage)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("Index");
            }

            bool isOk = _contactMessageRepository.SaveContactMessage(ContactMessage);


            if (isOk)
            {
                TempData["MessageValidation"] = "Votre message a bien été envoyé au service client Mercadona !";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }
        }

    }
}
