using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    class BillingEngine
    {
        private Dictionary<long, Bill> billList;

        public BillingEngine()
        {
            billList = new Dictionary<long, Bill>();
        }

        public void Generate(List<Customer> customers, List<CDR> listOfCDRs)
        {
            foreach (Customer customer in customers)
            {
                billList.Add(customer.phoneNumber,new Bill(customer));
            }

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
            }
            
        }
    }
}
