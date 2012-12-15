/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using SmartPaymentFormat.Utilities;

namespace SmartPaymentFormat.Tests.Utilities
{
    public class SmartPaymentValidatorTest
    {
        [SetUp]
        public void SetUp() {}

        [TearDown]
        public void TearDown() {}

        /// <summary>
        /// Test of validatePaymentString method, of class SmartPaymentValidator.
        /// </summary>
        [Test]
        public void TestValidatePaymentStringBasic()
        {
            Debug.WriteLine("validatePaymentString");

            string paymentString = "SPD*1.0*232131";
            List<SmartPaymentValidationError> result = SmartPaymentValidator.ValidatePaymentString(paymentString);
            // 1 error is expected
            Assert.AreEqual(result.Count, 1);
        }

        /// <summary>
        /// Test of validatePaymentString method, of class SmartPaymentValidator.
        /// </summary>
        [Test]
        public void TestValidatePaymentStringSimpleCorrect()
        {
            Debug.WriteLine("validatePaymentString");
            string paymentString = "SPD*1.0*ACC:CZ3155000000001043006511";
            List<SmartPaymentValidationError> result = SmartPaymentValidator.ValidatePaymentString(paymentString);
            // 0 error is expected
            if (result != null && result.Count > 0)
            {
                Debug.WriteLine(result[0].ErrorDescription);
            }
            Assert.Null(result);
        }

        /// <summary>
        /// Test of generating Paylibo string with alternate account
        /// </summary>
        [Test]
        public void TestValidatePaymentStringAlternateAccounts()
        {
            Debug.WriteLine("testValidatePaymentStringAlternateAccounts");
            string paymentString = "SPD*1.0*ACC:CZ3155000000001043006511+RBCZ66*ALT-ACC:CZ3155000000001043006511+RBCZ66,CZ3155000000001043006511+RBCZ66,CZ3155000000001043006511+RBCZ66";
            List<SmartPaymentValidationError> result = SmartPaymentValidator.ValidatePaymentString(paymentString);
            // 0 error is expected
            if (result != null && result.Count > 0)
            {
                Debug.WriteLine(result[0].ErrorDescription);
            }
            Assert.Null(result);
        }

        /// <summary>
        /// Test of the situation with "**" in the string.
        /// </summary>
        [Test]
        public void TestDoubleStarInString()
        {
            Debug.WriteLine("testDoubleStarInString");
            string paymentString = "SPD*1.0*AM:100**ACC:CZ05678876589087329";
            List<SmartPaymentValidationError> result = SmartPaymentValidator.ValidatePaymentString(paymentString);
            // 0 error is expected
            if (result != null && result.Count > 0)
            {
                Debug.WriteLine(result[0].ErrorDescription);
            }
            Assert.AreEqual(result.Count, 1);
        }
    }
}