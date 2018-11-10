using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.ReadLine();

            RadSaXML ra = new RadSaXML();
          //  ra.NapraviXMLRecenica();

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            string adresa = "net.tcp://localhost:4000/IZahtjev";
            ServiceHost svc = new ServiceHost(typeof(Zahtjev));

            svc.AddServiceEndpoint(typeof(IZahtjev), binding, adresa);

           
            svc.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            svc.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });


            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            svc.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            svc.Authorization.PrincipalPermissionMode = System.ServiceModel.Description.PrincipalPermissionMode.Custom;



            InterniModel interniModel = new InterniModel();
            interniModel.NapraviInterniModel();

            /// srvCertCN.SubjectName should be set to the service's username. .NET WindowsIdentity class provides information about Windows user running the given process
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:9999/Receiver";
            ServiceHost host = new ServiceHost(typeof(Zahtjev));
            host.AddServiceEndpoint(typeof(IZahtjev), binding1, address);

            ///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            /// host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromFile("WCFService.pfx");
            /// 
          //  host.Authorization.ServiceAuthorizationManager = new AuthorizationManagerCert();
            List<IAuthorizationPolicy> policies1 = new List<IAuthorizationPolicy>();
            policies1.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies1.AsReadOnly();
            host.Authorization.PrincipalPermissionMode = System.ServiceModel.Description.PrincipalPermissionMode.Custom;

            //host.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();
            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });


            // host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.CurrentUser, srvCertCN);
            // host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            // host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
            // host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            //try
            //{
            //    host.Open();
            //    svc.Open();
            //    Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
            //    Console.ReadLine();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("[ERROR] {0}", e.Message);
            //    Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            //}
            //finally
            //{
            //    host.Close();
            //    svc.Close();
            //}

            host.Open();
            svc.Open();
            Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
            Console.ReadLine();

            host.Close();
            svc.Close();


            Console.ReadKey();
        }
    }
}
