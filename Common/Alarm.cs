using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
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

        [DataMember]
        public DateTime VrijemeGenerisanjaAlarma { get; set; }
        [DataMember]
        public string PorukaOAlarmu { get; set; }
        [DataMember]
        public int Rizik { get; set; }
    }
}
