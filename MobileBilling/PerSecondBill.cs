using System;

namespace MobileBilling
{
    public class PerSecondBill : Bill
    {
        private double _peakHoursLocalPerSecondCharge;
        private double _peakHoursLongDistancePerSecondCharge;
        private double _offPeakHoursLocalPerSecondCharge;
        private double _offPeakHoursLongDistancePerSecondCharge;
        private double _monthlyRental;
        private double _toatalDiscount;
        private double _totalCallCharges;
        private double _tax;

        public PerSecondBill(Customer customer) : base(customer)
        {
            this._peakHoursLocalPerSecondCharge = 4/60.0;
            this._offPeakHoursLocalPerSecondCharge = 3/60.0;
            this._peakHoursLongDistancePerSecondCharge = 6/60.0;
            this._offPeakHoursLongDistancePerSecondCharge = 5/60.0;
            this._monthlyRental = 100;
            this._toatalDiscount = 0;
        }

        public void CalculateTheBill()
        {
            foreach (CDR cdr in listOfCallDetails)
            {
                //Finding number of mins to add...
                int totalMinutes = (int)(cdr.callDurationInSeconds / 60);

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
                            this._totalCallCharges += this._peakHoursLocalPerSecondCharge * 60;
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Local Calls...
                        if (((cdr.startingTimeOfTheCall.Hour < 8) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= 20) && (cdr.startingTimeOfTheCall.Hour <= 24)))
                        {
                            this._totalCallCharges += this._offPeakHoursLocalPerSecondCharge * 60;
                        }
                    }
                    else
                    {
                        // Calculating Tatal Call Charges For Peak Hours Long Distance Calls...
                        if ((cdr.startingTimeOfTheCall.Hour >= 8) && (cdr.startingTimeOfTheCall.Hour) < 20)
                        {
                            this._totalCallCharges += this._peakHoursLongDistancePerSecondCharge * 60;
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Long Distance Calls...
                        if (((cdr.startingTimeOfTheCall.Hour < 8) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= 20) && (cdr.startingTimeOfTheCall.Hour <= 24)))
                        {
                            this._totalCallCharges += this._offPeakHoursLongDistancePerSecondCharge * 60;
                        }
                    }

                    //Increasing the time by minute, to find the correct time period for charging...
                    cdr.startingTimeOfTheCall = cdr.startingTimeOfTheCall.AddMinutes(1);
                }

                //Adding the cost for extra seconds...
                if (cdr.callDurationInSeconds % 60 > 0)
                {
                    if ((int)(cdr.calledPartyNumber / 10000000) == (int)(cdr.callingPartyNumber / 10000000))
                    {
                        // Calculating Tatal Call Charges For Peak Hours Local Calls...
                        if ((cdr.startingTimeOfTheCall.Hour >= 8) && (cdr.startingTimeOfTheCall.Hour) < 20)
                        {
                            this._totalCallCharges += this._peakHoursLocalPerSecondCharge * (cdr.callDurationInSeconds % 60);
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Local Calls...
                        if (((cdr.startingTimeOfTheCall.Hour < 8) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= 20) && (cdr.startingTimeOfTheCall.Hour <= 24)))
                        {
                            this._totalCallCharges += this._offPeakHoursLocalPerSecondCharge * (cdr.callDurationInSeconds % 60);
                        }
                    }
                    else
                    {
                        // Calculating Tatal Call Charges For Peak Hours Long Distance Calls...
                        if ((cdr.startingTimeOfTheCall.Hour >= 8) && (cdr.startingTimeOfTheCall.Hour) < 20)
                        {
                            this._totalCallCharges += this._peakHoursLongDistancePerSecondCharge * (cdr.callDurationInSeconds % 60);
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Long Distance Calls...
                        if (((cdr.startingTimeOfTheCall.Hour < 8) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= 20) && (cdr.startingTimeOfTheCall.Hour <= 24)))
                        {
                            this._totalCallCharges += this._offPeakHoursLongDistancePerSecondCharge * (cdr.callDurationInSeconds % 60);
                        }
                    }
                }
            }

            this._tax = (this._totalCallCharges + this._monthlyRental) * taxPercentage / 100;
            billAmount =Math.Round((this._totalCallCharges + this._monthlyRental + this._tax - this._toatalDiscount), 2);
        }
    }
}
