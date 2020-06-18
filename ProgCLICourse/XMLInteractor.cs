using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProgCLICourse
{
    public static class XMLInteractor
    {        
        public static void save(string path, ShoppingList list)
        {

            switch (ConfigurationManager.AppSettings["howtoXML"])
            {
                case "XML_doc":
                    XML_XML.saveToXML(path, list);
                    break;
                case "XML_LINQ":
                    XML_LINQ.saveToXML(path, list);
                    break;
                case "XML_serialization":
                    XML_serialization.saveToXML(path, list);
                    break;
            }
        }

        public static ShoppingList loadXML(string path)
        {
            switch (ConfigurationManager.AppSettings["howtoXML"])
            {
                case "XML_doc":
                    return XML_XML.loadFromXML(path);
                case "XML_LINQ":
                    return XML_LINQ.loadFromXML(path);
                case "XML_serialization":
                    return XML_serialization.loadFromXML(path);
                default:
                    return null;
            }
        }
    }
}
