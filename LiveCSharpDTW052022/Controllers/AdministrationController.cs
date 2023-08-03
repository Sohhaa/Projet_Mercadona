using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Mercadona.Repository.User;
using Mercadona.Models;
using Mercadona.Utilities;
using Microsoft.AspNetCore.Identity;

namespace Mercadona.Controllers
{
    public class AdministrationController : Controller
    {

        private readonly IProduitRepository _produitRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IUserRepository _userRepository;


        public AdministrationController(IProduitRepository produitRepository, ICategorieRepository categorieRepository, IPromotionRepository promotionRepository, IUserRepository userRepository)
        {
            _produitRepository = produitRepository;
            _categorieRepository = categorieRepository;
            _promotionRepository = promotionRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var allProduits = _produitRepository.GetAllProduits();
            var allCategories = _categorieRepository.GetAllCategories();
            var allPromotions = _promotionRepository.GetAllPromotions();
            var allUsers = _userRepository.GetAllUsers();

            int nombreTotalProduits = allProduits.Count();
            int nombreTotalCategories = allCategories.Count();
            int nombreTotalUsers = allUsers.Count();

            List<PromotionModel> LstPromotionsEnCours = new List<PromotionModel>();

            DateTime now = DateTime.Now;
            DateTime dateDuJour = now.Date;

            foreach (var promotionEnCours in allPromotions)
            {
                    DateTime dateDebutPromo = DateTime.ParseExact(promotionEnCours.DateDebut, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime dateFinPromo = DateTime.ParseExact(promotionEnCours.DateFin, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    if (dateDuJour >= dateDebutPromo && dateDuJour <= dateFinPromo)
                    {
                    LstPromotionsEnCours.Add(promotionEnCours);
                    }
            }

            int nombreTotalPromotionsEnCours = LstPromotionsEnCours.Count();

            var vm = new AdministrationViewModel()
            {
                NombreTotalProduits = nombreTotalProduits,
                NombreTotalCategories = nombreTotalCategories,
                NombreTotalPromotionsEnCours = nombreTotalPromotionsEnCours,
                NombreTotalUsers = nombreTotalUsers
            };
            return View(vm);
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

        public IActionResult AddPromotionProduitPage(int idProduit)
        {

            var editProduit = _produitRepository.GetProduitById(idProduit);
            var LstPromotions = _promotionRepository.GetAllPromotions();
            int? newIdPromotion = null;

            EditProduitViewModel vm = new EditProduitViewModel()
            {
                Produit = editProduit,
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
            int? idPromotion = null;


            var vm = new CreateProduitViewModel()
            {
                LstCategories = lstCategories,
                LstPromotions = LstPromotions,
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
        public IActionResult CreateProduitAction(CreateProduitViewModel vm, int? idPromotion)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                vm.LstCategories = _categorieRepository.GetAllCategories();
                vm.LstPromotions = _promotionRepository.GetAllPromotions();
                return View("CreateProduitPage", vm);
            }

            bool isOk = _produitRepository.CreateProduit(vm.Produit, idPromotion );


            if (isOk)
            {
                TempData["MessageValidation"] = "Le produit a bien été créé";
                return RedirectToAction("ListProduitsAdminPage");
            }
            else
            {
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
            string actualCategorie = editCategorie.Libelle;

            EditCategorieViewModel vm = new EditCategorieViewModel()
            {
                Categorie = editCategorie,
                ActualCategorie = actualCategorie
            };

            return View(vm);
        }

        public IActionResult CreateCategoriePage()
        {
            var newCategorie = new CategorieModel();

            CreateCategorieViewModel vm = new CreateCategorieViewModel()
            {
                NewCategorie = newCategorie
            };

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
        public IActionResult CreateCategorieAction(CreateCategorieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("CreateCategoriePage", vm);
            }

            bool isOk = _categorieRepository.CreateCategorie(vm.NewCategorie.Libelle);

            if (isOk)
            {
                TempData["MessageValidation"] = "La catégorie a bien été créée";
                return RedirectToAction("ListCategoriesPage");
            }
            else
            {
                return View("CreateCategoriePage", vm);
            }
        }


        public IActionResult ListPromotionsPage()
        {
            var allPromotions = _promotionRepository.GetAllPromotions();

            List<PromotionModel> allPromotionsEnCours = new List<PromotionModel>();
            List<PromotionModel> allPromotionsTerminees = new List<PromotionModel>();
            List<PromotionModel> allPromotionsFutures = new List<PromotionModel>();


            DateTime now = DateTime.Now;
            DateTime dateDuJour = now.Date;

            foreach (var promotion in allPromotions)
            {
                DateTime dateDebutPromo = DateTime.ParseExact(promotion.DateDebut, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime dateFinPromo = DateTime.ParseExact(promotion.DateFin, "dd-MM-yyyy", CultureInfo.InvariantCulture);


                if (dateDuJour >= dateDebutPromo && dateDuJour <= dateFinPromo)
                {
                    allPromotionsEnCours.Add(promotion);
                }
                else if (dateDebutPromo >= dateDuJour)
                {
                    allPromotionsFutures.Add(promotion);
                }
                else
                {
                    allPromotionsTerminees.Add(promotion);
                }
            };

            ListPromotionsViewModel vm = new ListPromotionsViewModel()
            {
                LstPromotionsEnCours = allPromotionsEnCours,
                LstPromotionsTerminees = allPromotionsTerminees,
                LstPromotionsFutures = allPromotionsFutures
            };
           


            return View(vm);
        }

        public IActionResult EditPromotionPage(int idPromotion)
        {

            var editPromotion = _promotionRepository.GetPromotionById(idPromotion);

            EditPromotionViewModel vm = new EditPromotionViewModel()
            {
                Promotion = editPromotion
            };

            return View(vm);
        }

        public IActionResult CreatePromotionPage()
        {
            var promotion = new PromotionModel();

            CreatePromotionViewModel vm = new CreatePromotionViewModel()
            {
                Promotion = promotion
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeletePromotionAction(EditPromotionViewModel vm)
        {
            bool isOk = _promotionRepository.DeletePromotion(vm.Promotion.IdPromotion);

            if (isOk)
            {
                TempData["MessageValidation"] = "La promotion a bien été supprimée";
                return RedirectToAction("ListPromotionsPage");
            }
            else
            {
                vm.Promotion = _promotionRepository.GetPromotionById(vm.Promotion.IdPromotion);
                return View("EditPromotionPage", vm);
            }
        }

        [HttpPost]
        public IActionResult EditPromotionAction(EditPromotionViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("EditPromotionPage", vm);
            }
            bool isOk = _promotionRepository.EditPromotion(vm.Promotion);

            if (isOk)
            {
                TempData["MessageValidation"] = "La promotion a bien été modifiée";
                return RedirectToAction("ListPromotionsPage");
            }
            else
            {
                return View("EditPromotionPage", vm);
            }
        }

        [HttpPost]
        public IActionResult CreatePromotionAction(CreatePromotionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("CreatePromotionPage", vm);
            }

            bool isOk = _promotionRepository.CreatePromotion(vm.Promotion);

            if (isOk)
            {
                TempData["MessageValidation"] = "La promotion a bien été créé";
                return RedirectToAction("ListPromotionsPage");
            }
            else
            {
                return View("CreatePromotionPage", vm);
            }
        }

        //  ------------------------- USER ADMINISTRATION-------------------

        public IActionResult ListUsersPage()
        {
            var allUsers = _userRepository.GetAllUsers();

            List<UserModel> allUser = new List<UserModel>();

       
            ListUsersViewModel vm = new ListUsersViewModel()
            {
                LstUsers = allUsers,
            };



            return View(vm);
        }

        public IActionResult EditUserPage(int idUser)
        {

            var editUser = _userRepository.GetUserById(idUser);

            EditUserViewModel vm = new EditUserViewModel()
            {
                User = editUser
            };

            return View(vm);
        }

        public IActionResult CreateUserPage(CreateUserViewModel vm)
        {

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteUserAction(EditUserViewModel vm)
        {

            if(vm.User.IdUser != 1) { 
            bool isOk = _userRepository.DeleteUser(vm.User.IdUser);





            if (isOk)
            {
                TempData["MessageValidation"] = "L'utilisateur a bien été supprimé";
                return RedirectToAction("ListUsersPage");
            }
            else
            {
                vm.User = _userRepository.GetUserById(vm.User.IdUser);
                return View("EditUserPage", vm);
            }
            }
            else
            {
                TempData["MessageValidation"] = "L'utilisateur ne peut pas être supprimé";
                vm.User = _userRepository.GetUserById(vm.User.IdUser);
                return View("EditUserPage", vm);
            }
        }


        [HttpPost]
        public IActionResult EditUserAction(EditUserViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("EditUserPage", vm);
            }

            var passwordHasher = new PasswordHasher();
            string salt;
            string hashedPassword = passwordHasher.HashPassword(vm.User.Password, out salt);

            vm.User.Password = hashedPassword;
            vm.User.Salt = salt;

            bool isOk = _userRepository.EditUser(vm.User);

            if (isOk)
            {
                TempData["MessageValidation"] = "L'utilisateur a bien été modifié";
                return RedirectToAction("ListUsersPage");
            }
            else
            {
                return View("EditUserPage", vm);
            }
        }

        [HttpPost]
        public IActionResult CreateUserAction(CreateUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageErreur"] = "Erreur";
                return View("CreateUserPage", vm);
            }

            List<UserModel> allUsers = _userRepository.GetAllUsers();

            bool sameEmail = false;

            foreach(UserModel user in allUsers)
            {
                if (vm.User.Email == user.Email)
                {
                    sameEmail = true;
                }
            }

            if (!sameEmail) { 
            var passwordHasher = new PasswordHasher();

            string salt;
            string hashedPassword = passwordHasher.HashPassword(vm.User.Password, out salt);

            vm.User.Password = hashedPassword;
            vm.User.Salt = salt;

            bool isOk = _userRepository.CreateUser(vm.User);

            if (isOk)
            {
                TempData["MessageValidation"] = "L'utilisateur a bien été créé";
                return RedirectToAction("ListUsersPage");
            }
            else
            {
                return View("CreateUserPage", vm);
            }
            }
            else
            {
                TempData["MessageErreur"] = "Cet adresse email existe déjà";
                return RedirectToAction("CreateUserPage", vm);
            }
        }


    }
}
