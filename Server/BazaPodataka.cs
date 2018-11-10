using Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class BazaPodataka : IBazaPodataka
    {
        public void Upisi(Alarm a, string imeKlijenta)
        {
            DateTime x = DateTime.Now;
            string date = x.ToString();
            ElementZaUpis el = new ElementZaUpis(a, DateTime.Now, imeKlijenta);
            RadSaXML r = new RadSaXML();
            r.UpisiUXML(el);
        }
    }
}
