using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   
    public class Alarm
    {
        public Alarm()
        {
        }

        public Alarm(DateTime vrijemeGenerisanjaAlarma, string porukaOAlarmu, int rizik)
        {
            VrijemeGenerisanjaAlarma = vrijemeGenerisanjaAlarma;
            PorukaOAlarmu = porukaOAlarmu;
            Rizik = rizik;
        }

      
        public DateTime VrijemeGenerisanjaAlarma { get; set; }
      
        public string PorukaOAlarmu { get; set; }
   
        public int Rizik { get; set; }
    }
}
