using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string adresa = "net.tcp://localhost:4000/IZahtjev";
            ServiceHost svc = new ServiceHost(typeof(Zahtjev));

            svc.AddServiceEndpoint(typeof(IZahtjev), binding, adresa);

            svc.Open();

            Console.ReadKey();

            svc.Close();
        }
    }
}
