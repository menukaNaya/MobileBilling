using System;
using System.Collections.Generic;

namespace MobileBilling
{
    class Program
    {
        static void Main(string[] args)
        {
            BillingEngine _sut;
            List<CDR> listOfCallDetails = new List<CDR>();
            List<Customer> listOfCustomers = new List<Customer>();

            Customer customerForTest = new Customer("Menuka Nayanadeepa", 0711535724, "Pallekekulawala, Nawathalwaththa.", new DateTime(2010, 5, 27, 10, 0, 0), 'B');
            //Customer customerForTest2 = new Customer("Menuka Nayanadeepa", 0721535724, "Pallekekulawala, Nawathalwaththa.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');

            CDR cdrForTest = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 23, 9, 0, 0), 60);
            //CDR cdrForTest1 = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 24, 9, 0, 0), 58);
            //CDR cdrForTest2 = new CDR(0711535724, 0721593911, new DateTime(2017, 12, 25, 19, 0, 0), 128);
            //CDR cdrForTest3 = new CDR(0721535724, 0721593911, new DateTime(2017, 12, 23, 9, 0, 0), 128);

            listOfCustomers.Add(customerForTest);
            //listOfCustomers.Add(customerForTest2);

            listOfCallDetails.Add(cdrForTest);
            //listOfCallDetails.Add(cdrForTest1);
            //listOfCallDetails.Add(cdrForTest2);
            //listOfCallDetails.Add(cdrForTest3);
            
            //Console.WriteLine((int)(cdrForTest.calledPartyNumber / 10000000));
            _sut = new BillingEngine(listOfCustomers);
            _sut.Generate(listOfCallDetails);

            //DateTime me = new DateTime(2017, 12, 23, 23, 59, 58);
            //me = me.AddSeconds(2);
            //Console.WriteLine(me);
        }
    }
}
