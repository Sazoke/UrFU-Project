using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectURFU
{
    class Level
    {
        Random rnd = new Random();
        List<char> symbols;
        public Level(string text)
        {
            symbols = new List<char>();
            foreach (var word in text)
                symbols.Add(word);
        }

        public List<char> Words 
        {
            get
            {
                return symbols;
            }
            private set
            {
            }
        }

        public string Text(int words)
        {
            var result = "";
            for (int i = 0; i < words; i++)
            {
                var j = rnd.Next(0, this.symbols.Count);
                result += symbols[j];
            }
            return result;
        }
    }
}
