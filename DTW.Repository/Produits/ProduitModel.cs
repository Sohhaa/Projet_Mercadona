﻿using Mercadona.Repository.Categorie;
using Mercadona.Repository.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mercadona.Repository.Produits
{
    public class ProduitModel
    {
        public ProduitModel()
        {

        }

        public ProduitModel(int idProduit, string libelle, string description, float prix, string image, CategorieModel categorie, int idPromotion)
        {
            IdProduit = idProduit;
            Libelle = libelle;
            Description = description;
            Prix = prix;
            Image = image;
            Categorie = categorie;
            IdPromotion = idPromotion;
        }

        public int IdProduit { get; set; }
        [Required]
        public string Libelle { get; set; }
        public string Description { get; set; }
        [Required]
        public float Prix { get; set; }
        public string Image { get; set; }
        public CategorieModel Categorie { get; set; }
        public int IdPromotion { get; set; }

    }
}
