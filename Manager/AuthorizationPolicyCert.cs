using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class AuthorizationPolicyCert : IAuthorizationPolicy
    {
        private string id;
        private object locker = new object();

        public AuthorizationPolicyCert()
        {
            this.id = Guid.NewGuid().ToString();
        }

        public string Id
        {
            get
            {
                return this.id;
            }
        }

        public ClaimSet Issuer
        {
            get
            {
                return ClaimSet.System;
            }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            object list;

            if (!evaluationContext.Properties.TryGetValue("Identities", out list))
            {
                return false;
            }

            IList<IIdentity> identities = list as IList<IIdentity>;
            if (list == null || identities.Count <= 0)
            {
                return false;
            }

            evaluationContext.Properties["Principal"] = GetPrincipal(identities[0]);
            return true;
        }

        protected virtual IPrincipal GetPrincipal(IIdentity identity)
        {
            lock (locker)
            {
                IPrincipal principal = null;
                X509Certificate2 cert = GetCertificate(identity);
                //WindowsIdentity winIdentitiy = identity as WindowsIdentity;

                //if (winIdentitiy != null)
                //{
                //    principal = new CustomPrincipal(winIdentitiy);
                //}
                principal = new PrincipalCert(cert, identity);
                return principal;
            }
        }

        private X509Certificate2 GetCertificate(IIdentity identity)
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
