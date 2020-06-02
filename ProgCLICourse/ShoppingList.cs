using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgCLICourse
{
    class ShoppingList
    {
        private List<ShoppingNote> PurchaseList;

        public ShoppingList()
        {
             this.PurchaseList = new List<ShoppingNote>();
        }

        public Status AddPurchase(String name, String comment, double spentamount, DateTime dateofpurchase)
        {
            if (spentamount < 0) return Status.Unexpected; 

            long number = 0;
            if (PurchaseList.Count() > 0)
                number = PurchaseList.OrderBy(x => x.name).Last().number;

            this.PurchaseList.Add(new ShoppingNote(name, comment, spentamount, dateofpurchase, number));

            return Status.OK;
        }

        public Status RemovePurchase(long number)
        {
            if (PurchaseList.All(x => x.number != number))
                return Status.Failed;

            PurchaseList.Remove(PurchaseList.Single(x=> x.number==number));
            return Status.OK;
        }

        public List<string> PurchasesRange(DateTime start, DateTime end)
        {
            List<string> notes = new List<string>();
            PurchaseList.ForEach(x =>
            {
                if (x.dateofpurchase <= end && x.dateofpurchase >= start)
                    notes.Add(x.toString());
            });

            return notes;
        }


    }
}
