using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Mercadona.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

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

        public IActionResult ListProduitsAdminPage(int perPage = 12, int nbPage = 1, string search = "")
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

       

        public IActionResult EditProduitPage(int idProduit)
        {

            var editProduit = _produitRepository.GetProduitById(idProduit);
            var lstCategories = _categorieRepository.GetAllCategories();
            var LstPromotions = _promotionRepository.GetAllPromotions();
            int? newIdPromotion = null;

            EditProduitViewModel vm = new EditProduitViewModel()
            {
                Produit = editProduit,
                LstCategories = lstCategories,
                LstPromotions = LstPromotions,
                NewIdPromotion = newIdPromotion
            };

            return View(vm);
        }

        public IActionResult CreateProduitPage(ProduitModel Produit)
        {
            var lstProduits = _produitRepository.GetAllProduits();
            var lstCategories = _categorieRepository.GetAllCategories();
            var LstPromotions = _promotionRepository.GetAllPromotions();
            int idCategorie = 0;
            int? idPromotion = null;


            var vm = new CreateProduitViewModel()
            {
                LstProduits = lstProduits,
                LstCategories = lstCategories,
                LstPromotions = LstPromotions,
                idCategorie = idCategorie,
                idPromotion = idPromotion
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteProduitAction(EditProduitViewModel vm)
        {
            bool isOk = _produitRepository.DeleteProduit(vm.Produit.IdProduit);

            if (isOk)
            {
                TempData["MessageValidation"] = "Le produit a bien été supprimé";
                return RedirectToAction("Index");
            }
            else
            {
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.LstPromotions = _promotionRepository.GetAllPromotions();
                vm.Produit = _produitRepository.GetProduitById(vm.Produit.IdProduit);
                return View("EditProduitPage", vm);
            }
        }

        [HttpPost]
        public IActionResult EditProduitAction(EditProduitViewModel vm, int? NewIdPromotion)
        {

            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.LstPromotions = _promotionRepository.GetAllPromotions();
                return View("EditProduitPage", vm);
            }
            bool isOk = _produitRepository.EditProduit(vm.Produit, NewIdPromotion);

            if (isOk)
            {
                TempData["MessageValidation"] = "Le produit a bien été modifié";
                return RedirectToAction("ListProduitsAdminPage");
            }
            else
            {
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.LstPromotions = _promotionRepository.GetAllPromotions();

                return View("EditProduitPage", vm);
            }
        }

        [HttpPost]
        public IActionResult CreateProduitAction(CreateProduitViewModel vm, int idCategorie, int? idPromotion)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                vm.LstProduits = _produitRepository.GetAllProduits();
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.LstPromotions = _promotionRepository.GetAllPromotions();
                return View("CreateProduitPage", vm);
            }

            bool isOk = _produitRepository.CreateProduit(vm.Produit, idCategorie, idPromotion );

            if (isOk)
            {
                TempData["MessageValidation"] = "Le produit a bien été créé";
                return RedirectToAction("ListProduitsAdminPage");
            }
            else
            {
                vm.LstProduits = _produitRepository.GetAllProduits();
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.LstPromotions = _promotionRepository.GetAllPromotions();

                return View("CreateProduitPage", vm);
            }
        }

        public IActionResult ListCategoriesPage()
        {
            var allcategories = _categorieRepository.GetAllCategories();

            ListCategoriesViewModel vm = new ListCategoriesViewModel()
            {
                LstCategories = allcategories
            };

            return View(vm);
        }

        public IActionResult EditCategoriePage(int idCategorie)
        {

            var editCategorie = _categorieRepository.GetCategorieById(idCategorie);

            EditCategorieViewModel vm = new EditCategorieViewModel()
            {
                Categorie = editCategorie
            };

            return View(vm);
        }

        public IActionResult CreateCategoriePage(CreateCategorieViewModel vm)
        {
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteCategorieAction(EditCategorieViewModel vm)
        {
            bool isOk = _categorieRepository.DeleteCategorie(vm.Categorie.IdCategorie);

            if (isOk)
            {
                TempData["MessageValidation"] = "La catégorie a bien été supprimé";
                return RedirectToAction("ListCategoriesPage");
            }
            else
            {
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.Categorie = _categorieRepository.GetCategorieById(vm.Categorie.IdCategorie);
                return View("EditCategoriePage", vm);
            }
        }

        [HttpPost]
        public IActionResult EditCategorieAction(EditCategorieViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("EditCategoriePage", vm);
            }
            bool isOk = _categorieRepository.EditCategorie(vm.Categorie);

            if (isOk)
            {
                TempData["MessageValidation"] = "La catégorie a bien été modifiée";
                return RedirectToAction("ListCategoriesPage");
            }
            else
            {
                return View("EditCategoriePage", vm);
            }
        }

        [HttpPost]
        public IActionResult CreateCategorieAction(CreateCategorieViewModel vm, string libelleCategorie)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("CreateCategoriePage", vm);
            }

            bool isOk = _categorieRepository.CreateCategorie(libelleCategorie);

            if (isOk)
            {
                TempData["MessageValidation"] = "Le produit a bien été créé";
                return RedirectToAction("ListCategoriesPage");
            }
            else
            {
                return View("CreateCategoriePage", vm);
            }
        }
    }
}
