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
        void GenerisiZahtjev(int brojBloka, int brojVektora, int brojElementa, Alarm alarm);
    }
}