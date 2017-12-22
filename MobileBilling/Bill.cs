using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    public class Bill
    {
        private Customer customer;
        private double _totalCallCharges { get; set; }
        private double _toatalDiscount { get; set; }
        private double _tax { get; set; }
        private double _rental { get; set; }
        private double _billAmount { get; set; }
        private List<CDR> ListOfCallDetails;


        public Bill(Customer customer)
        {
            this.customer = customer;
            ListOfCallDetails = new List<CDR>();
        }

        public void AddCDRToList(CDR cdr)
        {
            ListOfCallDetails.Add(cdr);
        }

        public void CalculateTheBill()
        {
            //ListOfCallDetails.Add(cdr);
        }
    }
}
