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
        public bool GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa, Alarm alarm)
        {
			bool ret = false;
            IIdentity id = Thread.CurrentPrincipal.Identity;
			IPrincipal principal = Thread.CurrentPrincipal;

			WindowsIdentity wId = id as WindowsIdentity;

            if (wId != null)//.Name.ToString() != "")
            {
				if (principal.IsInRole("Trazi"))
				{
					Console.WriteLine(String.Format("Authentificated User: {0}", wId.Name.ToString()));
				}
				else
				{
					MyException ex = new MyException();
					ex.Message = "No permision for TRAZI";
					throw new FaultException<MyException>(ex);
					
				}

			}
            else
            {

                //  X509Certificate2 cer = CertManager.GetCertificate(id);

                //	string name = CertManager.GetGroupCrt(cer);
                //if (CertManager.IsInRoleCer(name))
                //{ 
                if (principal.IsInRole("Trazi"))
                {
                    Console.WriteLine("Klijen identifikovan preko sertifikata...");
                }
                else
                {
                    MyException ex = new MyException();
                    ex.Message = "No permision for TRAZI";
                    throw new FaultException<MyException>(ex);

                }
                //	}
                //else
                //{
                //	MyException ex = new MyException();
                //	ex.Message = "No permision for TRAZI";
                //	throw new FaultException<MyException>(ex);

                //}

            }

            Console.WriteLine("Ovo je test metoda !!!!!");
			return ret;
        }
    }
}
