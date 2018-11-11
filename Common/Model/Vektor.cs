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
     
        static List<string> li = new RadSaXML().CitajPoruke();
       // private List<string> li1 = VratiAlarme();
        public void NapraviVektor()
        {
            elementi = new Dictionary<int, Alarm>();
            int x;
            for (int i = 0; i < VelicinaModela.BrojElemenata; i++)
            {
                if (i % 2 == 0)
                {
                    x = r.Next(1, 10);
                    elementi.Add(i, new Alarm(DateTime.Now, li[x-1], x));
                }
                else
                {
                    elementi.Add(i, null);
                }
            }
        }

        public Dictionary<int, Alarm> GetElementi
        {
            get { return elementi; }
        }

        //public static List<string> VratiAlarme()
        //{
        //    List<string> pomLi = new List<string>();
        //    pomLi.Add("Alarm0");
        //    foreach(string s in li)
        //    {
        //        pomLi.Add(s);
        //    }
        //    return pomLi;
        //}
    }
}
