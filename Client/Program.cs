using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client is alive....");
           
            NetTcpBinding binding = new NetTcpBinding();
            string adresa = "net.tcp://localhost:4000/IZahtjev";

            IZahtjev factory = new ChannelFactory<IZahtjev>(binding, adresa).CreateChannel();
            factory.GenerisiZahtjev(2, 2, 2, new Alarm(DateTime.Now, "bzv", 2));

            Console.ReadKey();
        }
    }
}
