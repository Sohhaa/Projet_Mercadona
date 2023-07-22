using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class CreateProduitViewModel
    {
        public ProduitModel Produit { get; set; }
        public List<ProduitModel> LstProduits { get; set; }
        public List<CategorieModel> LstCategories { get; set; }
        public List<PromotionModel> LstPromotions { get; set; }

        public int idCategorie { get; set; }
        public int? idPromotion { get; set; }
    }
}
