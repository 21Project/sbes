using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class InterniModel
    {
        Dictionary<int, Blok> blokovi;

        public InterniModel()
        {
            blokovi = new Dictionary<int, Blok>();
            for (int i = 0; i < 100; i++)
            {
                blokovi.Add(i, new Blok());
            }

        }




    }
}
