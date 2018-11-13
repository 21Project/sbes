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
            RadSaXML ra = new RadSaXML();
            ra.NapraviXMLRecenica();
            ra.NapraviXML();

            InterniModel interniModel = new InterniModel();
            interniModel.NapraviInterniModel();

            NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Mode = SecurityMode.Transport; 
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


            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            NetTcpBinding binding1 = new NetTcpBinding();
			binding1.Security.Mode = SecurityMode.Transport;
			binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:9999/IZahtjev";
            ServiceHost host = new ServiceHost(typeof(Zahtjev));
            host.AddServiceEndpoint(typeof(IZahtjev), binding1, address);

			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
 
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            List<IAuthorizationPolicy> policies1 = new List<IAuthorizationPolicy>();
            policies1.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies1.AsReadOnly();
            host.Authorization.PrincipalPermissionMode = System.ServiceModel.Description.PrincipalPermissionMode.Custom;

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            
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
