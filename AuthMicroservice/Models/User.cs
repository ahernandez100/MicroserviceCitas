﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthMicroservice.Models.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}