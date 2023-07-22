using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Promotion
{
    public interface IPromotionRepository
    {
        public List<PromotionModel> GetAllPromotions();

        public PromotionModel GetPromotionById(int id);

        //public bool EditProduit(ProduitModel produit);
        //public bool DeleteProduit(int id);
    }
}
