using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    class BillingEngine
    {
        public long _phoneNumber { get; set; }
        public string _billingAddress { get; set; }
        public double _totalCallCharges { get; set; }
        public double _toatalDiscount { get; set; }
        public double _tax { get; set; }
        public long _rental { get; set; }
    }
}
