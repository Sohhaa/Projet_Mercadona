﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTW.Repository.User
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(int idUser, string userForeName, string userSurName, string userEmail)
        {
            IdUser = idUser;
            UserForeName = userForeName;
            UserSurName = userSurName;
            UserEmail = userEmail;
        }

        public int IdUser { get; set; }
        public string UserForeName { get; set; }
        public string UserSurName { get; set; }
        public string UserEmail { get; set; }

    }
}
