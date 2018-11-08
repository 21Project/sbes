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
    //Enkapsulira podatke o ovlascenjima autentifikovanog korisnika
    //Metoda IsInRole proverava da li clanovi grupe(role) imaju ovlascenja za izvrsavanje
    public class CustomPrincipal : IPrincipal, IDisposable
    {
        private WindowsIdentity identity = null;

        private IIdentity ident;
        public IIdentity Ident
        {
            get
            {
                return ident;
            }
        }

        private Dictionary<string, string[]> roles = new Dictionary<string, string[]>();
		RadSaXML r = new RadSaXML();
        public CustomPrincipal(WindowsIdentity winIdentity)
        {
            this.identity = winIdentity;

            foreach (IdentityReference group in this.identity.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string groupName = Formatter.ParseName(name.ToString());

				List<GrupaPermisija> lista = r.CitajIzXML();
                //string[] permisije = new string[] { "permisija1" };
				foreach(GrupaPermisija g in lista)
				{
					if(g.NazivGrupe == groupName)
					{ 
						//if(g.Permisije.Count == 0)
						//{

						//}
						if (!roles.ContainsKey(groupName))
						{
							roles.Add(groupName, g.Permisije.ToArray());
							break;
						}
					}
				}

                //if (!roles.ContainsKey(groupName))
                //{
                //    roles.Add(groupName, permisije);
                //}
            }
        }


        //public CustomPrincipal(X509Certificate2 clientCert, IIdentity ident)
        //{
        //    this.ident = ident;

        //    string organization = null;
        //    string group = null;

        //    string[] nameParts = clientCert.SubjectName.Name.Split(',');
        //    foreach (var pp in nameParts)
        //    {
        //        string[] keyVal = pp.Trim().Split('=');
        //        if (keyVal[0] == "O")
        //        {
        //            organization = keyVal[1];
        //        }
        //        else if (keyVal[0] == "OU")
        //        {
        //            group = keyVal[1];
        //        }

        //    }

        //    //string finalGroupName = organization == null ? group : organization + "\\" + group;
        //    string finalGroupName = group;

        //    //try
        //    //{
        //    //    roles.UnionWith(RBACManager.GetInstance().GetPermsForGroup(finalGroupName));
        //    //}
        //    //catch (Exception)
        //    //{
        //    //}
        //}



        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }

        public void Dispose()
        {
            if (identity != null)
            {
                identity.Dispose();
                identity = null;
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
    }
}
