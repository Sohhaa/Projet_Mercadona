using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class CreateCategorieViewModel
    {
        public string libelleCategorie { get; set; }
        public List<CategorieModel> LstCategories { get; set; }

    }
}
