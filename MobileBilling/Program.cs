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
            CDR cdrForTest = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 23, 9, 0, 0), 123.5);
            CDR cdrForTest1 = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 23, 9, 0, 0), 58);

            listOfCallDetails.Add(cdrForTest);
            listOfCallDetails.Add(cdrForTest1);
            listOfCustomers.Add(customerForTest);
            _sut = new BillingEngine(listOfCustomers);
            _sut.Generate(listOfCallDetails);
        }
    }
}
