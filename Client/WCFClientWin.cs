using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	public class WCFClientWin : ChannelFactory<IZahtjev>, IZahtjev, IDisposable
	{
		IZahtjev factory;

		public WCFClientWin(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
		{
			factory = this.CreateChannel();

		}

		public int GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa)
		{
			int ret = -1;
			try
			{
				ret = factory.GenerisiZahtjev(brojBloka, brojVektora, brojElementa);
			}
			catch(FaultException<MyException> ex)
			{
				Console.WriteLine("{0}", ex.Detail.Message);
			}
		
			return ret;
		}

		public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}
	}
}
