using System;
using MobileBilling;
using NUnit.Framework;

namespace MobileBillingTests
{

    [TestFixture]
    public class CDRTests
    {
        //private CDR _sut;
        DateTime date = new DateTime(2004, 5, 27, 0, 0, 0);

        [SetUp]
        public void Init()
        {
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
            //Assert.Inconclusive();
        }

    }
}
