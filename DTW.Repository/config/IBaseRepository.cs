using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.config
{
    public interface IBaseRepository
    {
        public MySqlConnection OpenConnexion();
    }
}
