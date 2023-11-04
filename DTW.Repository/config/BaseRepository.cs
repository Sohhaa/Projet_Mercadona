using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.config
{
    public class BaseRepository : IBaseRepository
    {
        public string ConectionString { get; set; }

        public BaseRepository(IConfiguration configuration)
        {
            var builder = new MySqlConnectionStringBuilder(
                configuration.GetConnectionString("DefaultConnection"));
                //builder.Password = configuration["DbPassword"];
            ConectionString = builder.ConnectionString + ";";
        }

        //public MySqlConnection OpenConnexion()
        //{
        //    try
        //    {
        //        MySqlConnection cnn = new MySqlConnection(ConectionString);
        //        cnn.Open();
        //        return cnn;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception("Impossible de se connecter à la base de données"+ex);
        //    }
        //}

        public NpgsqlConnection OpenConnexion()
        {
            try
            {
                NpgsqlConnection cnn = new NpgsqlConnection(ConectionString);
                cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                throw new Exception("Impossible de se connecter à la base de données" + ex);
            }
        }
    }
}
