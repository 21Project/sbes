using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GenerisanjeIndeksa
    {
        public GenerisanjeIndeksa()
        {
        }

        public List<int> GenerisiIndekse()
        {
            List<int> retVal = new List<int>();
            Random r = new Random();
            retVal.Add(r.Next(0, VelicinaModela.BrojBlokova));
            retVal.Add(r.Next(0, VelicinaModela.BrojVektora));
            retVal.Add(r.Next(0, VelicinaModela.BrojElemenata));

            return retVal;

        }
    }
}
