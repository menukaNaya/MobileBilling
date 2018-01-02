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
            double _peakHoursLocalPerSecondCharge = 4;
           _peakHoursLocalPerSecondCharge = Math.Round(_peakHoursLocalPerSecondCharge, 2);
            Console.WriteLine(_peakHoursLocalPerSecondCharge);
        }
    }
}
