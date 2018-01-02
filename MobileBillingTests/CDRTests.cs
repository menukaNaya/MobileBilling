using System;
using MobileBilling;
using NUnit.Framework;

namespace MobileBillingTests
{

    [TestFixture]
    public class CDRTests
    {
        private CDR _sut;
        DateTime date = new DateTime(2004, 5, 27, 0, 0, 0);

        [SetUp]
        public void Init()
        {
            _sut = new CDR(0711535724, 0711593911, date, 65);
        }

        [Test]
        public void CDRConstructor_WhenGivingValidPhoneNumbersAndValidCallDuration_ShouldThrowAnException()
        {

            Assert.AreEqual(0711535724, _sut.callingPartyNumber);
            Assert.AreEqual(0711593911, _sut.calledPartyNumber);
            Assert.AreEqual(date, _sut.startingTimeOfTheCall);
            Assert.AreEqual(65, _sut.callDurationInSeconds);

        }

        [Test]
        public void CDRConstructor_WhenGivingInvalidPhoneNumbers_ShouldThrowAnException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CDR(071153, -5, date, 23423.234));
        }

        [Test]
        public void CDRConstructor_WhenGivingWrongTimeDurations_ShouldThrowAnException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CDR(0711535724, 0711593911, date, -8));
        }

        [Test]
        public void CDRConstructor_WhenTheCallingNumberAndCalledNumberIsSame_ShouldThrowAnException()
        {
            Assert.Throws<Exception>(() => new CDR(0711535724, 0711535724, date, 55));
        }

    }
}
