using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mercadona.Repository.Promotion
{
    public class PromotionModel
    {
        public PromotionModel()
        {

        }

        public PromotionModel(int idPromotion, string libelle, string dateDebut, string dateFin, int reduction)
        {
            IdPromotion = idPromotion;
            Libelle = libelle;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Reduction = reduction;
        }

        public int IdPromotion { get; set; }

        [Required(ErrorMessage = "Le nom de la promotion est obligatoire.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Le nom de la promotion doit faire au moins 3 caractères.")]
        public string Libelle { get; set; }

        [Required(ErrorMessage = "La date de début de la promotion est obligatoire.")]
        public string DateDebut { get; set; }

        [Required(ErrorMessage = "La date de fin de la promotion est obligatoire.")]
        public string DateFin { get; set; }

        [Required(ErrorMessage = "Le pourcentage de réduction de la promotion est obligatoire.")]
        public int Reduction { get; set; }

        public PromotionModel Promotion { get; set; }
    }
}
