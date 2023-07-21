using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mercadona.Repository.Categorie
{
    public class CategorieModel
    {
        public CategorieModel()
        {

        }

        public CategorieModel(int idCategorie, string libelle)
        {
            IdCategorie = idCategorie;
            Libelle = libelle;
          
        }

        public int IdCategorie { get; set; }
        [Required]
        public string Libelle { get; set; }

    }
}
