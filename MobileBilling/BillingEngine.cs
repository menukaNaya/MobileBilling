using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    public class BillingEngine
    {
        private Dictionary<long, Bill> billList;

        public BillingEngine(List<Customer> customers)
        {
            billList = new Dictionary<long, Bill>();
            foreach (Customer customer in customers)
            {
                if (billList.ContainsKey(customer.phoneNumber))
                {
                    continue;
                }
                else
                {
                    billList.Add(customer.phoneNumber, new Bill(customer));
                }
            }
        }

        public void Generate(List<CDR> listOfCDRs)
        {
            

            foreach (CDR cdr in listOfCDRs)
            {
                if (billList.ContainsKey(cdr.callingPartyNumber))
                {
                    billList[cdr.callingPartyNumber].AddCDRToList(cdr);
                }
                else
                {
                    continue;
                }
            }

            foreach (var pair in billList)
            {
                (pair.Value).CalculateTheBill();
                Console.WriteLine((pair.Value).printThebill());
            }
        }

        public Bill getTheBill(long phoneNumber)
        {
            if (billList.ContainsKey(phoneNumber))
            {
                return billList[phoneNumber];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(phoneNumber), "This customer is not registered!");
            }
        }
    }
}
