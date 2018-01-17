using System;
using System.Collections.Generic;

namespace MobileBilling
{
    public class Bill
    {
        private Customer _customer;
        private double _taxPercentage;
        private double _billAmount;
        private int _startingHourOfPeakTime;
        private int _startingHourOfOffPeakTime;

        private List<CDR> _listOfCallDetails;


        public Bill(Customer customer, int startingHourOfPeakTime, int startingHourOfOffPeakTime)
        {
            this._customer = customer;
            this._taxPercentage = 20;
            this._startingHourOfPeakTime = startingHourOfPeakTime;
            this._startingHourOfOffPeakTime = startingHourOfOffPeakTime;
            this._listOfCallDetails = new List<CDR>();
        }

        public virtual void CalculateTheBill()
        {

        }
        

            public void AddCDRToList(CDR cdr)
        {
            this._listOfCallDetails.Add(cdr);
        }

        public List<CDR> listOfCallDetails
        {
            get { return this._listOfCallDetails; }
        }

        
        public double billAmount
        {
            get { return _billAmount; }
            set { _billAmount = value; }
        }

        public double taxPercentage
        {
            get { return _taxPercentage; }
        }

        public double startingHourOfPeakTime
        {
            get { return _startingHourOfPeakTime; }
        }

        public double startingHourOfOffPeakTime
        {
            get { return _startingHourOfOffPeakTime; }
        }

        public Customer customer
        {
            get { return _customer; }
        }

        //For printing purposes...
        public string PrintThebill()
        {
            return "Customer Name: " + _customer.fullName +
                "\nPhone number: " + _customer.phoneNumber +
                "\nAddress: " + _customer.billingAddress +
                "\nTotal Amount to Pay: LKR: " + this._billAmount;
        }

        public int CalculateMinutesToAdd(double seconds)
        {
            int totalMinutes = (int)(seconds / 60);
            
            //Adding extra min for extra seconds...
            if (seconds % 60 > 0)
            {
                totalMinutes++;
            }
            return totalMinutes;
        }

        public void CalculateTotalCallCharges(ref CDR cdr, double peakHoursLocalPerMiniuteOrPerSecondCharge, double offPeakHoursLocalPerMiniuteOrPerSecondCharge, double peakHoursLongDistancePerMiniuteOrPerSecondCharge, double offPeakHoursLongDistancePerMiniuteOrPerSecondCharge, ref double totalCallCharges, bool isPerMinute)
        {
            bool itIsALocalCall = ((int)(cdr.calledPartyNumber / 10000000) == (int)(cdr.callingPartyNumber / 10000000));
            bool itIsALongDistanceCall = ((int)(cdr.calledPartyNumber / 10000000) != (int)(cdr.callingPartyNumber / 10000000));

            bool inOffPeakTime = (((cdr.startingTimeOfTheCall.Hour < _startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= _startingHourOfOffPeakTime) && (cdr.startingTimeOfTheCall.Hour <= 24)));
            bool inPeakTime = ((cdr.startingTimeOfTheCall.Hour >= _startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour < _startingHourOfOffPeakTime));

            int totalMinutes = 0;


            if (isPerMinute)
            {
                //Code Original...
                //Finding number of mins to add...
                totalMinutes = CalculateMinutesToAdd(cdr.callDurationInSeconds);
            }
            else
            {
                totalMinutes = CalculateMinutesToAdd(cdr.callDurationInSeconds);
                if (cdr.callDurationInSeconds % 60 > 0)
                {
                    totalMinutes--;
                }
            }
            //Calculating Total Call Charges...
            for (int i = 0; i < totalMinutes; i++)
            {
                //Finding whethter the call is a long sitance call or a local one...
                //If it's a local one...
                if (itIsALocalCall)
                {
                    // Calculating Tatal Call Charges For Peak Hours Local Calls...
                    if (inPeakTime)
                    {
                        totalCallCharges += peakHoursLocalPerMiniuteOrPerSecondCharge;
                    }
                    // Calculating Tatal Call Charges For Off Peak Hours Local Calls...
                    if (inOffPeakTime)
                    {
                        totalCallCharges += offPeakHoursLocalPerMiniuteOrPerSecondCharge;
                    }
                }
                else if (itIsALongDistanceCall)
                {
                    // Calculating Tatal Call Charges For Peak Hours Long Distance Calls...
                    if (inPeakTime)
                    {
                        totalCallCharges += peakHoursLongDistancePerMiniuteOrPerSecondCharge;
                    }
                    // Calculating Tatal Call Charges For Off Peak Hours Long Distance Calls...
                    if (inOffPeakTime)
                    {
                        totalCallCharges += offPeakHoursLongDistancePerMiniuteOrPerSecondCharge;
                    }
                }
                //Increasing the time by minute, to find the correct time period for charging...
                cdr.startingTimeOfTheCall = cdr.startingTimeOfTheCall.AddMinutes(1);
                inOffPeakTime = (((cdr.startingTimeOfTheCall.Hour < _startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= _startingHourOfOffPeakTime) && (cdr.startingTimeOfTheCall.Hour <= 24)));
                inPeakTime = ((cdr.startingTimeOfTheCall.Hour >= _startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour < _startingHourOfOffPeakTime));


            }
        }

        
    }
}
