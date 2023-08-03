using Mercadona.Repository.Categorie;
using Mercadona.Repository.Promotion;
using System;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class ListPromotionsViewModel
    {
        public List<PromotionModel> LstPromotionsEnCours { get; set; }
        public List<PromotionModel> LstPromotionsTerminees { get; set; }
        public List<PromotionModel> LstPromotionsFutures { get; set; }

    }
}
