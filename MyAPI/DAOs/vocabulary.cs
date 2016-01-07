using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.DAOs
{
    public class Vocabulary
    {
        public Vocabulary()
        {
            Words = new List<Word>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Word> Words { get; set; }

    }
}
