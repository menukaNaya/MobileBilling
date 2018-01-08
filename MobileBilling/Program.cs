using System;

namespace MobileBilling
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime ne = new DateTime(2017, 12, 23, 9, 0, 5);
            DateTime we = new DateTime(2017, 12, 23, 20, 0, 20);


            TimeSpan mn = new TimeSpan(-9,0,-5);
            TimeSpan kaw = we - (ne.Add(mn)).AddHours(20);

            Console.WriteLine(kaw.TotalSeconds);
        }
    }
}
