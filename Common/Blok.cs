using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Blok
    {
        public static Dictionary<int, Vektor> vektori;

        public void NapraviBlok()
        {
            Vektor v = new Vektor();
            v.NapraviVektor();
            vektori = new Dictionary<int, Vektor>();
            for (int i = 0; i < 100; i++)
            {
                vektori.Add(i, new Vektor());
            }
        }

        public Dictionary<int, Vektor> GetVektori
        {
            get { return vektori; }
        }

    }
}
