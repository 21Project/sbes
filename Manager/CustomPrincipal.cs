using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    //Enkapsulira podatke o ovlascenjima autentifikovanog korisnika
    //Metoda IsInRole proverava da li clanovi grupe(role) imaju ovlascenja za izvrsavanje
    //, IDisposable

    public class CustomPrincipal : IPrincipal
    {
        //private WindowsIdentity identity = null;
        private IIdentity identity;

        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }

        // private IIdentity identitet = null;
        //public IIdentity Identitet
        //{
        //    get
        //    {
        //        return this.identitet;
        //    }
        //}

        private Dictionary<string, string[]> roles = new Dictionary<string, string[]>();
        RadSaXML r = new RadSaXML();

        public CustomPrincipal(WindowsIdentity winIdentity)
        {
            this.identity = winIdentity;

            foreach (IdentityReference group in winIdentity.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string groupName = Formatter.ParseName(name.ToString());

                List<GrupaPermisija> lista = r.CitajIzXML();
                //string[] permisije = new string[] { "permisija1" };
                foreach (GrupaPermisija g in lista)
                {
                    if (g.NazivGrupe == groupName)
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

        public CustomPrincipal(X509Certificate2 clientCert, IIdentity id)
        {
            this.identity = id;


            string group = null;
            if (!clientCert.SubjectName.Name.Contains(','))
            {
                MyException e = new MyException();
                e.Message = "Nemate pravo da izvrsite operaciju.";
                throw new FaultException<MyException>(e);
            }

            string[] ss = clientCert.SubjectName.Name.Split(',');
            string[] s = ss[1].Split('=');
            if (s[0].Trim() == "OU")
            {
                group = s[1].Trim();
            }

            List<GrupaPermisija> lista = r.CitajIzXML();

            foreach (GrupaPermisija g in lista)
            {
                if (g.NazivGrupe == group)
                {
                    roles.Add(group, g.Permisije.ToArray());
                }
            }


        }


       

        //public void Dispose()
        //{
        //    if (identity != null)
        //    {
        //        this.identity.Dispose();
        //        identity = null;
        //    }
        //}

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

        //public string VratiIme()
        //{
        //    string[] ss = identitet.Name.Split(',');
        //    string[] s = ss[0].Trim().Split('=');
        //    return s[1].Trim();
        //}
    }
}
