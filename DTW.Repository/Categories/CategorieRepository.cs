using Mercadona.Repository.config;
using Mercadona.Repository.Categorie;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Categorie
{
    public class CategorieRepository : BaseRepository, ICategorieRepository
    {
        public CategorieRepository(IConfiguration configuration): base (configuration)
        {

        }

        public List<CategorieModel> GetAllCategories()
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql
            
            string sql = @"
                SELECT 
                    c.idCategorie, 
                    c.libelle,
                   
                FROM 
                    categories c
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var listeCategories = new List<CategorieModel>();

            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                var laCategorie = new CategorieModel(
                    Convert.ToInt32(reader["idCategorie"]),
                    reader["libelle"].ToString()
                );

                listeCategories.Add(laCategorie);
            }

            cnn.Close();
            return listeCategories;
        }


        public CategorieModel GetCategorieById(int id)
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql

            string sql = @"
                SELECT 
                    c.idCategorie, 
                    c.libelle,
                FROM 
                    categories c
                WHERE 
                    c.idCategorie = @idCategorie
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idCategorie", id);
            var reader = cmd.ExecuteReader();
            CategorieModel maCategorie = null;

            //Récupérer le retour, et le transformer en objet
            if (reader.Read())
            {
                maCategorie = new CategorieModel()
                {
                    IdCategorie = Convert.ToInt32(reader["idCategorie"]),
                    Libelle = reader["libelle"].ToString(),
                };
            }

            cnn.Close();
            return maCategorie;
        }



    }
}
