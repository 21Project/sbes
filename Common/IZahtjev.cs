﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IZahtjev
    {
        [OperationContract]
		[FaultContract(typeof(MyException))]
        int GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa);
    }
}
