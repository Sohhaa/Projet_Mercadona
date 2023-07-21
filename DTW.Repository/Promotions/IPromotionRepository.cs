using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Promotion
{
    public interface IPromotionRepository
    {
        public List<PromotionModel> GetAllPromotion();

        public PromotionModel GetPromotionById(int id);
        //public void CreateCategorie(CategorieModel categorie);

        //public bool EditProduit(ProduitModel produit);
        //public bool DeleteProduit(int id);
    }
}
