using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	//[Serializable]
	[DataContract]
	public class MyException
	{
		public MyException()
		{
		}

		//public MyException(string m) : base(m)
		//{
		//	Message = m;
		//}

		[DataMember]
		public string Message { get; set; }
	}
}
