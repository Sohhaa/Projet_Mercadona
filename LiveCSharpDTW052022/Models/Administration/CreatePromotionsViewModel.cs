using Mercadona.Repository.Categorie;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Promotion;
using System.Collections.Generic;

namespace Mercadona.Models.Administration
{
    public class CreatePromotionViewModel
    {
        public string libellePromotion { get; set; }
        public float reduction { get; set; }
        public string dateDebut { get; set; }
        public string dateFin { get; set; }
    }
}
