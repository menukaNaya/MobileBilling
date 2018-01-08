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
            Customer customerForTest = new Customer("FirstName SecondName", 0711535724, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest1 = new Customer("FirstName SecondName", 0711593911, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest2 = new Customer("FirstName SecondName", 0711593912, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest3 = new Customer("FirstName SecondName", 0711593913, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest4 = new Customer("FirstName SecondName", 0711593914, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest5 = new Customer("FirstName SecondName", 0711593915, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest6 = new Customer("FirstName SecondName", 0711593916, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest7 = new Customer("FirstName SecondName", 0711593917, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'A');
            Customer customerForTest8 = new Customer("FirstName SecondName", 0711535725, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'B');
            Customer customerForTest9 = new Customer("FirstName SecondName", 0711535726, "Address1, Address2.", new DateTime(2010, 5, 27, 10, 0, 0), 'B');

            listOfCustomers.Add(customerForTest);
            listOfCustomers.Add(customerForTest1);
            listOfCustomers.Add(customerForTest2);
            listOfCustomers.Add(customerForTest3);
            listOfCustomers.Add(customerForTest4);
            listOfCustomers.Add(customerForTest5);
            listOfCustomers.Add(customerForTest6);
            listOfCustomers.Add(customerForTest7);
            listOfCustomers.Add(customerForTest8);
            listOfCustomers.Add(customerForTest9);

            _sut = new BillingEngine(listOfCustomers);
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLocalCallInPeakHoursLessThanAMiniuteCallDuration_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(711535724).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLocalCallInPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593911).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLocalCallInOffPeakHoursLessThanAMiniuteCallDuration_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593912).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLocalCallInOffPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593913).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLongDistanceCallInPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593914).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLongDistanceCallInOffPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593915).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLocalCallInBetweenOffPeakHoursAndPeakHours_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593916).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageACustomerAndOneCDRWihtLongDistanceCallInBetweenOffPeakHoursAndPeakHours_ShouldReturnTheBill()
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
            Assert.AreEqual(expected, _sut.getTheBill(0711593917).PrintThebill());
        }

        //Plan B Tests starting form here...
        [Test]
        public void Genereate_WhenGivingPackageBCustomerAndOneCDRWihtLocalCallInPeakHoursLessThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711535725, 0711593911, new DateTime(2017, 12, 23, 9, 0, 0), 58);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711535725" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 124.64";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(711535725).PrintThebill());
        }

        [Test]
        public void Genereate_WhenGivingPackageBCustomerAndOneCDRWihtLongDistanceCallInOffPeakHoursMoreThanAMiniuteCallDuration_ShouldReturnTheBill()
        {
            // Arrange
            CDR cdrForTest = new CDR(0711535726, 0721535724, new DateTime(2017, 12, 23, 21, 0, 0), 245);
            listOfCallDetails.Add(cdrForTest);

            string expected = "Customer Name: FirstName SecondName" +
                "\nPhone number: 711535726" +
                "\nAddress: Address1, Address2." +
                "\nTotal Amount to Pay: LKR: 144.5";

            // Act
            _sut.Generate(listOfCallDetails);

            // Assert
            Assert.AreEqual(expected, _sut.getTheBill(711535726).PrintThebill());
        }
    }
}
