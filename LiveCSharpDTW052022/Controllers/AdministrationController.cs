using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Mercadona.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mercadona.Controllers
{
    public class AdministrationController : Controller
    {

        private readonly IProduitRepository _produitRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IPromotionRepository _promotionRepository;

        public AdministrationController(IProduitRepository produitRepository, ICategorieRepository categorieRepository, IPromotionRepository promotionRepository)
        {
            _produitRepository = produitRepository;
            _categorieRepository = categorieRepository;
            _promotionRepository = promotionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListProduitsAdminPage(int perPage = 24, int nbPage = 1, string search = "")
        {

            var allproduits = _produitRepository.GetAllProduits();

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                allproduits = allproduits.Where(produit =>
                        produit.Libelle.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                        produit.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                        produit.Categorie.Libelle.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
            }


            int nbProduitsTotal = allproduits.Count();

            //Faire ma pagination
            // LINQ : Take pour prendre un certain nombre d'éléments
            // LINQ : Skip pour passer un certain nombre d'éléments
            allproduits = allproduits.Skip(perPage * (nbPage - 1))
                                .Take(perPage)
                                .ToList();

            var vm = new ListProduitsAdminViewModel()
            {
                LstProduits = allproduits,
                NbProduitsTotalBdd = nbProduitsTotal,
                NbPage = nbPage,
                PerPage = perPage,
                Recherche = search
            };

            return View(vm);
        }
    }
}
