using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Zahtjev : IZahtjev
    {
        public int GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa)
        {
            int ret = -1;
            IIdentity id = Thread.CurrentPrincipal.Identity;
            IPrincipal principal = Thread.CurrentPrincipal;

            WindowsIdentity wId = id as WindowsIdentity;

            if (principal.IsInRole("Trazi"))
            {
               
                Audit.AuthorizationSuccess(Formatter.VratiIme((principal as CustomPrincipal).Identity.Name), OperationContext.Current.IncomingMessageHeaders.Action);

                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("Klijent je generisao brojeve: "+ brojBloka.ToString() + " " + brojVektora.ToString() + " " + brojElementa.ToString());
                Dictionary<int, Blok> blok = InterniModel.blokovi;
                Blok b = blok[brojBloka];
                Dictionary<int, Vektor> vektor = b.GetVektori;
                Vektor v = vektor[brojVektora];
                Dictionary<int, Alarm> alarm = v.GetElementi;
                Alarm a = alarm[brojElementa];
                if (a == null)
                {
                    Console.WriteLine("Na zadatoj poziciji nema alarma.");
                    Console.WriteLine("-------------------------------------------\n");
                    ret = 0;
                }
                else
                {

                    Console.WriteLine("\n-------------------------------------------");
                    Console.WriteLine("PRONADJENI ALARM: \nPoruka: {0} \nVreme generisanja : {1} \nRizik: {2}", a.PorukaOAlarmu, a.VrijemeGenerisanjaAlarma, a.Rizik);
                    Console.WriteLine("-------------------------------------------\n");
                    new BazaPodataka().Upisi(a, Formatter.VratiIme((principal as CustomPrincipal).Identity.Name));
                    ret = 1;
                }
                
            }

            else
            {
                Audit.AuthorizationFailed(Formatter.VratiIme((principal as CustomPrincipal).Identity.Name), OperationContext.Current.IncomingMessageHeaders.Action, "Authorization failed.");

                MyException ex = new MyException();
                ex.Message = "Nemate pravo 'Trazi'!";
                throw new FaultException<MyException>(ex);

            }
            
            return ret;
        }
    }
}
