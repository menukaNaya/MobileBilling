﻿using System;

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
        private double _allSpecialOffersFromThePackage;
        private double _discountPercentageOfPackageB;
        private double _discountPercentageOfPackageD;

        

        public PerSecondBill(Customer customer, int startingHourOfPeakTime, int startingHourOfOffPeakTime, double peakHoursLocalPerSecondCharge, double peakHoursLongDistancePerSecondCharge, double offPeakHoursLocalPerSecondCharge, double offPeakHoursLongDistancePerSecondCharge, double monthlyRental) : base(customer, startingHourOfPeakTime, startingHourOfOffPeakTime)
        {
            this._peakHoursLocalPerSecondCharge = peakHoursLocalPerSecondCharge;
            this._offPeakHoursLocalPerSecondCharge = offPeakHoursLocalPerSecondCharge;
            this._peakHoursLongDistancePerSecondCharge = peakHoursLongDistancePerSecondCharge;
            this._offPeakHoursLongDistancePerSecondCharge = offPeakHoursLongDistancePerSecondCharge;
            this._monthlyRental = monthlyRental;
            this._discountPercentageOfPackageB = 40;
            this._discountPercentageOfPackageD = 0;
        }

        public override void CalculateTheBill()
        {
            foreach (CDR cdr in listOfCallDetails)
            {
                CDR cdrInProcess = cdr;

                CalculateTotalCallCharges(ref cdrInProcess, this._peakHoursLocalPerSecondCharge, this._offPeakHoursLocalPerSecondCharge, this._peakHoursLongDistancePerSecondCharge, this._offPeakHoursLongDistancePerSecondCharge, ref this._totalCallCharges, false);

                bool itIsALocalCall = ((int)(cdr.calledPartyNumber / 10000000) == (int)(cdr.callingPartyNumber / 10000000));
                bool itIsALongDistanceCall = ((int)(cdr.calledPartyNumber / 10000000) != (int)(cdr.callingPartyNumber / 10000000));

                bool inOffPeakTime = (((cdrInProcess.startingTimeOfTheCall.Hour < 8) && (cdrInProcess.startingTimeOfTheCall.Hour >= 0)) || ((cdrInProcess.startingTimeOfTheCall.Hour >= 20) && (cdrInProcess.startingTimeOfTheCall.Hour <= 24)));
                bool inPeakTime = ((cdrInProcess.startingTimeOfTheCall.Hour >= 8) && (cdrInProcess.startingTimeOfTheCall.Hour < 20));

                //Adding the cost for extra seconds...
                if (cdr.callDurationInSeconds % 60 > 0)
                {
                    if (itIsALocalCall)
                    {
                        // Calculating Tatal Call Charges For Peak Hours Local Calls...
                        if (inPeakTime)
                        {
                            this._totalCallCharges += this._peakHoursLocalPerSecondCharge / 60.0 * (cdrInProcess.callDurationInSeconds % 60);
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Local Calls...
                        if (inOffPeakTime)
                        {
                            this._totalCallCharges += this._offPeakHoursLocalPerSecondCharge / 60.0 * (cdrInProcess.callDurationInSeconds % 60);
                        }
                    }
                    else if (itIsALongDistanceCall)
                    {
                        // Calculating Tatal Call Charges For Peak Hours Long Distance Calls...
                        if (inPeakTime)
                        {
                            this._totalCallCharges += this._peakHoursLongDistancePerSecondCharge / 60.0 * (cdrInProcess.callDurationInSeconds % 60);
                        }

                        // Calculating Tatal Call Charges For Off Peak Hours Long Distance Calls...
                        if (inOffPeakTime)
                        {
                            this._totalCallCharges += this._offPeakHoursLongDistancePerSecondCharge / 60.0 * (cdrInProcess.callDurationInSeconds % 60);
                        }
                    }
                }

                this._allSpecialOffersFromThePackage = CalculatePackageOffer(cdr);
            }

            this._tax = (this._totalCallCharges + this._monthlyRental - this._allSpecialOffersFromThePackage) * taxPercentage / 100;
            this._toatalDiscount = CalculateDiscountForTheSpecificPackage();
            billAmount = Math.Round((this._totalCallCharges + this._monthlyRental + this._tax - this._toatalDiscount - this._allSpecialOffersFromThePackage), 2);
        }

        private double CalculatePackageOffer(CDR cdr)
        {
            bool itIsALocalCall = ((int)(cdr.calledPartyNumber / 10000000) == (int)(cdr.callingPartyNumber / 10000000));
            bool itIsALongDistanceCall = ((int)(cdr.calledPartyNumber / 10000000) != (int)(cdr.callingPartyNumber / 10000000));

            bool inOffPeakTime = (((cdr.startingTimeOfTheCall.Hour < startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= startingHourOfOffPeakTime) && (cdr.startingTimeOfTheCall.Hour <= 24)));
            bool inPeakTime = ((cdr.startingTimeOfTheCall.Hour >= startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour < startingHourOfOffPeakTime));

            double packageOffer = 0;

            if (customer.packageCode == 'B' && itIsALocalCall && inOffPeakTime)
            {
                    packageOffer = this._offPeakHoursLocalPerSecondCharge;
            }

            return packageOffer;
        }

        private double CalculateDiscountForTheSpecificPackage()
        {
            double packageDiscount = 0;

            if (customer.packageCode == 'B')
            {
                if (this._totalCallCharges > 1000)
                {
                    packageDiscount = this._totalCallCharges * this._discountPercentageOfPackageB / 100;
                }
            }
            else if (customer.packageCode == 'D')
            {
                    packageDiscount = this._totalCallCharges * this._discountPercentageOfPackageD / 100;
            }
 
            return packageDiscount;
        }

        public double discountPercentageOfPackageB
        {
            get { return _discountPercentageOfPackageB; }
            set { _discountPercentageOfPackageB = value; }
        }

        public double discountPercentageOfPackageD
        {
            get { return _discountPercentageOfPackageD; }
            set { _discountPercentageOfPackageD = value; }
        }
    }
}
