using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;

namespace ProgCLICourse
{
    class XML_XML
    {
        public static void saveToXML(string path, ShoppingList list)
        {
            XmlDocument doc = new XmlDocument();

            var root = doc.CreateElement("ShoppingList");

            list.PurchaseList.ForEach(note =>
                {
                    XmlElement o = doc.CreateElement("ShoppingNote");

                    XmlElement nameEl = doc.CreateElement("Name");
                    nameEl.InnerText = note.name;
                    o.AppendChild(nameEl);

                    XmlElement commentEl = doc.CreateElement("Comment");
                    commentEl.InnerText = note.comment;
                    o.AppendChild(commentEl);

                    XmlElement spentamountEl = doc.CreateElement("Spentamount");
                    spentamountEl.InnerText = note.spentamount.ToString(CultureInfo.InvariantCulture);
                    o.AppendChild(spentamountEl);
                    
                    XmlElement dateofpurchaseEl = doc.CreateElement("Dateofpurchase");
                    dateofpurchaseEl.InnerText = note.dateofpurchase.ToString("yyyy-MM-ddTHH:mm:ss");
                    o.AppendChild(dateofpurchaseEl);
                    root.AppendChild(o);
                }
                );
            doc.AppendChild(root);
            doc.Save(path);
        }

        public static ShoppingList loadFromXML(string path)
        {
            ShoppingList list = new ShoppingList();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            

            foreach (XmlElement note in doc.DocumentElement)
            {
                list.AddPurchase(
                note.GetElementsByTagName("Name").Item(0).InnerText,
                note.GetElementsByTagName("Comment").Item(0).InnerText,
                double.Parse(note.GetElementsByTagName("Spentamount").Item(0).InnerText, CultureInfo.InvariantCulture),
                System.DateTime.Parse(note.GetElementsByTagName("Dateofpurchase").Item(0).InnerText));
            }

            return list;
        }
    }
}
