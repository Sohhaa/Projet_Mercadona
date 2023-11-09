using Mercadona.Repository.Categorie;
using Mercadona.Repository.config;
using Mercadona.Repository.Promotion;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Promotion
{
    public class PromotionRepository : BaseRepository, IPromotionRepository
    {
        public PromotionRepository(IConfiguration configuration): base (configuration)
        {

        }

        public List<PromotionModel> GetAllPromotions()
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql

            string sql = @"
                SELECT 
                    promo.idPromotion, 
                    promo.libelle,
                    promo.dateDebut,
                    promo.dateFin,
                    promo.reduction
                FROM 
                    promotions promo
            ";

            //Executer la requête sql, donc créer une commande
            //MySqlCommand cmd = new MySqlCommand(sql, cnn);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var listePromotion = new List<PromotionModel>();

            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                var laPromotion = new PromotionModel(
                    Convert.ToInt32(reader["idPromotion"]),
                    reader["libelle"].ToString(),
                    reader["dateDebut"].ToString(),
                    reader["dateFin"].ToString(),
                    Convert.ToInt32(reader["reduction"])
            );

                listePromotion.Add(laPromotion);
            }

            cnn.Close();
            return listePromotion;
        }


        public PromotionModel GetPromotionById(int idPromotion)
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql

            string sql = @"
                SELECT 
                    promo.idPromotion, 
                    promo.libelle,
                    promo.dateDebut,
                    promo.dateFin,
                    promo.reduction
                FROM 
                    promotions promo
                WHERE idPromotion = @idPromotion
                    
                ";

            //Executer la requête sql, donc créer une commande
            //MySqlCommand cmd = new MySqlCommand(sql, cnn);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idPromotion", idPromotion);
            var reader = cmd.ExecuteReader();
            PromotionModel maPromotion = null;

            //Récupérer le retour, et le transformer en objet
            if (reader.Read())
            {
                maPromotion = new PromotionModel()
                {
                    IdPromotion = Convert.ToInt32(reader["idPromotion"]),
                    Libelle = reader["libelle"].ToString(),
                    DateDebut = reader["dateDebut"].ToString(),
                    DateFin = reader["dateFin"].ToString(),
                    Reduction = Convert.ToInt32(reader["reduction"])
                };
            }

            cnn.Close();
            return maPromotion;
        }

        public bool CreatePromotion(PromotionModel Promotion)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                    INSERT INTO promotions
                        (libelle, reduction, dateDebut, dateFin)
                    VALUES
                        (@libellePromotion, @reduction, @dateDebut, @dateFin)";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@libellePromotion", Promotion.Libelle);
                cmd.Parameters.AddWithValue("@reduction", Promotion.Reduction);
                cmd.Parameters.AddWithValue("@dateDebut", Promotion.DateDebut);
                cmd.Parameters.AddWithValue("@dateFin", Promotion.DateFin);

                var reader = cmd.ExecuteNonQuery();


                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool EditPromotion(PromotionModel Promotion)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                UPDATE promotions
                SET 
                    libelle = @libellePromotion,
                    reduction = @Reduction,
                    dateDebut = @DateDebut,
                    dateFin = @DateFin
                WHERE 
                    idPromotion = @idPromotion
                ";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idPromotion", Promotion.IdPromotion);
                cmd.Parameters.AddWithValue("@libellePromotion", Promotion.Libelle);
                cmd.Parameters.AddWithValue("@Reduction", Promotion.Reduction);
                cmd.Parameters.AddWithValue("@DateDebut", Promotion.DateDebut);
                cmd.Parameters.AddWithValue("@DateFin", Promotion.DateFin);

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

        public bool DeletePromotion(int idPromotion)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                    DELETE FROM 
                        promotions
                    WHERE 
                        idPromotion = @idPromotion
                    ";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idPromotion", idPromotion);

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
