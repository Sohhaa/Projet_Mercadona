using Mercadona.Repository.config;
using Mercadona.Repository.Produits;
using Mercadona.Repository.User;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public List<UserModel> GetAllUsers()
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql
            
            string sql = @"
                SELECT 
                    u.idUser,
                    u.firstName,
                    u.lastName,
                    u.email
                FROM 
                    users u
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var LstUsers = new List<UserModel>();

            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                UserModel User = new UserModel()
                {
                    IdUser = Convert.ToInt32(reader["idUser"]),
                    FirstName = reader["firstName"].ToString(),
                    LastName = reader["lastName"].ToString(),
                    Email = reader["email"].ToString()
                };

                LstUsers.Add(User);
            }

            cnn.Close();
            return LstUsers;
        }

        public UserModel GetUserByEmail(string Email)
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql

            string sql = @"
                    SELECT 
                        idUser,
                        firstName,
                        lastName,
                        email
                    FROM 
                        users
                    WHERE 
                        email = @email
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@email", Email);
            var reader = cmd.ExecuteReader();
            UserModel User = null;


            if (reader.Read())
            {
                User = new UserModel()
                {
                    IdUser = Convert.ToInt32(reader["idUser"]),
                    FirstName = reader["firstName"].ToString(),
                    LastName = reader["lastName"].ToString(),
                    Email = reader["email"].ToString()
                };
                cnn.Close();
                return User;
            }
            else 
            {
                cnn.Close();
                return null;
            }       
        }

        public UserModel GetUserById(int userId)
        {
            //je me connecte à la bdd
            var cnn = OpenConnexion();
            //Je crée une requête sql

            string sql = @"
                    SELECT 
                        idUser,
                        firstName,
                        lastName,
                        email
                    FROM 
                        users
                    WHERE 
                        idUser = @userId
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@userId", userId);
            var reader = cmd.ExecuteReader();
            UserModel User = null;


            if (reader.Read())
            {
                User = new UserModel()
                {
                    IdUser = Convert.ToInt32(reader["idUser"]),
                    FirstName = reader["firstName"].ToString(),
                    LastName = reader["lastName"].ToString(),
                    Email = reader["email"].ToString()
                };
                cnn.Close();
                return User;
            }
            else
            {
                cnn.Close();
                return null;
            }
        }

        public bool EditUser(UserModel user)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                UPDATE users
                SET 
                     firstName = @firstName,
                     lastName = @lastName,
                     email = @email
                WHERE 
                    idUser = @idUser
                ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idUser", user.IdUser);
                cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                cmd.Parameters.AddWithValue("@lastName", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);

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

        public bool DeleteUser(int idUser)
        {
            try
            {
                //je me connecte à la bdd
                var cnn = OpenConnexion();
                //Je crée une requête sql

                string sql = @"
                    DELETE FROM 
                        users
                    WHERE 
                        idUser = @idUser
                    ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idUser", idUser);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CreateUser(UserModel User)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                    INSERT INTO users
                        (firstname, lastname, email, password, salt)
                    VALUES
                        (@firstname, @lastname, @email, @password, @salt)";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@firstname", User.FirstName);
                cmd.Parameters.AddWithValue("@lastname", User.LastName);
                cmd.Parameters.AddWithValue("@email", User.Email);
                cmd.Parameters.AddWithValue("@password", User.Password);
                cmd.Parameters.AddWithValue("@salt", User.Salt);

              
                var reader = cmd.ExecuteNonQuery();


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
