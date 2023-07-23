using Mercadona.Repository.Categorie;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Promotion
{
    public interface IPromotionRepository
    {
        public List<PromotionModel> GetAllPromotions();

        public PromotionModel GetPromotionById(int id);

        public bool CreatePromotion(string libellePromotion, int reduction, string dateDebut, string dateFin);
        public bool EditPromotion(PromotionModel Promotion);
        public bool DeletePromotion(int idPromotion);
    }
}
