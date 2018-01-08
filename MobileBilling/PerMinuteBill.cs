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
        
        public PerMinuteBill(Customer customer, double peakHoursLocalPerMiniuteCharge, double peakHoursLongDistancePerMiniuteCharge, double offPeakHoursLocalPerMiniuteCharge, double offPeakHoursLongDistancePerMiniuteCharge, double monthlyRental) : base(customer)
        {
            this._peakHoursLocalPerMiniuteCharge = peakHoursLocalPerMiniuteCharge;
            this._offPeakHoursLocalPerMiniuteCharge = offPeakHoursLocalPerMiniuteCharge;
            this._peakHoursLongDistancePerMiniuteCharge = peakHoursLongDistancePerMiniuteCharge;
            this._offPeakHoursLongDistancePerMiniuteCharge = offPeakHoursLongDistancePerMiniuteCharge;
            this._monthlyRental = monthlyRental;
            this._toatalDiscount = 0;
        }

        public override void CalculateTheBill()
        {
            foreach (CDR cdr in listOfCallDetails)
            {
                CDR cdrInProcess = cdr;
                CalculateTotalCallCharges(ref cdrInProcess, this._peakHoursLocalPerMiniuteCharge, this._offPeakHoursLocalPerMiniuteCharge, this._peakHoursLongDistancePerMiniuteCharge, this._offPeakHoursLongDistancePerMiniuteCharge, ref this._totalCallCharges, true);
            }

            this._tax = (this._totalCallCharges + this._monthlyRental) * taxPercentage / 100;

            billAmount =Math.Round((this._totalCallCharges + this._monthlyRental + this._tax - this._toatalDiscount), 2);
        }

        

    }
}

