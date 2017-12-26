using System;
using System.Collections.Generic;
using MobileBilling;
using NUnit.Framework;

namespace MobileBillingTests
{
    [TestFixture]
    public class BillingEngineTests
    {
        
        private List <CDR> listOfCallDetails = new List<CDR>();
        private List <Customer> listOfCustomers = new List<Customer>();
        BillingEngine _sut;


        [SetUp]
        public void Init()
        {
            Customer customerForTest = new Customer("Menuka Nayanadeepa", 0711535724, "Pallekekulawala, Nawathalwaththa.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest1 = new Customer("Menuka Nayanadeepa", 0711593911, "Pallekekulawala, Nawathalwaththa.", new DateTime(2010, 5, 27, 10, 0, 0));
            listOfCustomers.Add(customerForTest);
            listOfCustomers.Add(customerForTest1);
            _sut = new BillingEngine(listOfCustomers);
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLocalCallInPeakHoursLessThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 23, 9, 0, 0), 58);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: Menuka Nayanadeepa" +
                "\nPhone number: 711535724" +
                "\nAddress: Pallekekulawala, Nawathalwaththa." +
                "\nTotal Amount to Pay: LKR: 123.6";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(711535724).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLocalCallInPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593911, 0711535724, new DateTime(2017, 12, 23, 9, 0, 0), 123.5);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: Menuka Nayanadeepa" +
                "\nPhone number: 711593911" +
                "\nAddress: Pallekekulawala, Nawathalwaththa." +
                "\nTotal Amount to Pay: LKR: 130.8";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593911).printThebill());
        }
    }
}
