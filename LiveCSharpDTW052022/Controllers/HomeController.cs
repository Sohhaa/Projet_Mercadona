using Mercadona.Models;
using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mercadona.Controllers

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduitRepository _produitRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IPromotionRepository _promotionRepository;

        public HomeController(IProduitRepository produitRepository, ICategorieRepository categorieRepository, IPromotionRepository promotionRepository, ILogger<HomeController> logger)
        {
            _logger = logger;

            _produitRepository = produitRepository;
            _categorieRepository = categorieRepository;
            _promotionRepository = promotionRepository;
        }


        public IActionResult Index()
        {

            var allProduits = _produitRepository.GetAllProduits();
            var allCategories = _categorieRepository.GetAllCategories();
            var allPromotions = _promotionRepository.GetAllPromotions();

            List<PromotionModel> LstPromotionsEnCours = new List<PromotionModel>();
            List<PromotionModel> LstPromotionsEnCoursWithProduit = new List<PromotionModel>();
            List<ProduitModel> LstProduitWithPromotion = new List<ProduitModel>();

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

            foreach (var produitWithPromotion in allProduits)
            {
                if (produitWithPromotion.Promotion != null)
                {
                    LstProduitWithPromotion.Add(produitWithPromotion);
                }
            }

            var vm = new HomeViewModel()
            {
                LstProduitWithPromotion = LstProduitWithPromotion,
                LstCategories = allCategories,
                LstPromotionsEnCours = LstPromotionsEnCours,
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
