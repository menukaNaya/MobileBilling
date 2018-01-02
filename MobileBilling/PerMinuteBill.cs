using System;

namespace MobileBilling
{
    public class PerMinuteBill : Bill
    {
        private double _peakHoursLocalPerMiniuteCharge;
        private double _peakHoursLongDistancePerMiniuteCharge;
        private double _offPeakHoursLocalPerMiniuteCharge;
        private double _offPeakHoursLongDistancePerMiniuteCharge;
        private double _monthlyRental;
        private double _toatalDiscount;
        private double _totalCallCharges;
        private double _tax;


        public PerMinuteBill(Customer customer) : base(customer)
        {
            this._peakHoursLocalPerMiniuteCharge = 3;
            this._offPeakHoursLocalPerMiniuteCharge = 2;
            this._peakHoursLongDistancePerMiniuteCharge = 5;
            this._offPeakHoursLongDistancePerMiniuteCharge = 4;
            this._monthlyRental = 100;
            this._toatalDiscount = 0;

        }

        public void CalculateTheBill()
        {
            foreach (CDR cdr in listOfCallDetails)
            {
                //Finding number of mins to add...
                int totalMinutes = (int)(cdr.callDurationInSeconds / 60);

                //Adding extra min for extra seconds...
                if (cdr.callDurationInSeconds % 60 > 0)
                {
                    totalMinutes++;
                }

                //Calculating Total Call Charges...
                for (int i = 0; i < totalMinutes; i++)
                {
                    //Finding whethter the call is a long sitance call or a local one...
                    //If it's a local one...
                    if ((int)(cdr.calledPartyNumber / 10000000) == (int)(cdr.callingPartyNumber / 10000000))
                    {
                        // Calculating Tatal Call Charges For Peak Hours Local Calls...
                        if ((cdr.startingTimeOfTheCall.Hour >= 8) && (cdr.startingTimeOfTheCall.Hour) < 20)
                        {
                            this._totalCallCharges += this._peakHoursLocalPerMiniuteCharge;
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Local Calls...
                        if (((cdr.startingTimeOfTheCall.Hour < 8) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= 20) && (cdr.startingTimeOfTheCall.Hour <= 24)))
                        {
                            this._totalCallCharges += this._offPeakHoursLocalPerMiniuteCharge;
                        }
                    }
                    else
                    {
                        // Calculating Tatal Call Charges For Peak Hours Long Distance Calls...
                        if ((cdr.startingTimeOfTheCall.Hour >= 8) && (cdr.startingTimeOfTheCall.Hour) < 20)
                        {
                            this._totalCallCharges += this._peakHoursLongDistancePerMiniuteCharge;
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Long Distance Calls...
                        if (((cdr.startingTimeOfTheCall.Hour < 8) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= 20) && (cdr.startingTimeOfTheCall.Hour <= 24)))
                        {
                            this._totalCallCharges += this._offPeakHoursLongDistancePerMiniuteCharge;
                        }
                    }

                    //Increasing the time by minute, to find the correct time period for charging...
                    cdr.startingTimeOfTheCall = cdr.startingTimeOfTheCall.AddMinutes(1);
                }
            }

            this._tax = (this._totalCallCharges + this._monthlyRental) * taxPercentage / 100;
            //Console.WriteLine(this._tax);

            billAmount =Math.Round((this._totalCallCharges + this._monthlyRental + this._tax - this._toatalDiscount), 2);
        }
    }
}
