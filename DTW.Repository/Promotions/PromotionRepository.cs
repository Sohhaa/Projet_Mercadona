using Mercadona.Repository.config;
using Mercadona.Repository.Promotion;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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

        public List<PromotionModel> GetAllPromotion()
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
                    promo.redution,
                    
                   
                FROM 
                    promotions promo
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
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


        public PromotionModel GetPromotionById(int id)
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
                    promo.redution,
                    
                   
                FROM 
                    promotions promo
                WHERE idPromotion = @idPromotion
                    
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idPromotion", id);
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



    }
}
