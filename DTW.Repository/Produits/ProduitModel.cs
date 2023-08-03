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

        [Required(ErrorMessage = "Le nom du produit est obligatoire.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Le nom du produit doit faire au moins 3 caractères.")]
        public string Libelle { get; set; }
        [Required(ErrorMessage = "La description produit est obligatoire.")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "La description du produit doit faire au moins 15 caractères.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le prix du produit est obligatoire.")]
        public float Prix { get; set; }
        [Required(ErrorMessage = "Le lien de l'image du produit est obligatoire.")]
        public string Image { get; set; }
        public CategorieModel Categorie { get; set; }

        public PromotionModel Promotion { get; set; }

    }
}
