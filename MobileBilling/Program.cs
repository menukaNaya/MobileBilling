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
            //long num = 9711535724;
            //Console.WriteLine(num);
            //Console.WriteLine(num / 10000000);
            //Console.WriteLine(num % 10000000);
            DateTime me = new DateTime(2004, 5, 27, 0, 0, 0);

            //CDR ne = new CDR(-5, 0808018203, me, 23423.234);

            Customer n = new Customer("Menuka Nayanadeepa", 0711535724, "Pllaekekulawala, Nawathalwaththa", me);
            //n.fullName = "Weerasooriya";
            //Console.WriteLine(n.fullName);
        }
    }
}
