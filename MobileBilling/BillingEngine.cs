using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    public class BillingEngine : Customer
    {
        
        private double _totalCallCharges { get; set; }
        private double _toatalDiscount { get; set; }
        private double _tax { get; set; }
        private long _rental { get; set; }
    }
}
