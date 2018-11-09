using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Vektor
    {
        public static Dictionary<int, Alarm> elementi;
        private Random r = new Random();

        public void NapraviVektor()
        {
            elementi = new Dictionary<int, Alarm>();
            int x;
            for (int i = 0; i < 10000; i++)
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

        public Dictionary<int, Alarm> GetElementi
        {
            get { return elementi; }
        }
    }
}
