using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public class Vocabulary:IndentityObject
    {
        public Vocabulary()
        { }

        public Vocabulary(string name):this(name, null)
        {
        }
        public Vocabulary(string name, List<Word> Words)
        { this.Name = name;
            this.Words = Words;
        }
        public List<Word> Words
        {
            get; set;
        }

        public bool Create()
        {
            var model = Mapper.Map<Vocabulary, DAOs.Vocabulary>(this);

            var result = GLOBAL.db.Vocabularys.Add(model);
            GLOBAL.db.SaveChanges();
            GLOBAL.db.Entry(model).State = EntityState.Detached;

            this.ID = result.ID;

            return true;
        }

        public List<Word> GetWords()
        {
            return Words;
        }

        public void AddWord(Word word)
        {
            
            Words.Add(word);

        }
    }
}
