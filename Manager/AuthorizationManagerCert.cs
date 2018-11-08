using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class AuthorizationManagerCert : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            bool authorized = true;

            IPrincipal principal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as IPrincipal;

            if (principal != null)
            {
               // authorized = (principal as CustomPrincipal).IsInRole("Pristupi");
                //if (!authorized)
                //{

                //	throw new SecurityException("No permision for Trazi");
                //}

            }

            return authorized;
        }
    }
}
