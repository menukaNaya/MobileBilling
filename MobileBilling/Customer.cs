using System;

namespace MobileBilling
{
     public class Customer
    {
        private string _fullName { get; set; }
        private long _phoneNumber { get; set; }
        private string _billingAddress { get; set; }
        private int _packageCode { get; set; }
        private DateTime _registeredDate { get; set; }

        public Customer(string fullName, long phoneNumber, string billingAddress, DateTime registeredDate)
        {
            this._fullName = fullName;
            this._phoneNumber = phoneNumber;
            this._billingAddress = billingAddress;
            this._registeredDate = registeredDate;
        }
    }
}
