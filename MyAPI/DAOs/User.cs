using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAPI.DAOs
{
    public class User
    {
        public User()
        {
            Friends = new List<User>();
            Vocabularys = new List<Vocabulary>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Scores { get; set; }
        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<Vocabulary> Vocabularys { get; set; }
    }
}