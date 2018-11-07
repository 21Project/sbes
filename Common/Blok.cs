using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Blok
    {
        private Dictionary<int, Vektor> vektori;

        public Blok()
        {
            vektori = new Dictionary<int, Vektor>();
            for (int i = 0; i < 1000; i++)
            {
                vektori.Add(i, new Vektor());
            }
        }



    }
}
