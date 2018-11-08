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

		public bool GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa, Alarm alarm)
		{
			bool ret = false;
			try
			{
				ret = factory.GenerisiZahtjev(brojBloka, brojVektora, brojElementa, alarm);
			}
			//catch (SecurityAccessDeniedException e)
			//{
			//	Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
			//}
			catch(FaultException<MyException> ex)
			{
				Console.WriteLine("[TestCommunication] ERROR = {0}", ex.Detail.Message);
			}
			//throw new NotImplementedException();
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
