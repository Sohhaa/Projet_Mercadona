using Mercadona.Repository.Produits;
using Mercadona.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mercadona.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly IProduitRepository _produitRepository;

        public ProduitsController(IProduitRepository produitRepository)
        {
            _produitRepository = produitRepository;
        }

        public IActionResult Index()
        {
            var allProduits = _produitRepository.GetAllProduits();
            var vm = new ListProduitsViewModel()
            {
                LstProduits = allProduits,
            };

            return View(vm);
        }
                
    }
}
