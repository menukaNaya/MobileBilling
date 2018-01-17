using System;
using System.Collections.Generic;

namespace MobileBilling
{
    public class BillingEngine
    {
        private Dictionary<long, Bill> BillList;

        //Addiiing customers to the Bill list...
        public BillingEngine(List<Customer> customers)
        {
            BillList = new Dictionary<long, Bill>();
            //perSecondBillList = new Dictionary<long, PerSecondBill>();
            foreach (Customer customer in customers)
            {
                //If the customer is already registered just continue...
                if (BillList.ContainsKey(customer.phoneNumber))
                {
                    continue;
                }
                else
                {
                    //Otherwise adding the customer to the list...
                    if (customer.packageCode == 'A')
                    {
                        BillList.Add(customer.phoneNumber, new PerMinuteBill(customer, 10, 18, 3, 5, 2, 4, 100));
                    }
                    else if (customer.packageCode == 'B')
                    {
                        BillList.Add(customer.phoneNumber, new PerSecondBill(customer, 8, 20, 4, 6, 3, 5, 100));
                    }
                    else if (customer.packageCode == 'C')
                    {
                        BillList.Add(customer.phoneNumber, new PerMinuteBill(customer, 9, 18, 2, 3, 1, 2, 300));
                    }
                    else if (customer.packageCode == 'D')
                    {
                        BillList.Add(customer.phoneNumber, new PerSecondBill(customer, 8, 20, 3, 5, 2, 4, 300));
                    }
                }
            }
        }

        public void Generate(List<CDR> listOfCDRs)
        {
            foreach (CDR cdr in listOfCDRs)
            {
                //Adding the CDRs to there specific customer...
                if (BillList.ContainsKey(cdr.callingPartyNumber))
                {
                    BillList[cdr.callingPartyNumber].AddCDRToList(cdr);
                }
                else
                {
                    //We can catch the bills of not registered customers here...
                    continue;
                }
            }

            foreach (var bill in BillList.Values)
            {
                //Calculating all bills of all customers...
                bill.CalculateTheBill();

                //Printing them all...
                Console.WriteLine(bill.PrintThebill());
            }

        }

        //This for getting each Customers bill seperately using customer mobile number...
        public Bill getTheBill(long phoneNumber)
        {
            if (BillList.ContainsKey(phoneNumber))
            {
                return BillList[phoneNumber];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(phoneNumber), "This customer is not registered!");
            }
        }
    }
}
