﻿using Mercadona.Repository.config;
using Mercadona.Repository.User;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Links
{
    public class LinkRepository : BaseRepository, ILinkRepository
    {
        public LinkRepository(IConfiguration configuration): base (configuration)
        {
        }

        public List<LinkModel> GetAllLinks()
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql
            
            string sql = @"
                SELECT 
                    l.idLinks, 
                    l.Title,
                    l.Description, 
                    l.Link,
                    u.idUser,
                    u.forename, 
                    u.surname,
                    u.mail
                FROM 
                    links l
                INNER JOIN users u ON l.idAuteur = u.idUser
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<LinkModel>();

            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                UserModel auteur = new UserModel()
                {
                    IdUser = Convert.ToInt32(reader["idUser"]),
                    UserForeName= reader["forename"].ToString(),
                    UserSurName=reader["surname"].ToString(),
                    UserEmail=reader["mail"].ToString()
                };

                var leLien = new LinkModel(
                    Convert.ToInt32(reader["idLinks"]),
                    reader["Title"].ToString(),
                    reader["Description"].ToString(),
                    reader["Link"].ToString(),
                    auteur
                );

                maListe.Add(leLien);
            }

            cnn.Close();
            return maListe;
        }

        public LinkModel GetLink(int id)
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql

            string sql = @"
                SELECT 
                    l.idLinks, 
                    l.Title,
                    l.Description, 
                    l.Link,
                    u.idUser,
                    u.forename, 
                    u.surname,
                    u.mail
                FROM 
                    links l
                INNER JOIN users u ON l.idAuteur = u.idUser
                WHERE l.idLinks = @idLink
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idLink", id);

            var reader = cmd.ExecuteReader();
            LinkModel monLien = null;
            //Récupérer le retour, et le transformer en objet
            if (reader.Read())
            {
                UserModel auteur = 
                   new UserModel()
                   {
                       IdUser = Convert.ToInt32(reader["idUser"]),
                       UserForeName = reader["forename"].ToString(),
                       UserSurName = reader["surname"].ToString(),
                       UserEmail = reader["mail"].ToString()
                   };

                monLien = new LinkModel(
                    Convert.ToInt32(reader["idLinks"]),
                    reader["Title"].ToString(),
                    reader["Description"].ToString(),
                    reader["Link"].ToString(),
                    auteur
                );
            }

            cnn.Close();
            return monLien;
        }

        public bool EditLink(LinkModel link)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                UPDATE links
                SET 
                    Title = @title,
                    Description = @description,
                    Link = @link,
                    idAuteur = @idUser
                WHERE 
                    IdLinks = @idLink
                ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@title", link.Title);
                cmd.Parameters.AddWithValue("@description", link.Description);
                cmd.Parameters.AddWithValue("@link", link.URL);
                cmd.Parameters.AddWithValue("@idUser", link.Auteur.IdUser);
                cmd.Parameters.AddWithValue("@idLink", link.IdLink);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool DeleteLink(int id)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                    DELETE FROM 
                        links
                    WHERE 
                        IdLinks = @idLink
                    ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idLink", id);

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
