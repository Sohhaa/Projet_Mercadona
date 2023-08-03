using Mercadona.Repository.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Produits 
{
    public interface IProduitRepository
    {
        public List<ProduitModel> GetAllProduits();

        public ProduitModel GetProduitById(int idProduit);
        public bool EditProduit(ProduitModel produit, int? NewIdPromotion);
        public bool DeleteProduit(int idProduit);
        public bool CreateProduit(ProduitModel produit, int? idPromotion);

        public bool RemoveExpiredPromotion(ProduitModel produit);


    }
}
