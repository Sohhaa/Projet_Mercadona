using Mercadona.Repository.Produits;
using Mercadona.Repository.Categorie;
using Mercadona.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mercadona.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IProduitRepository _produitRepository;
        //private readonly ICategorieRepository _categorieRepository;

        public CatalogueController(IProduitRepository produitRepository, ICategorieRepository categorieRepository)
        {
            _produitRepository = produitRepository;
            //_categorieRepository = categorieRepository;
        }

        public IActionResult Index()
        {
            var allProduits = _produitRepository.GetAllProduits();
            //var allCategorie = _categorieRepository.GetAllCategories();
            var vm = new ListProduitsViewModel()
            {
                LstProduits = allProduits,
            };

            return View(vm);
        }
                
    }
}
