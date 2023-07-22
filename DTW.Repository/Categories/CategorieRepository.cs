﻿using Mercadona.Repository.config;
using Mercadona.Repository.Produits;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
                    c.libelle
                   
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


        public CategorieModel GetCategorieById(int idCategorie)
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
            cmd.Parameters.AddWithValue("@idCategorie", idCategorie);
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

        public bool CreateCategorie(string libelleCategorie)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                    INSERT INTO categories 
                        libelle
                    VALUES
                        @libelleCategorie";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@libelleCategorie", libelleCategorie);

                var reader = cmd.ExecuteNonQuery();


                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool EditCategorie(int idCategorie)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                UPDATE categories
                SET 
                    libelle = @idCategorie,
                WHERE 
                    idCategorie = @idCategorie
                ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idCategorie", idCategorie);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
                Console.WriteLine($"StackTrace : {e.StackTrace}");
                return false;
            }
        }

        public bool DeleteCategorie(int idCategorie)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                    DELETE FROM 
                        categories
                    WHERE 
                        idCategorie = @idCategorie
                    ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idCategorie", idCategorie);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
