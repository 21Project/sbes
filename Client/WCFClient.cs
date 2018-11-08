using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Manager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace Client
{
    public class WCFClient : ChannelFactory<IZahtjev>, IZahtjev, IDisposable
    {
        IZahtjev factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            string cltCertCN = Manager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

       

        

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public bool GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa, Alarm alarm)
        {
			bool ret = false;
            try
            {
                 ret = factory.GenerisiZahtjev(brojBloka, brojVektora, brojElementa,alarm);
            }
			catch (FaultException<MyException> ex)
			{
				Console.WriteLine("[TestCommunication] ERROR = {0}", ex.Detail.Message);
			}
			//catch (SecurityAccessDeniedException e)
			//{
			//    Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
			//}

			return ret;
        }
    }
}
