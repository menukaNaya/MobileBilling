using System;

namespace MobileBilling
{
    public class Customer
    {
        private string _fullName;
        private long _phoneNumber;
        private string _billingAddress;
        private int _packageCode;
        private DateTime _registeredDate;

        public Customer(string fullName, long phoneNumber, string billingAddress, DateTime registeredDate)
        {
            this._fullName = fullName;
            _phoneNumber = phoneNumber;
            _billingAddress = billingAddress;
            _registeredDate = registeredDate;
        }

        public string fullName
        {
            get { return _fullName; }
            set {_fullName = value; }
        }

        public long phoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string billingAddress
        {
            get { return _billingAddress; }
            set { _billingAddress = value; }
        }

        public int packageCode
        {
            get { return _packageCode; }
            set { _packageCode = value; }
        }

        public DateTime registeredDate
        {
            get { return _registeredDate; }
            set { _registeredDate = value; }
        }
    }
}
