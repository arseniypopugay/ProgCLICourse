using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgCLICourse
{
    
    class ShoppingList
    {
        long sequence_number;

        private List<ShoppingNote> PurchaseList;

        public ShoppingList()
        {
            this.PurchaseList = new List<ShoppingNote>();
            sequence_number = 1;
        }

        public Status AddPurchase(String name, String comment, double spentamount, DateTime dateofpurchase)
        {
            if (spentamount < 0) return Status.Unexpected; 


            this.PurchaseList.Add(new ShoppingNote(name, comment, spentamount, dateofpurchase, sequence_number++));

            return Status.OK;
        }

        public Status RemovePurchase(long number)
        {
            int count = PurchaseList.Count();
            PurchaseList = PurchaseList.Where(x => x.number != number).ToList();
            
            if (count == PurchaseList.Count())
                return Status.Failed;

            return Status.OK;
        }

        public List<string> PurchasesRange(DateTime start, DateTime end)
        {
            List<string> notes = new List<string>();
            
            foreach(ShoppingNote x in PurchaseList)
                if (x.dateofpurchase <= end && x.dateofpurchase >= start)
                    notes.Add(x.toString());


            return notes;
        }


    }
}
