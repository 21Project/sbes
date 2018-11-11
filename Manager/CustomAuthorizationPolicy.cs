using Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CustomAuthorizationPolicy : IAuthorizationPolicy
    {

        private string id;
        private object locker = new object();

        public CustomAuthorizationPolicy()
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
                WindowsIdentity winIdentitiy = identity as WindowsIdentity;

                if (winIdentitiy != null)
                {
                    principal = new CustomPrincipal(winIdentitiy);
                }
                else
                {
                    X509Certificate2 certificate = CertManager.GetCertificate(identity);
                    if(certificate == null)
                    {
                        Audit.AuthenticationFailed(Formatter.VratiIme(identity.Name), OperationContext.Current.IncomingMessageHeaders.Action, "Authentication failed.");
                    }else
                    {
                        Audit.AuthenticationSuccess(Formatter.VratiIme(identity.Name));
                    }
                    principal = new CustomPrincipal(certificate, identity);
                }

                return principal;
            }
        }




    }
}
