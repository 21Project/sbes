using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class PrincipalCert : IPrincipal
    {
        private IIdentity identity;
        private Dictionary<string, string[]> roles = new Dictionary<string, string[]>();
        RadSaXML r = new RadSaXML();
        public IIdentity Identity
        {
            get
            {
                return identity;
            }
        }
        public bool IsInRole(string role)
        {
            
                bool IsAuthz = false;
                foreach (string[] r in roles.Values)
                {
                    if (r.Contains(role))
                    {
                        IsAuthz = true;
                        break;
                    }
                }

                return IsAuthz;
            
        }

        public PrincipalCert(X509Certificate2 clientCert, IIdentity ident)
        {
            this.identity = ident;

            
            string group = null;

            string[] ss = clientCert.SubjectName.Name.Split(',');
            string[] s = ss[1].Split('=');
            group = s[1];
                

            

            List<GrupaPermisija> lista = r.CitajIzXML();
           
          

            foreach(GrupaPermisija g in lista)
            {
                if(g.NazivGrupe == group)
                {
                    roles.Add(group, g.Permisije.ToArray());
                }
            }

           
        }

    }
}
