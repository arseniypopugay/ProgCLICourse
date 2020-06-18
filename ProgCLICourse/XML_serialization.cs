using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ProgCLICourse
{
    class XML_serialization
    {
        public static void saveToXML(string path, ShoppingList list)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ShoppingListType));

            ShoppingListType wrappedList = new ShoppingListType();
            var notes = new List<NoteType>();
            list.PurchaseList.ForEach(o => notes.Add(new NoteType(o.name, o.comment, o.spentamount, o.dateofpurchase)));
            wrappedList.ShoppingNote = notes.ToArray();
            
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, wrappedList);
            }

        }

        public static ShoppingList loadFromXML(string path)
        {
            ShoppingList list = new ShoppingList();
            ShoppingListType wrappedList;
            XmlSerializer ser = new XmlSerializer(typeof(ShoppingListType));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                wrappedList = (ShoppingListType) ser.Deserialize(fs);
            }

            wrappedList.ShoppingNote.ToList().ForEach(o => list.AddPurchase(o.Name, o.Comment, o.Spentamount, o.Dateofpurchase));

            return list;
        }
    }
}
