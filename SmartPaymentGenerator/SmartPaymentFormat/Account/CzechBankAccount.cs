/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

using System;

namespace SmartPaymentFormat.Account
{
    public class IllegalArgumentException : ArgumentException
    {
        public IllegalArgumentException(string message) : base(message) {}
    }

    public class CzechBankAccount : BankAccount
    {
        /// <summary>
        /// IBAN
        /// </summary>
        public override string IBAN
        {
            get { return this.ToIBANFromCzechBankAccount(); }
            set
            {
                bankCode = value.Substring(4, 8);
                number = value.Substring(16, 26);
                prefix = value.Substring(9, 15);
            }
        }

        /// <summary>
        /// BankCode
        /// </summary>
        public string BankCode
        {
            get { return bankCode; }
            set
            {
                ValidateAccountParameters(null, null, value);
                bankCode = value;
            }
        }
        private string bankCode;

        /// <summary>
        /// Number
        /// </summary>
        public string Number
        {
            get { return number; }
            set
            {
                ValidateAccountParameters(null, value, null);
                number = value;
            }
        }
        private string number;

        /// <summary>
        /// Prefix
        /// </summary>
        public string Prefix
        {
            get { return prefix ?? "000000"; }
            set
            {
                ValidateAccountParameters(value, null, null);
                prefix = value;
            }
        }
        private string prefix;

        public CzechBankAccount(string prefix, string number, string bankCode)
        {
            ValidateAccountParameters(prefix, number, bankCode);
            this.prefix = prefix;
            this.number = number;
            this.bankCode = bankCode;
        }

        private bool validateMod11(string number)
        {
            int weight = 1;
            int sum = 0;
            for (int k = number.Length - 1; k >= 0; k--)
            {
                sum += (number[k] - '0') * weight;
                weight *= 2;
            }

            return (sum % 11) == 0;
        }

        /// <exception cref="IllegalArgumentException"></exception>
        private void ValidateAccountParameters(string prefix, string number, string bankCode)
        {
            if (prefix != null)
            {
                for (int i = 0; i < prefix.Length; i++)
                {
                    if (prefix[i] < 0 && prefix[i] > 9)
                    {
                        throw new IllegalArgumentException("Czech account number (prefix) must be numeric.");
                    }
                }
                if (!validateMod11(prefix))
                {
                    throw new IllegalArgumentException("Czech account number (prefix) must pass bank mod 11 test.");
                }
            }
            if (number != null)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] < 0 && number[i] > 9)
                    {
                        throw new IllegalArgumentException("Czech account number (basic part) must be numeric.");
                    }
                }
                if (!validateMod11(number))
                {
                    throw new IllegalArgumentException("Czech account number (basic part) must pass bank mod 11 test.");
                }
            }
            if (bankCode != null)
            {
                for (int i = 0; i < bankCode.Length; i++)
                {
                    if (bankCode[i] < 0 && bankCode[i] > 9)
                    {
                        throw new IllegalArgumentException("Czech account number (bank code) must be numeric.");
                    }
                }
            }
        }
    }
}
