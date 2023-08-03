using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mercadona.Models
{
    public class CreateCategorieViewModel
    {

        public CategorieModel NewCategorie { get; set; }

    }
}
