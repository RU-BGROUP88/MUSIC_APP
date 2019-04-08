using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MusicType { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }


        public Users(int _userId, string _name, string _familyName, string _gender, double _age,  string _image, string _address,  string _email, string _password, string _musicType)
        {
            UserId = _userId;
            Name = _name;
            FamilyName = _familyName;
            Gender = _gender;
            Age = _age;
            Address = _address;
            Image = _image;
            Email = _email;
            Password = _password;
            MusicType = _musicType;
        }

        public int Insert()
        {
            NewDBservices dbs = new NewDBservices();
            int numAffected = dbs.Insert(this);
            return numAffected;
        }

        public Users()
        {

        }


        public Users Login(string Email, string Password)
        {
            NewDBservices dbs = new NewDBservices();
            return dbs.Login(Email, Password, "ConnectionString", "Users");

        }

        public int SaveChange(Users user)
        {
            NewDBservices dbs = new NewDBservices();
            int numAffected = dbs.SaveChange(user);

            return numAffected;
        }
    }
}