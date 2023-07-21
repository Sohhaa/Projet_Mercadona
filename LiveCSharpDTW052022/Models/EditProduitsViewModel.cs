using Mercadona.Repository.Produits;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class EditProduitsViewModel
    {
        public ProduitModel monLien { get; set; }
        public List<ProduitModel> lstUsers { get; set; }
    }
}
