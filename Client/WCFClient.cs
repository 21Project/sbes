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
            
            string cltCertCN = Manager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            
            try
            {
                this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
                factory = this.CreateChannel();
            }
            catch (FaultException<MyException> ex)
            {
                Console.WriteLine("{0}", ex.Detail.Message);
            }
            
        }
        
        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public int GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa)
        {
			int ret = -1;
            try
            {
                ret = factory.GenerisiZahtjev(brojBloka, brojVektora, brojElementa);
            }
			catch (FaultException<MyException> ex)
			{
				Console.WriteLine("{0}", ex.Detail.Message);
			}
			
			return ret;
        }
    }
}
