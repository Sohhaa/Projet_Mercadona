using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using Quartz;

namespace Mercadona.Utilities 
{


    public class MyJob : IJob
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IProduitRepository _produitRepository;


        public MyJob(IPromotionRepository promotionRepository, IProduitRepository produitRepository)
        {
            _promotionRepository = promotionRepository;
            _produitRepository = produitRepository;

        }

        public Task Execute(IJobExecutionContext context)
        {


            RemoveExpiredPromotion();

            return Task.CompletedTask;
        }


        private void RemoveExpiredPromotion()
        {

            var allPromotions = _promotionRepository.GetAllPromotions();
            List<int> allPromotionsEnCours = new List<int>();

            var allProduits = _produitRepository.GetAllProduits();

            DateTime dateDuJour = DateTime.Now;

            foreach (var promotion in allPromotions)
            {
                DateTime dateDebutPromo = DateTime.ParseExact(promotion.DateDebut, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                DateTime dateFinPromo = DateTime.ParseExact(promotion.DateFin, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);


                if (dateDuJour >= dateDebutPromo && dateDuJour <= dateFinPromo)
                {
                    allPromotionsEnCours.Add(promotion.IdPromotion);
                }
            };

            foreach (var produit in allProduits)
            {
                if (produit.Promotion != null)
                {
                    if (!allPromotionsEnCours.Contains(produit.Promotion.IdPromotion))
                    {
                        _produitRepository.RemoveExpiredPromotion(produit);
                    }
                }
            }
        }
    }
}