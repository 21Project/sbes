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
        public bool GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa)
        {
            bool ret = false;
            IIdentity id = Thread.CurrentPrincipal.Identity;
            IPrincipal principal = Thread.CurrentPrincipal;

            WindowsIdentity wId = id as WindowsIdentity;


            if (principal.IsInRole("Trazi"))
            {

                Console.WriteLine(String.Format("Authentificated User"));
                Console.WriteLine(brojBloka.ToString() + " " + brojVektora.ToString() + " " + brojElementa.ToString());
                Dictionary<int, Blok> blok = InterniModel.blokovi;
                Blok b = blok[brojBloka];
                Dictionary<int, Vektor> vektor = b.GetVektori;
                Vektor v = vektor[brojVektora];
                Dictionary<int, Alarm> alarm = v.GetElementi;
                Alarm a = alarm[brojElementa];
                if (a == null)
                {
                    Console.WriteLine("Alarm je null.");
                }
                else
                {
                    Console.WriteLine("Alarm: {0}, {1}, {2}", a.PorukaOAlarmu, a.VrijemeGenerisanjaAlarma, a.Rizik);
                    BazaPodataka baza = new BazaPodataka();
                    if (wId == null)
                    {
                        baza.Upisi(a, (principal as CustomPrincipal).VratiIme());
                    }
                    else
                    {
                        baza.Upisi(a, wId.Name);
                    }



                }



            }

            else
            {
                MyException ex = new MyException();
                ex.Message = "No permision for TRAZI";
                throw new FaultException<MyException>(ex);

            }


            Console.WriteLine("Ovo je test metoda !!!!!");
            return ret;
        }
    }
}
