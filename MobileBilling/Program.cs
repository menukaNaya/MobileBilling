using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    class Program
    {
        static void Main(string[] args)
        {

            BillingEngine _sut;
            List<CDR> listOfCallDetails = new List<CDR>();
            List<Customer> listOfCustomers = new List<Customer>();

            Customer customerForTest = new Customer("Menuka Nayanadeepa", 0711535724, "Pallekekulawala, Nawathalwaththa.", new DateTime(2010, 5, 27, 10, 0, 0));
            CDR cdrForTest = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 23, 19, 59, 0), 185);

            listOfCallDetails.Add(cdrForTest);
            listOfCustomers.Add(customerForTest);

            //Console.WriteLine((int)(cdrForTest.calledPartyNumber / 10000000));
            _sut = new BillingEngine(listOfCustomers);
            _sut.Generate(listOfCallDetails);

            //DateTime me = new DateTime(2017, 12, 23, 23, 59, 58);
            //me = me.AddSeconds(2);
            //Console.WriteLine(me);
        }
    }
}
