using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgCLICourse
{
    class XML_LINQ
    {

        public static ShoppingList loadFromXML(string path)
        {
            ShoppingList list = new ShoppingList();
            XDocument.Load(path).Descendants("ShoppingList").ToList().First()
                .Descendants("ShoppingNote").ToList().ForEach( note =>
                list.AddPurchase((string)note.Element("Name"), (string)note.Element("Comment"),
                (double)note.Element("Spentamount"), (System.DateTime)note.Element("Dateofpurchase")));

            return list;

        }
        public static void saveToXML(string path, ShoppingList list)
        {

            XDocument doc = new XDocument(
                new XElement("ShoppingList",
                    list.PurchaseList.Select(o => new XElement("ShoppingNote",
                    new XElement[] { new XElement("Name", o.name),
                                     new XElement("Comment", o.comment),
                                     new XElement("Spentamount", o.spentamount),
                                     new XElement("Dateofpurchase", o.dateofpurchase)})).ToArray()));

            doc.Save(path);
        }

        
    }
}
