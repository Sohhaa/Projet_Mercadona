using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using Mercadona.Repository.User;
using System.Collections.Generic;

namespace Mercadona.Models.Administration
{
    public class AdministrationViewModel
    {
        public List<ProduitModel> LstProduits { get; set; }
        public List<CategorieModel> LstCategories { get; set; }
        public List<PromotionModel> LstPromotions { get; set; }
        public UserModel User { get; set; }
        public int NombreTotalProduits { get; set; }
        public int NombreTotalCategories { get; set; }
        public int NombreTotalPromotionsEnCours { get; set; }


    }
}
