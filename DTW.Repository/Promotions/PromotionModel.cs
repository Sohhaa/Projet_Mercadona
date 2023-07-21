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
        [Required]
        public string Libelle { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public int Reduction { get; set; }

    }
}
