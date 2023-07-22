using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class EditProduitViewModel
    {
        public ProduitModel Produit { get; set; }
        public CategorieModel Categorie { get; set; }
        public PromotionModel Promotion { get; set; }
        public List<ProduitModel> LstProduits { get; set; }
        public List<CategorieModel> LstCategories { get; set; }
        public List<PromotionModel> LstPromotions { get; set; }
        public int? NewIdPromotion { get; set; }


    }
}
