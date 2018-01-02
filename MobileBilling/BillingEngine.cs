using System;
using System.Collections.Generic;

namespace MobileBilling
{
    public class BillingEngine
    {
        private Dictionary<long, PerMinuteBill> perMinuteBillList;
        private Dictionary<long, PerSecondBill> perSecondBillList;

        //Addiiing customers to the Bill list...
        public BillingEngine(List<Customer> customers)
        {
            perMinuteBillList = new Dictionary<long, PerMinuteBill>();
            perSecondBillList = new Dictionary<long, PerSecondBill>();

            foreach (Customer customer in customers)
            {
                //If the customer is already registered just continue...
                if (perMinuteBillList.ContainsKey(customer.phoneNumber) || perSecondBillList.ContainsKey(customer.phoneNumber))
                {
                    continue;
                }
                else
                {
                    //Otherwise adding the customer to the list...
                    if (customer.packageCode == 'A')
                    {
                        perMinuteBillList.Add(customer.phoneNumber, new PerMinuteBill(customer));
                    }
                    else if (customer.packageCode == 'B')
                    {
                        perSecondBillList.Add(customer.phoneNumber, new PerSecondBill(customer));
                    }

                }
            }
        }

        public void Generate(List<CDR> listOfCDRs)
        {
            foreach (CDR cdr in listOfCDRs)
            {
                //Adding the CDRs to there specific customer...
                if (perMinuteBillList.ContainsKey(cdr.callingPartyNumber))
                {
                    perMinuteBillList[cdr.callingPartyNumber].AddCDRToList(cdr);
                }
                else if (perSecondBillList.ContainsKey(cdr.callingPartyNumber))
                {
                    perSecondBillList[cdr.callingPartyNumber].AddCDRToList(cdr);
                }
                else
                {
                    //We can catch the bills of not registered customers here...
                    continue;
                }
            }

            foreach (var bill in perMinuteBillList.Values)
            {
                //Calculating all bills of all customers...
                bill.CalculateTheBill();

                //Printing them all...
                Console.WriteLine(bill.printThebill());
            }

            foreach (var bill in perSecondBillList.Values)
            {
                //Calculating all bills of all customers...
                bill.CalculateTheBill();

                //Printing them all...
                Console.WriteLine(bill.printThebill());
            }
        }

        //This for getting each Customers bill seperately using customer mobile number...
        public Bill getTheBill(long phoneNumber)
        {
            if (perMinuteBillList.ContainsKey(phoneNumber))
            {
                return perMinuteBillList[phoneNumber];
            }
            else if (perSecondBillList.ContainsKey(phoneNumber))
            {
                return perSecondBillList[phoneNumber];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(phoneNumber), "This customer is not registered!");
            }
        }
    }
}
