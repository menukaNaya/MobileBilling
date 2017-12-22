using System;

namespace MobileBilling
{
    public class CDR
    {
        private long _callingPartyNumber;
        private long _calledPartyNumber;
        private DateTime _startingTimeOfTheCall;
        private double _callDurationInSeconds;

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

            _callingPartyNumber = callingPartyNumber;
            _calledPartyNumber = calledPartyNumber;
            _startingTimeOfTheCall = startingTimeOfTheCall;
            _callDurationInSeconds = callDurationInSeconds;
        }

        public long callingPartyNumber
        {
            get { return _callingPartyNumber; }
        }
        public long calledPartyNumber
        {
            get { return _calledPartyNumber; }
        }
        public DateTime startingTimeOfTheCall
        {
            get { return _startingTimeOfTheCall; }
        }
        public double callDurationInSeconds
        {
            get { return _callDurationInSeconds; }
        }

    }
}
