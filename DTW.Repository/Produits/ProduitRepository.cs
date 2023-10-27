using Mercadona.Repository.Categorie;
using Mercadona.Repository.config;
using Mercadona.Repository.Promotion;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
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
                    p.idCategorie,
                    p.idPromotion,
                    c.libelle AS categorieLibelle,
                    promo.libelle AS promotionLibelle, 
                    promo.dateDebut, 
                    promo.dateFin, 
                    promo.reduction
                FROM produits p
                LEFT JOIN categories c ON p.idCategorie = c.idCategorie
                LEFT JOIN promotions promo ON p.idPromotion = promo.idPromotion
                ORDER BY promo.dateDebut DESC;

                ";

            //Executer la requête sql, donc créer une commande
            //MySqlCommand cmd = new MySqlCommand(sql, cnn);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var listeProduits = new List<ProduitModel>();

            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                CategorieModel categorie = new CategorieModel()
                {
                    IdCategorie = Convert.ToInt32(reader["idCategorie"]),
                    Libelle = reader["categorieLibelle"].ToString()
                };

                PromotionModel promotion = null;

                if (!reader.IsDBNull(reader.GetOrdinal("idPromotion")))
                {
                    promotion = new PromotionModel()
                    {
                        IdPromotion = Convert.ToInt32(reader["idPromotion"]),
                        Libelle = reader["promotionLibelle"].ToString(),
                        Reduction = Convert.ToInt32(reader["reduction"]),
                        DateDebut = Convert.ToDateTime(reader["dateDebut"].ToString()).ToString(),
                        DateFin = Convert.ToDateTime(reader["dateFin"].ToString()).ToString(),
                    };
                }

                var leproduit = new ProduitModel(
                    Convert.ToInt32(reader["idProduit"]),
                    reader["libelle"].ToString(),
                    reader["description"].ToString(),
                    (float)Convert.ToDouble(reader["prix"]),
                    reader["image"].ToString(),
                    categorie,
                    promotion
                );

                listeProduits.Add(leproduit);
            }

            cnn.Close();
            return listeProduits;
        }

        public ProduitModel GetProduitById(int idProduit)
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
                    p.idCategorie,
                    p.idPromotion,
                    c.libelle AS categorieLibelle,
                    promo.libelle AS promotionLibelle, 
                    promo.dateDebut, 
                    promo.dateFin, 
                    promo.reduction
                FROM produits p
                LEFT JOIN categories c ON p.idCategorie = c.idCategorie
                LEFT JOIN promotions promo ON p.idPromotion = promo.idPromotion
                WHERE p.idProduit = @idProduit
                ";

            //Executer la requête sql, donc créer une commande
            //MySqlCommand cmd = new MySqlCommand(sql, cnn);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idProduit", idProduit);
            var reader = cmd.ExecuteReader();
            ProduitModel myProduit = null;

            //Récupérer le retour, et le transformer en objet
            if (reader.Read())
            {
                CategorieModel categorie = new CategorieModel()
                {
                    IdCategorie = Convert.ToInt32(reader["idCategorie"]),
                    Libelle = reader["CategorieLibelle"].ToString()
                };

                PromotionModel promotion = null;

                if (!reader.IsDBNull(reader.GetOrdinal("idPromotion")))
                {
                    promotion = new PromotionModel()
                    {
                        IdPromotion = Convert.ToInt32(reader["idPromotion"]),
                        Libelle = reader["promotionLibelle"].ToString(),
                        DateDebut = reader["dateDebut"].ToString(),
                        DateFin = reader["dateFin"].ToString(),
                        Reduction = Convert.ToInt32(reader["reduction"])
                    };
                }

                myProduit = new ProduitModel(
                    Convert.ToInt32(reader["idProduit"]),
                    reader["libelle"].ToString(),
                    reader["description"].ToString(),
                    (float)Convert.ToDouble(reader["prix"]),
                    reader["image"].ToString(),
                    categorie,
                    promotion
                );
            }

            cnn.Close();
            return myProduit;
        }

        public bool EditProduit(ProduitModel produit, int? NewIdPromotion)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                UPDATE produits
                SET 
                    libelle = @libelle,
                    description = @description, 
                    prix = @prix,
                    image = @image,
                    idCategorie = @idCategorie,
                    idPromotion = @idPromotion
                WHERE 
                    idProduit = @idProduit
                ";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idProduit", produit.IdProduit);
                cmd.Parameters.AddWithValue("@libelle", produit.Libelle);
                cmd.Parameters.AddWithValue("@description", produit.Description);
                cmd.Parameters.AddWithValue("@prix", produit.Prix);
                cmd.Parameters.AddWithValue("@image", produit.Image);
                cmd.Parameters.AddWithValue("@idCategorie", produit.Categorie.IdCategorie);

                if (NewIdPromotion.HasValue)
                {
                    cmd.Parameters.AddWithValue("@idPromotion", NewIdPromotion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idPromotion", DBNull.Value);
                }

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

        public bool DeleteProduit(int idProduit)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                    DELETE FROM 
                        produits
                    WHERE 
                        idProduit = @idProduit
                    ";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idProduit", idProduit);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CreateProduit(ProduitModel Produit, int? idPromotion)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                    INSERT INTO produits 
                        (libelle, description, prix, image, idCategorie, idPromotion)
                    VALUES
                        (@libelle, @description, @prix, @image, @idCategorie, @idPromotion)";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@libelle", Produit.Libelle);
                cmd.Parameters.AddWithValue("@description", Produit.Description);
                cmd.Parameters.AddWithValue("@prix", Produit.Prix);
                cmd.Parameters.AddWithValue("@image", Produit.Image);
                cmd.Parameters.AddWithValue("@idCategorie", Produit.Categorie.IdCategorie);

                if (idPromotion.HasValue)
                {
                    cmd.Parameters.AddWithValue("@idPromotion", idPromotion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idPromotion", DBNull.Value);
                }

                var reader = cmd.ExecuteNonQuery();


                cnn.Close();
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveExpiredPromotion(ProduitModel produit)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                UPDATE produits
                SET 
                    idPromotion = @idPromotion
                WHERE 
                    idProduit = @idProduit
                ";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idProduit", produit.IdProduit);
                cmd.Parameters.AddWithValue("@idPromotion", DBNull.Value);
              
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

    }
}
