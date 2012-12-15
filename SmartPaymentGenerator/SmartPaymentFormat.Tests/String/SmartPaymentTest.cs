using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using SmartPaymentFormat.Account;
using SmartPaymentFormat.String;

namespace SmartPaymentFormat.Tests.String
{
    public class SmartPaymentTest
    {
        [SetUp]
        public void SetUp() {}

        [TearDown]
        public void TearDown() {}

        /// <summary>
        /// Test of paymentStringFromAccount method, of class SmartPayment.
        /// </summary>
        [Test]
        public void TestPaymentStringFromAccount()
        {
            Debug.WriteLine("paymentStringFromAccount");

            SmartPaymentParameters parameters = new SmartPaymentParameters();
            parameters.BankAccount = new CzechBankAccount("19", "123", "0800");
            SmartPaymentMap extendedParameters = null;
            bool transliterateParams = false;
            string expResult = "SPD*1.0*ACC:CZ2408000000190000000123";
            string result = SmartPayment.PaymentStringFromAccount(parameters, extendedParameters, transliterateParams);

            Assert.AreEqual(expResult, result);
        }
        
        [Test]
        public void TestSpecialCharacterEscaping()
        {
            string original = "abc  123\u2665\u2620**123  abc-+ěščřžýáíé---%20";
            Debug.WriteLine(SmartPayment.EscapeDisallowedCharacters(original));

            string expected = "abc  123%E2%99%A5%E2%98%A0%2A%2A123  abc-%2B%C4%9B%C5%A1%C4%8D%C5%99%C5%BE%C3%BD%C3%A1%C3%AD%C3%A9---%2520"; // TODO is this right?

            Assert.AreEqual(expected, SmartPayment.EscapeDisallowedCharacters(original));
        }

        /// <summary>
        /// Test of paymentStringFromAccount method, of class SmartPayment.
        /// </summary>
        [Test]
        public void TestPaymentStringFromAccountAmountAndAlternateAccounts()
        {
            Debug.WriteLine("paymentStringFromAccount");

            SmartPaymentParameters parameters = new SmartPaymentParameters();
            parameters.BankAccount = new CzechBankAccount("19", "123", "0800");

            List<BankAccount> alternateAccounts = new List<BankAccount>();
            alternateAccounts.Add(new CzechBankAccount(null, "19", "5500"));
            alternateAccounts.Add(new CzechBankAccount(null, "19", "0100"));
            parameters.AlternateAccounts = alternateAccounts;
            parameters.Amount = 100.5m;
            SmartPaymentMap extendedParameters = null;
            bool transliterateParams = false;
            string expResult = "SPD*1.0*ACC:CZ2408000000190000000123*ALT-ACC:CZ9755000000000000000019,CZ7301000000000000000019*AM:100.5";
            string result = SmartPayment.PaymentStringFromAccount(parameters, extendedParameters, transliterateParams);

            Debug.WriteLine(result);
            Assert.AreEqual(expResult, result);
        }
    }
}