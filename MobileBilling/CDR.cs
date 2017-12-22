using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBilling
{
    public class CDR
    {
        private long _callingPartyNumber { get; }
        private long _calledPartyNumber { get; }
        private DateTime _startingTimeOfTheCall { get; }
        private double _callDurationInSeconds { get; }

        public CDR(long callingPartyNumber, long calledPartyNumber, DateTime startingTimeOfTheCall, double callDurationInSeconds)
        {
            if (callingPartyNumber > 999999999 || callingPartyNumber < 111111111 || calledPartyNumber > 999999999 || calledPartyNumber < 111111111)
            {
                throw new ArgumentOutOfRangeException(nameof(callingPartyNumber), "Invalid Phone Number!");
            }

            if (callDurationInSeconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(callDurationInSeconds), "Invalid Input! Seconds can't be minus!");
            }

            if (callingPartyNumber.Equals(calledPartyNumber))
            {
                throw new Exception( "Calling party number and Called party number can't be same!");
            }

            this._callingPartyNumber = callingPartyNumber;
            this._calledPartyNumber = calledPartyNumber;
            this._startingTimeOfTheCall = startingTimeOfTheCall;
            this._callDurationInSeconds = callDurationInSeconds;
        }

    }
}
