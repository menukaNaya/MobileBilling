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
            Customer customerForTest = new Customer("FirstName SecondName", 0711535724, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest1 = new Customer("FirstName SecondName", 0711593911, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest2 = new Customer("FirstName SecondName", 0711593912, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest3 = new Customer("FirstName SecondName", 0711593913, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest4 = new Customer("FirstName SecondName", 0711593914, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest5 = new Customer("FirstName SecondName", 0711593915, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest6 = new Customer("FirstName SecondName", 0711593916, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));
            Customer customerForTest7 = new Customer("FirstName SecondName", 0711593917, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0));

            listOfCustomers.Add(customerForTest);
            listOfCustomers.Add(customerForTest1);
            listOfCustomers.Add(customerForTest2);
            listOfCustomers.Add(customerForTest3);
            listOfCustomers.Add(customerForTest4);
            listOfCustomers.Add(customerForTest5);
            listOfCustomers.Add(customerForTest6);
            listOfCustomers.Add(customerForTest7);

            _sut = new BillingEngine(listOfCustomers);
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLocalCallInPeakHoursLessThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711535724, 0711593911, new DateTime(2017, 12, 23, 9, 0, 0), 58);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711535724" +
                "\nAddress: Address1, Address2." +
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

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593911" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 130.8";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593911).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLocalCallInOffPeakHoursLessThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593912, 0711535724, new DateTime(2017, 12, 23, 21, 0, 0), 58);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593912" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 122.4";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593912).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLocalCallInOffPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593913, 0711535724, new DateTime(2017, 12, 23, 21, 0, 0), 185.6);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593913" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 129.6";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593913).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLongDistanceCallInPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593914, 0721535724, new DateTime(2017, 12, 23, 9, 0, 0), 128);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593914" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 138";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593914).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLongDistanceCallInOffPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593915, 0721535724, new DateTime(2017, 12, 23, 21, 0, 0), 245);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593915" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 144";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593915).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLocalCallInBetweenOffPeakHoursAndPeakHours_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593916, 0711535724, new DateTime(2017, 12, 23, 19, 59, 0), 185);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593916" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 130.8";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593916).printThebill());
        }

        [Test]
        public void Genereate_WhenGivingOneCustomerAndOneCDRWihtLongDistanceCallInBetweenOffPeakHoursAndPeakHours_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711593917, 0721535724, new DateTime(2017, 12, 23, 19, 59, 0), 185);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711593917" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 140.4";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(0711593917).printThebill());
        }
    }
}
