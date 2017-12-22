﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    public class Bill
    {
        private string _fullName { get; set; }
        private long _phoneNumber { get; set; }
        private string _billingAddress { get; set; }
        private double _totalCallCharges { get; set; }
        private double _toatalDiscount { get; set; }
        private double _tax { get; set; }
        private long _rental { get; set; }

        public Bill(string fullName, long phoneNumber, string billingAddress)
        {
            _fullName = fullName;
            _phoneNumber = phoneNumber;
            _billingAddress = billingAddress;
        }
    }
}