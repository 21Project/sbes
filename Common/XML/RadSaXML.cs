using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    public class RadSaXML
    {
        public void NapraviXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<GrupaPermisija>), new XmlRootAttribute("GP"));
            if (!File.Exists(@"../../../GrupeIPermisije.xml"))
            {
                List<GrupaPermisija> lista = new List<GrupaPermisija>()
                {
                   new GrupaPermisija("Grupa1",new List<string>()),
                   new GrupaPermisija("Grupa2",new List<string>(){"Trazi","Modifikuj"}),
                   new GrupaPermisija("Grupa3",new List<string>(){"Modifikuj"}),
                   new GrupaPermisija("Grupa4",new List<string>()),
                   new GrupaPermisija("Grupa5",new List<string>()),
                   new GrupaPermisija("Grupa6",new List<string>(){"Trazi","Modifikuj"}),
                   new GrupaPermisija("Grupa7",new List<string>(){"Modifikuj"}),
                   new GrupaPermisija("Grupa8",new List<string>()),
                   new GrupaPermisija("Grupa9",new List<string>(){"Trazi","Modifikuj"}),
                   new GrupaPermisija("Grupa10",new List<string>(){"Trazi"})
                };

                using (TextWriter write = new StreamWriter(@"../../../GrupeIPermisije.xml"))
                {
                    xml.Serialize(write, lista);
                }
            }
        }

        public List<GrupaPermisija> CitajIzXML()
        {
            try
            {
                List<GrupaPermisija> retVal = new List<GrupaPermisija>();
                XmlSerializer desrializer = new XmlSerializer(typeof(List<GrupaPermisija>), new XmlRootAttribute("GP"));

                using (TextReader reader = new StreamReader(@"../../../GrupeIPermisije.xml"))
                {
                    object obj = desrializer.Deserialize(reader);
                    retVal = (List<GrupaPermisija>)obj;
                }
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }


        public void UpisiUXML(ElementZaUpis el)
        {
            List<ElementZaUpis> lista = new List<ElementZaUpis>();
            XmlSerializer xml = new XmlSerializer(typeof(List<ElementZaUpis>), new XmlRootAttribute("BP"));

            if (File.Exists(@"../../../BazaPodataka.xml"))
            {

                lista = Citaj();
                lista.Add(el);
                using (TextWriter write = new StreamWriter(@"../../../BazaPodataka.xml"))
                {
                    xml.Serialize(write, lista);
                }
            }
            else
            {
                lista.Add(el);
                using (TextWriter write = new StreamWriter(@"../../../BazaPodataka.xml"))
                {
                    xml.Serialize(write, lista);
                }
            }

        }

        public List<ElementZaUpis> Citaj()
        {
            try
            {
                List<ElementZaUpis> retVal = new List<ElementZaUpis>();
                XmlSerializer desrializer = new XmlSerializer(typeof(List<ElementZaUpis>), new XmlRootAttribute("BP"));

                using (TextReader reader = new StreamReader(@"../../../BazaPodataka.xml"))
                {
                    object obj = desrializer.Deserialize(reader);
                    retVal = (List<ElementZaUpis>)obj;
                }
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public void NapraviXMLRecenica()
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(List<string>), new XmlRootAttribute("Poruke"));
            if (!File.Exists(@"../../../PorukeAlarmi.xml"))
            {
                List<string> lista = new List<string>();
                lista.Add("Alarm 1");
                lista.Add("Alarm 2");
                lista.Add("Alarm 3");
                lista.Add("Alarm 4");
                lista.Add("Alarm 5");
                lista.Add("Alarm 6");
                lista.Add("Alarm 7");
                lista.Add("Alarm 8");
                lista.Add("Alarm 9");
                lista.Add("Alarm 10");


                using (TextWriter write = new StreamWriter(@"../../../PorukeAlarmi.xml"))
                {
                    xml.Serialize(write, lista);
                }
            }
        }

        public List<string> CitajPoruke()
        {
            try
            {
                List<string> retVal = new List<string>();
                XmlSerializer desrializer = new XmlSerializer(typeof(List<string>), new XmlRootAttribute("Poruke"));

                using (TextReader reader = new StreamReader(@"../../../PorukeAlarmi.xml"))
                {
                    object obj = desrializer.Deserialize(reader);
                    retVal = (List<string>)obj;
                }
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
