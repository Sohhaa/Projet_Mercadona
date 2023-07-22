using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class EditCategorieViewModel
    {
       
        public CategorieModel Categorie { get; set; }
        public List<CategorieModel> LstCategories { get; set; }


    }
}
