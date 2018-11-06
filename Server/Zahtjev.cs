using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Zahtjev : IZahtjev
    {
        public void GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa, Alarm alarm)
        {
            IIdentity id = Thread.CurrentPrincipal.Identity;

            WindowsIdentity wId = id as WindowsIdentity;

            Console.WriteLine(String.Format("Authentificated User: {0}", wId.Name.ToString()));

            Console.WriteLine("Ovo je test metoda !!!!!");   
        }
    }
}
