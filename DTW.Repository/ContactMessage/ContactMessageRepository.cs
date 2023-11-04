using Mercadona.Repository.config;
using Mercadona.Repository.ContactMessage;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Mercadona.Repository.ContactMessage
{
    public class ContactMessageRepository : BaseRepository, IContactMessageRepository
    {
        public ContactMessageRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public bool SaveContactMessage(ContactMessageModel ContactMessage)
        {
            try
            {
                var cnn = OpenConnexion();

                string sql = @"
                    INSERT INTO contactMessages
                        (prenom,nom,email,message)
                    VALUES
                        (@prenom,@nom, @email, @message)";

                //Executer la requête sql, donc créer une commande
                //MySqlCommand cmd = new MySqlCommand(sql, cnn);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@prenom", ContactMessage.FirstName);
                cmd.Parameters.AddWithValue("@nom", ContactMessage.LastName);
                cmd.Parameters.AddWithValue("@email", ContactMessage.Email);
                cmd.Parameters.AddWithValue("@message", ContactMessage.Message);

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
