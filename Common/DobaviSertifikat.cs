using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DobaviSertifikat
    {
        public static X509Certificate2 GetCertificate(IIdentity identity)
        {
            try
            {
                // X509Identity is an internal class, so we cannot directly access it
                Type x509IdentityType = identity.GetType();

                // The certificate is stored inside a private field of this class
                FieldInfo certificateField = x509IdentityType.GetField("certificate", BindingFlags.Instance | BindingFlags.NonPublic);

                return (X509Certificate2)certificateField.GetValue(identity);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
