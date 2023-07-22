using Mercadona.Repository.Categorie;
using Mercadona.Repository.Promotion;
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

        public ProduitModel(int idProduit, string libelle, string description, float prix, string image, CategorieModel categorie, PromotionModel promotion)
        {
            IdProduit = idProduit;
            Libelle = libelle;
            Description = description;
            Prix = prix;
            Image = image;
            Categorie = categorie;
            Promotion = promotion;
        }

        public int IdProduit { get; set; }
        [Required(ErrorMessage = "Merci de compléter le nom du produit.")]
        public string Libelle { get; set; }
        [Required(ErrorMessage = "Merci de compléter la description du produit.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Merci de compléter le prix du produit.")]
        public float Prix { get; set; }
        public string Image { get; set; }
        public CategorieModel Categorie { get; set; }
        public PromotionModel Promotion { get; set; }



    }
}
