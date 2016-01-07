using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public class Word:IndentityObject
    {
        
        public string Means { get; set; }
        public string Picture { get; set; }
        public int PhoBien { get; set; }


        public Word()
        { }
        public Word(string name) : this(name, "", "", 0)
        { }
        public Word(string name, string Means) : this(name, Means, "", 0)
        { }
        public Word(string name, string Means, string Picture):this(name, Means, Picture, 0)
        { }
        public Word(string name, string Means, string Picture, int PhoBien)
        {
            this.Name = name;
            this.Means = Means;
            this.Picture = Picture;
            this.PhoBien = PhoBien;
        }
        public bool Create()
        {
            var model = Mapper.Map<Word, DAOs.Word>(this);

            var result = GLOBAL.db.Words.Add(model);
            GLOBAL.db.SaveChanges();
            GLOBAL.db.Entry(model).State = EntityState.Detached;

            this.ID = result.ID;

            return true;
        }
    }
}
