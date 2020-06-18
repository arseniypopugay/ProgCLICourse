using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgCLICourse
{
    [Serializable]
    public class ShoppingList
    {
        long sequence_number;

        public List<ShoppingNote> PurchaseList { get; private set; }


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
            var index = PurchaseList.FindIndex(x => x.number != number);
            if (index < 0)
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

        public Status mergeList(List<ShoppingNote> another_list)
        {
            return Status.OK;
        }


    }
}
