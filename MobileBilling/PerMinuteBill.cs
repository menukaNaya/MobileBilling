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
        private double _allSpecialOffersFromThePackage = 0;
        private double _discountPercentageOfPackageA;
        private double _discountPercentageOfPackageC;

        public PerMinuteBill(Customer customer, int startingHourOfPeakTime, int startingHourOfOffPeakTime, double peakHoursLocalPerMiniuteCharge, double peakHoursLongDistancePerMiniuteCharge, double offPeakHoursLocalPerMiniuteCharge, double offPeakHoursLongDistancePerMiniuteCharge, double monthlyRental) : base(customer, startingHourOfPeakTime, startingHourOfOffPeakTime)
        {
            this._peakHoursLocalPerMiniuteCharge = peakHoursLocalPerMiniuteCharge;
            this._offPeakHoursLocalPerMiniuteCharge = offPeakHoursLocalPerMiniuteCharge;
            this._peakHoursLongDistancePerMiniuteCharge = peakHoursLongDistancePerMiniuteCharge;
            this._offPeakHoursLongDistancePerMiniuteCharge = offPeakHoursLongDistancePerMiniuteCharge;
            this._monthlyRental = monthlyRental;
            this._discountPercentageOfPackageA = 40;
            this._discountPercentageOfPackageC = 0;
        }


        public override void CalculateTheBill()
        {
            foreach (CDR cdr in listOfCallDetails)
            {
                CDR cdrInProcess = cdr;
                CalculateTotalCallCharges(ref cdrInProcess, this._peakHoursLocalPerMiniuteCharge, this._offPeakHoursLocalPerMiniuteCharge, this._peakHoursLongDistancePerMiniuteCharge, this._offPeakHoursLongDistancePerMiniuteCharge, ref this._totalCallCharges, true);
                this._allSpecialOffersFromThePackage = CalculatePackageOffer(cdr);

            }

            this._tax = (this._totalCallCharges + this._monthlyRental - this._allSpecialOffersFromThePackage) * taxPercentage / 100;
            this._toatalDiscount = CalculateDiscountForTheSpecificPackage();
            billAmount =Math.Round((this._totalCallCharges + this._monthlyRental + this._tax - this._toatalDiscount - this._allSpecialOffersFromThePackage), 2);
        }

        private double CalculatePackageOffer(CDR cdr)
        {
            bool itIsALocalCall = ((int)(cdr.calledPartyNumber / 10000000) == (int)(cdr.callingPartyNumber / 10000000));
            bool itIsALongDistanceCall = ((int)(cdr.calledPartyNumber / 10000000) != (int)(cdr.callingPartyNumber / 10000000));

            bool inOffPeakTime = (((cdr.startingTimeOfTheCall.Hour < startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour >= 0)) || ((cdr.startingTimeOfTheCall.Hour >= startingHourOfOffPeakTime) && (cdr.startingTimeOfTheCall.Hour <= 24)));
            bool inPeakTime = ((cdr.startingTimeOfTheCall.Hour >= startingHourOfPeakTime) && (cdr.startingTimeOfTheCall.Hour < startingHourOfOffPeakTime));

            double packageOffer = 0;

            if (customer.packageCode == 'C' && itIsALocalCall)
            {
                if (inOffPeakTime)
                {
                    packageOffer = this._offPeakHoursLocalPerMiniuteCharge;
                }
                else
                {
                    packageOffer = this._peakHoursLocalPerMiniuteCharge;
                }
            }

            return packageOffer;
        }

        private double CalculateDiscountForTheSpecificPackage()
        {
            double packageDiscount = 0;

            if (customer.packageCode == 'A')
            {
                if(this._totalCallCharges > 1000)
                {
                    packageDiscount = this._totalCallCharges * this._discountPercentageOfPackageA / 100;

                }
            }
            else if(customer.packageCode == 'C')
            {
                packageDiscount = this._totalCallCharges * this._discountPercentageOfPackageC / 100;

            }

            return packageDiscount;
        }

    }
}

