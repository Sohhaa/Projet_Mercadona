using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Categorie
{
    public interface ICategorieRepository
    {
        public List<CategorieModel> GetAllCategories();

        public CategorieModel GetCategorieById(int idCategorie);
        public bool CreateCategorie(string libelleCategorie);
        public bool EditCategorie(int idCategorie);
        public bool DeleteCategorie(int idCategorie);

        //public bool EditProduit(ProduitModel produit);
        //public bool DeleteProduit(int id);
    }
}
