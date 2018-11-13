using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   
    public interface IBazaPodataka
    {
       
        void Upisi(Alarm a, string imeKlijenta);
    }
}
