using Mercadona.Repository.Categorie;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mercadona.Models
{
    public class EditCategorieViewModel
    {
        public CategorieModel Categorie { get; set; }

        public string ActualCategorie { get; set; }

    }
}
