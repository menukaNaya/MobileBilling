using System.Collections.Generic;

namespace MobileBilling
{
    public class Bill
    {
        private Customer customer;
        private double _taxPercentage;
        private double _billAmount;
        
        private List<CDR> _listOfCallDetails;


        public Bill(Customer customer)
        {
            this.customer = customer;
            this._taxPercentage = 20;
            this._listOfCallDetails = new List<CDR>();
        }
        
        //For special cases...
        public Bill(Customer customer, double taxPercentage, double totalDiscount)
        {
            this.customer = customer;
            this._taxPercentage = taxPercentage;
            this._listOfCallDetails = new List<CDR>();
        }

        public void AddCDRToList(CDR cdr)
        {
            this._listOfCallDetails.Add(cdr);
        }

        public List<CDR> listOfCallDetails
        {
            get { return this._listOfCallDetails; }
        }

        
        public double billAmount
        {
            get { return _billAmount; }
            set { _billAmount = value; }
        }

        public double taxPercentage
        {
            get { return _taxPercentage; }
        }

        //For printing purposes...
        public string printThebill()
        {
            return "Customer Name: " + customer.fullName +
                "\nPhone number: " + customer.phoneNumber +
                "\nAddress: " + customer.billingAddress +
                "\nTotal Amount to Pay: LKR: " + this._billAmount;
        }
    }
}
