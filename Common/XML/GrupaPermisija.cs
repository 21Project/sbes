﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
	[XmlRoot("GP")]
	
	public class GrupaPermisija
	{
		public string NazivGrupe { get; set; }
		
		public List<string> Permisije = new List<string>();

		public GrupaPermisija() { }
        public GrupaPermisija(string naziv, List<string> permisije)
        {
            NazivGrupe = naziv;
            Permisije = permisije;
        }

    }
}
