using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Vektor
    {
        private Dictionary<int, Alarm> elementi;
        private Random r = new Random();

        public Vektor()
        {
            elementi = new Dictionary<int, Alarm>();
            int x;
            for (int i = 0; i < 100000; i++)
            {
                if (i % 2 == 0)
                {
                    x = r.Next(1, 10);
                    elementi.Add(i, new Alarm(DateTime.Now, "testPoruka", x));
                }
                else
                {
                    elementi.Add(i, new Alarm());
                }
            }
        }
    }
}
