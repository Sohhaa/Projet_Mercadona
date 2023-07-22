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
        public string Libelle { get; set; }
        [Required(ErrorMessage = "Merci de définir une date de début pour la promotion.")]
        public string DateDebut { get; set; }
        [Required(ErrorMessage = "Merci de définir une date de fin pour la promotion.")]

        public string DateFin { get; set; }
        [Required(ErrorMessage = "Merci de définir un pourcentage pour la promotion.")]

        public int Reduction { get; set; }

    }
}
