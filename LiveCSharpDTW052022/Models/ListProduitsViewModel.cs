using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using System;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class ListProduitsViewModel
    {
        public int PerPage { get; set; }
        public int NbPage { get; set; }
        public int NbProduitsTotalBdd { get; set; }

        public int NbPageTotal
        {
            get
            {
                return Convert.ToInt16(Math.Ceiling(((decimal)NbProduitsTotalBdd / PerPage)));
            }
        }
        public List<ProduitModel> LstProduits { get; set; }
        public List<CategorieModel> LstCategories { get; set; }

        public int Filtre { get; set; }

        public string Recherche { get; set; }

    }
}
