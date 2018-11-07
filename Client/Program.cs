using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client is alive....");
            int unos;
            do
            {
                Console.WriteLine("Odaberite tip autentifikacije:");
                Console.WriteLine("1. Windows");
                Console.WriteLine("2. Sertifikat");
                Console.Write(">>");
                unos = Int32.Parse(Console.ReadLine());
            } while (unos > 2 || unos < 1);


            if(unos == 1)
            {
                //10.1.212.171
                string adresa = "net.tcp://localhost:4000/IZahtjev";
                NetTcpBinding binding = new NetTcpBinding();

                IZahtjev factory = new ChannelFactory<IZahtjev>(binding, adresa).CreateChannel();
                factory.GenerisiZahtjev(2, 2, 2, new Alarm(DateTime.Now, "bzv", 2));
            }
            else
            {
                string srvCertCN = "wcfservice";

                //TrustedPeople

                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
                /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
                X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
                EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
                                          new X509CertificateEndpointIdentity(srvCert));


                using (WCFClient proxy = new WCFClient(binding, address1))
                {
                    
                    proxy.GenerisiZahtjev(2, 2, 2, new Alarm());
                    Console.WriteLine("TestCommunication() finished. Press <enter> to continue ...");
                    Console.ReadLine();
                }

            }
            /// Define the expected service certificate. It is required to establish cmmunication using certificates.


            //string adresa = "net.tcp://localhost:4000/IZahtjev";



            //IZahtjev factory = new ChannelFactory<IZahtjev>(binding, adresa).CreateChannel();
            //factory.GenerisiZahtjev(2, 2, 2, new Alarm(DateTime.Now, "bzv", 2));

            Console.ReadKey();
        }
    }
}
