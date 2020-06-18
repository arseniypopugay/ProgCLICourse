using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProgCLICourse
{
    class ShoppingMenu
    {
        
        static void Main(string[] args)
        {
            

            Console.WriteLine("Welcome. Point of menu you want to start");


            ShoppingList MainList = new ShoppingList();
            
            bool working = true;

            while (working)
            {
                Console.WriteLine(
                    "1. Add new purchase\n" +
                    "2. Remove previous purchase\n" +
                    "3. Show purchases in period of time\n" +
                    "4. Load list of purchases from file\n" +
                    "5. Save list of purchases into file\n" +
                    "6. Exit from this program\n");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        addPurchase(MainList);
                        break;
                    case "2":
                        removePurchase(MainList);
                        break;
                    case "3":
                        showPurchase(MainList);
                        break;
                    case "4":
                        MainList = loadList();
                        Console.WriteLine();
                        break;
                    case "5":
                        saveList(MainList);
                        break;
                    case "6":
                        working = false;
                        break;
                }
            }

        }

        private static void addPurchase(ShoppingList MainList)
        {
            string name;
            string comment;
            double spentamount;
            DateTime dateofpurchase;

            Console.WriteLine("Enter name of purchase");
            name = Console.ReadLine();

            Console.WriteLine("Enter comment for purchase");
            comment = Console.ReadLine();

            Console.WriteLine("Enter amount of spent credit");
            while (!double.TryParse(Console.ReadLine(), out spentamount))
                Console.WriteLine("Look's like you've made a mistake. Try one more time");

            Console.WriteLine("Enter date of purchase in format dd MM yyyy");
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd MM yyyy", new CultureInfo("ru-Ru"), DateTimeStyles.None, out dateofpurchase))
                Console.WriteLine("Something went wrong. Try to input one more time");

            switch(MainList.AddPurchase(name, comment, spentamount, dateofpurchase))
            {
                case Status.OK:
                    Console.WriteLine("Purchase was successfully added\n");
                    break;
                case Status.Unexpected:
                    Console.WriteLine("Some of your inputed data quite suspision. Are you sure that everything alright?\n");
                    break;
            }
        }

        private static void removePurchase(ShoppingList MainList)
        {
            long number;

            Console.WriteLine("Enter a number in list of purchase you want to delete");
            while (!long.TryParse(Console.ReadLine(), out number))
                Console.WriteLine("Looks like you misstaped some keys. Try to input INTEGER one more time");

            switch(MainList.RemovePurchase(number))
            {
                case Status.OK:
                    Console.WriteLine("Purchase note was successfully deleted\n");
                    break;

                case Status.Failed:
                    Console.WriteLine("An error has occured. Possible, note that you want to delete doesn't exist?\n");
                    break;
            }

        }

        private static void showPurchase(ShoppingList MainList)
        {
            DateTime start;
            DateTime end;

            Console.WriteLine("Enter Start of Time period in format dd MM yyyy");
            
            while(!DateTime.TryParseExact(Console.ReadLine(), "dd MM yyyy", new CultureInfo("ru-Ru"), DateTimeStyles.None, out start)) 
                Console.WriteLine("Something went wrong. Try to input one more time");
            

            Console.WriteLine("Enter End of Time period in format dd MM yyyy");

            while (!DateTime.TryParseExact(Console.ReadLine(), "dd MM yyyy", new CultureInfo("ru-Ru"), DateTimeStyles.None, out end))
                Console.WriteLine("Something went wrong. Try to input one more time");


            Console.WriteLine();
            List<string> return_list = MainList.PurchasesRange(start, end);

            if (return_list.Count() == 0)
                Console.WriteLine("Not found any notes in this period of time\n");
            else
                return_list.ForEach(x => Console.WriteLine(x));


        }

        private static void saveList(ShoppingList MainList)
        {
            Console.WriteLine("Enter path and file name to save:");
            try
            {
                XMLInteractor.save(Console.ReadLine(), MainList);
                Console.WriteLine();
            }
                catch
            {
                Console.WriteLine("Hmmmmmmmmmmmm, Something went wrong");
            }
        }

        private static ShoppingList loadList()
        {
            Console.WriteLine("Enter path and file name to load:");
            try
            {
                return XMLInteractor.loadXML(Console.ReadLine());
            }
                catch
            {
                Console.WriteLine("Hmmmmmmmmmmmm, Something went wrong");
                return new ShoppingList();
            }
        }
    }
}
