using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAPI.DAOs;

namespace MyAPI.DAL
{
    public class TransplayContext: DbContext
    {
        public TransplayContext() : base("TransplayContext")
        {
            Words.RemoveRange(Words);

            Words.AddRange(Models.RutTrich.rutTrich());

            this.SaveChanges();

            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vocabulary> Vocabularys { get; set; }
        public DbSet<Word> Words { get; set; }

    }
}
