/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

using System.Diagnostics;
using NUnit.Framework;
using SmartPaymentFormat.Account;

namespace SmartPaymentFormat.Tests.Account
{
    [TestFixture]
    public class IBANUtilitiesTest
    {
        [SetUp]
        public void SetUp() {}

        [TearDown]
        public void TearDown() {}
        
        /// <summary>
        /// Test of computeIBANFromBankAccount method, of class IBANUtilities.
        /// </summary>
        [Test]
        public void TestComputeIBANFromBankAccount()
        {
            Debug.WriteLine("ComputeIBANFromBankAccount");

            string prefix = "19";
            string number = "2000145399";
            string bank = "0800";
            CzechBankAccount account = new CzechBankAccount(prefix, number, bank);
            string expResult = "CZ6508000000192000145399";
            string result = account.ToIBANFromCzechBankAccount();

            Assert.AreEqual(expResult, result);
        }

        [Test]
        public void TestComputeIBANFromBankAccount2()
        {
            Debug.WriteLine("ComputeIBANFromBankAccount2");

            string prefix = "178124";
            string number = "4159";
            string bank = "0710";
            CzechBankAccount account = new CzechBankAccount(prefix, number, bank);
            string expResult = "CZ6907101781240000004159";
            string result = account.ToIBANFromCzechBankAccount();

            Assert.AreEqual(expResult, result);
        }
    }
}