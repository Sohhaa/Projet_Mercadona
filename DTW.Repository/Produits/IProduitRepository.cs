using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Produits
{
    public interface IProduitRepository
    {
        public List<ProduitModel> GetAllProduits();

        //public bool EditProduit(ProduitModel produit);
        //public bool DeleteProduit(int id);
    }
}
