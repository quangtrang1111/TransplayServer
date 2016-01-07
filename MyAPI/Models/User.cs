using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public class User: IndentityObject
    {
        public string Email { get; set; }
        public int Scores { get; set; }
        public List<User> Friends { get; set; }
        public List<Vocabulary> Vocabularys { get; set; }

        public User()
        { }
        public User(string email) : this(email, "", 0) { }
        public User(string email, string name) : this(email, name, 0) { }
        public User(string email, string name, int Scores)
        {
            this.Email = email;
            if (name.CompareTo("") != 0)
            {
                this.Name = name;
            }
            else
            {
                GetUserName(Email);

            }
            if (Scores <= 0)
            {
                this.Scores = GetScores(this.Email);
            }
            else
            {
                this.Scores = Scores;
            }
        }
        public bool Create()
        {
            var model = Mapper.Map<User, DAOs.User>(this);

            var result = GLOBAL.db.Users.Add(model);
            GLOBAL.db.SaveChanges();
            GLOBAL.db.Entry(model).State = EntityState.Detached;

            this.ID = result.ID;

            return true;
        }

        public List<Models.User> GetFriends()
        {
            List<Models.User> rs = new List<User>();

            DAOs.User user = GLOBAL.db.Users.Single(x => x.Email == this.Email);

            var model = Mapper.Map<DAOs.User, Models.User>(user);
            rs = model.Friends;
            return rs;
        }
        public User GetFriend(string email)
        {
            User rs = null;
            DAOs.User user = GLOBAL.db.Users.Single(x => x.Email == email);

            if (user != null)
            {
                var model = Mapper.Map<DAOs.User, Models.User>(user);
                return model;
            }
            return rs;
        }
        public int GetScores(string email)
        {
            int rs = 0;
            DAOs.User user = GLOBAL.db.Users.Single(x => x.Email == email);

            if (user != null)
            {
                return user.Scores;
            }
            return rs;
        }

        public string GetUserName(string email)
        {
            string rs = "";

            DAOs.User user = GLOBAL.db.Users.Single(x => x.Email == email);

            if(user != null)
            {
                return user.Name;
            }
            return rs;
        }
    }
}
