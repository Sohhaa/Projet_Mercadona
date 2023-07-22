using Mercadona.Repository.Produits;
using Mercadona.Repository.Categorie;
using Mercadona.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mercadona.Repository.Promotion;

namespace Mercadona.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IProduitRepository _produitRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IPromotionRepository _promotionRepository;

        //private readonly ICategorieRepository _categorieRepository;

        public CatalogueController(IProduitRepository produitRepository, ICategorieRepository categorieRepository, IPromotionRepository promotionRepository)
        {
            _produitRepository = produitRepository;
            _categorieRepository = categorieRepository;
            _promotionRepository = promotionRepository;
            //_categorieRepository = categorieRepository;
        }

        public IActionResult Index(int perPage = 12, int nbPage = 1, int filtreCategorie = 0)
        {
            var allProduits = _produitRepository.GetAllProduits();
            var allCategories = _categorieRepository.GetAllCategories();
    

            if (filtreCategorie > 0)
            {
                allProduits = allProduits.Where(produit =>
                        produit.Categorie.IdCategorie == filtreCategorie).ToList();

            }

            int nbProduitsTotal = allProduits.Count();

            allProduits = allProduits.Skip(perPage * (nbPage - 1))
                               .Take(perPage)
                               .ToList();



            var vm = new ListProduitsViewModel()
            {
                LstProduits = allProduits,
                LstCategories = allCategories,
                Filtre = filtreCategorie,
                NbProduitsTotalBdd = nbProduitsTotal,
                NbPage = nbPage,
                PerPage = perPage
            };



            return View(vm);
        }
                
    }
}
