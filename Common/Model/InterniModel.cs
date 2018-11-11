using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class InterniModel
    {
        public static Dictionary<int, Blok> blokovi;

        public void NapraviInterniModel()
        {
            Blok b = new Blok();
            b.NapraviBlok();
            blokovi = new Dictionary<int, Blok>();
            for (int i = 0; i < VelicinaModela.BrojBlokova; i++)
            {
                blokovi.Add(i, new Blok());
            }

        }




    }
}
