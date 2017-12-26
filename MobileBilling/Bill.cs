using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    public class Bill
    {
        private Customer customer;
        private double _totalCallCharges;
        private double _taxPercentage;
        private double _toatalDiscount;
        private double _tax;
        private double _rental;
        private double _billAmount;
        private double _peakHoursLocalPerMiniuteCharge;
        private double _peakHoursLongDistancePerMiniuteCharge;
        private double _offPeakHoursLocalPerMiniuteCharge;
        private double _offPeakHoursLongDistancePerMiniuteCharge;
        private List<CDR> listOfCallDetails;


        public Bill(Customer customer)
        {
            this.customer = customer;
            this._taxPercentage = 20;
            this._peakHoursLocalPerMiniuteCharge = 3;
            this._offPeakHoursLocalPerMiniuteCharge = 2;
            this._peakHoursLongDistancePerMiniuteCharge = 5;
            this._offPeakHoursLongDistancePerMiniuteCharge = 4;
            this._rental = 100;
            listOfCallDetails = new List<CDR>();
        }

        public Bill(Customer customer, double taxPercentage, double rental)
        {
            this.customer = customer;
            this._taxPercentage = taxPercentage;
            this._rental = rental;
            listOfCallDetails = new List<CDR>();
        }

        public void AddCDRToList(CDR cdr)
        {
            listOfCallDetails.Add(cdr);
        }

        public void CalculateTheBill()
        {
            foreach (CDR cdr in listOfCallDetails)
            {
                int totalMiniutes = (int) (cdr.callDurationInSeconds / 60);
                if(cdr.callDurationInSeconds % 60 > 0)
                {
                    totalMiniutes++;
                }

                //Console.WriteLine(totalMiniutes);

                if((cdr.startingTimeOfTheCall.Hour) >= 8 && (cdr.startingTimeOfTheCall.Hour < 16))
                {
                    this._totalCallCharges += totalMiniutes * this._peakHoursLocalPerMiniuteCharge;
                }

                //Console.WriteLine(this._totalCallCharges);

            }

            this._tax = (this._totalCallCharges + this._rental) * this._taxPercentage / 100;
            //Console.WriteLine(this._tax);

            this._billAmount = this._totalCallCharges + this._rental + this._tax - this._toatalDiscount;
        }

        public double billAmount
        {
            get { return _billAmount; }
        }

        public string printThebill()
        {
            return "Customer Name: " + customer.fullName +
                "\nPhone number: " + customer.phoneNumber +
                "\nAddress: " + customer.billingAddress +
                "\nTotal Amount to Pay: LKR: " + this._billAmount;
        }
    }
}
