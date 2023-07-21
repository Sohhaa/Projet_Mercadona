using Mercadona.Repository.Categorie;
using Mercadona.Repository.config;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Produits
{
    public class ProduitRepository : BaseRepository, IProduitRepository
    {
        public ProduitRepository(IConfiguration configuration): base (configuration)
        {
        }

        public List<ProduitModel> GetAllProduits()
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql
            
            string sql = @"
                SELECT 
                    p.idProduit, 
                    p.libelle,
                    p.description, 
                    p.prix,
                    p.image,
                    c.idCategorie,
                    c.libelle as CategorieLibelle,
                    p.idPromotion
                FROM 
                    produits p
                INNER JOIN categories c ON p.idCategorie = c.idCategorie
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var listeProduits = new List<ProduitModel>();

            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                CategorieModel categorie = new CategorieModel()
                {
                    IdCategorie = Convert.ToInt32(reader["idCategorie"]),
                    Libelle = reader["CategorieLibelle"].ToString()
                };

                var leproduit = new ProduitModel(
                    Convert.ToInt32(reader["idProduit"]),
                    reader["libelle"].ToString(),
                    reader["description"].ToString(),
                    (float)Convert.ToDouble(reader["prix"]),
                    reader["image"].ToString(),
                    categorie,
                    Convert.ToInt32(reader["idPromotion"])

                );

                listeProduits.Add(leproduit);
            }

            cnn.Close();
            return listeProduits;
        }


     

    }
}
