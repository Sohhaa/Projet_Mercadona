using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Categorie
{
    public interface ICategorieRepository
    {
        public List<CategorieModel> GetAllCategories();

        public CategorieModel GetCategorieById(int id);
        //public void CreateCategorie(CategorieModel categorie);

        //public bool EditProduit(ProduitModel produit);
        //public bool DeleteProduit(int id);
    }
}
