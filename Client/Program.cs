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
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            string ip = "192.168.137.12";
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
                
                string adresa = "net.tcp://" + ip + ":4000/IZahtjev";
                NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

                try
                {

                    using (WCFClientWin proxy = new WCFClientWin(binding, new EndpointAddress(new Uri(adresa))))
                    {
                        try
                        {
                            GenerisanjeIndeksa g = new GenerisanjeIndeksa();
                            List<int> lista = g.GenerisiIndekse();

                            int ret = proxy.GenerisiZahtjev(lista[0], lista[1], lista[2]);
                            if (ret == 1)
                            {
                                Console.WriteLine("Alarm na poziciji {0},{1},{2} je pronadjen!", lista[0], lista[1], lista[2]);
                            }
                            else if(ret == 0)
                            {
                                Console.WriteLine("Alarm na poziciji {0},{1},{2} nije pronadjen!", lista[0], lista[1], lista[2]);
                            }
                        }
                        catch (FaultException<MyException> ex)
                        {
                            Console.WriteLine("{0}", ex.Detail.Message);
                        }
                        catch (SecurityNegotiationException)
                        {
                            Console.WriteLine("Niste autentifikovani!");
                        }
                    }
                }
                catch (CommunicationException)
                {
                   
                }
			}
            else
            {
                string srvCertCN = "wcfservice";

                NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
              
                X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
                EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://" + ip + ":9999/IZahtjev"),
                                          new X509CertificateEndpointIdentity(srvCert));

                
                try
                {
                    using (WCFClientCert proxy = new WCFClientCert(binding, address1))
                    {
                        GenerisanjeIndeksa g = new GenerisanjeIndeksa();
                        List<int> lista = g.GenerisiIndekse();

                        int ret = proxy.GenerisiZahtjev(lista[0], lista[1], lista[2]);
                        if (ret == 1)
                        {
                            Console.WriteLine("Alarm na poziciji {0},{1},{2} je pronadjen!", lista[0], lista[1], lista[2]);
                        }
                        else if (ret == 0)
                        {
                            Console.WriteLine("Alarm na poziciji {0},{1},{2} nije pronadjen!", lista[0], lista[1], lista[2]);
                        }

                    }
                }
				catch(FaultException<MyException> ex)
				{
					Console.WriteLine(ex.Detail.Message);
				}
                catch (NullReferenceException)
                {
                    Console.WriteLine("Neuspela komunikacija!");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Invalid operation: Nemate sertifikat");
                }
                catch (CommunicationObjectFaultedException)
                {
                    Console.WriteLine("Nemate sertifikat!");
                }
            }
          
            Console.ReadKey();
        }
    }
}
