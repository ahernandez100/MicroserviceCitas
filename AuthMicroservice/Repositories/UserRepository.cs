using AuthMicroservice.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AuthMicroservice.Repositories
{
    public class UserRepository
    {
        private readonly string _filePath;

        public UserRepository()
        {
            _filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "App_Data", "userToken.json");
        }

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public User GetUserByUsername(string username)
        {
            return GetAllUsers().FirstOrDefault(u => u.Username == username);
        }
    }
}