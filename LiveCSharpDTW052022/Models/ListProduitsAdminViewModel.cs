using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class ListProduitsAdminViewModel
    {
        public List<ProduitModel> LstProduits { get; set; }
        public List<CategorieModel> LstCategories { get; set; }
        public List<PromotionModel> LstPromotions { get; set; }

        public int PerPage { get; set; }
        public int NbPage { get; set; }
        public int NbProduitsTotalBdd { get; set; }
        public int NbPageTotal
        {
            get
            {
                return Convert.ToInt16(Math.Ceiling((decimal)NbProduitsTotalBdd / PerPage));
            }
        }
        public string Recherche { get; set; }
    }
}
