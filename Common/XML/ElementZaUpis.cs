using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    [XmlRoot("BP")]
   
    public class ElementZaUpis
    {
      
        public Alarm Alarm { get; set; }
      
        public DateTime VremeUpisa { get; set; }
     
        public string NazivKlijenta { get; set; }

        public ElementZaUpis() { }
        public ElementZaUpis(Alarm a, DateTime d, string s)
        {
            Alarm = a;
            VremeUpisa = d;
            NazivKlijenta = s;
        }

    }
}
