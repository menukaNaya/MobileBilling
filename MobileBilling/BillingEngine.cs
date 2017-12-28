using System;
using System.Collections.Generic;

namespace MobileBilling
{
    public class BillingEngine
    {
        private Dictionary<long, Bill> billList;

        //Addiiing customers to the Bill list...
        public BillingEngine(List<Customer> customers)
        {
            billList = new Dictionary<long, Bill>();
            foreach (Customer customer in customers)
            {
                //If the customer is already registered just continue...
                if (billList.ContainsKey(customer.phoneNumber))
                {
                    continue;
                }
                else
                {
                    //Otherwise adding the customer to the list...
                    billList.Add(customer.phoneNumber, new Bill(customer));
                }
            }
        }

        public void Generate(List<CDR> listOfCDRs)
        {
            foreach (CDR cdr in listOfCDRs)
            {
                //Adding the CDRs to there specific customer...
                if (billList.ContainsKey(cdr.callingPartyNumber))
                {
                    billList[cdr.callingPartyNumber].AddCDRToList(cdr);
                }
                else
                {
                    //We can catch the bills of not registered customers here...
                    continue;
                }
            }

            foreach (var pair in billList)
            {
                //Calvulating all bills of all customers...
                (pair.Value).CalculateTheBill();

                //Printing them all...
                Console.WriteLine((pair.Value).printThebill());
            }
        }

        //This for getting each Customers bill seperately using customer mobile number...
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
