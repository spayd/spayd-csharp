/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

using System;

namespace SmartPaymentFormat.Account
{
    public static class IBANUtilities
    {
        /// <summary>
        /// Computes the IBAN number from a given Czech account information.
        /// </summary>
        /// <param name="account">A Czech account model class.</param>
        /// <returns>An IBAN number.</returns>
        public static string ToIBANFromCzechBankAccount(this CzechBankAccount account)
        {
            // preprocess the numbers
            string prefix = string.Format("{0:000000}", long.Parse(account.Prefix));
            string number = string.Format("{0:0000000000}", long.Parse(account.Number));
            string bank = string.Format("{0:0000}", long.Parse(account.BankCode));

            // calculate the check sum
            string buf = bank + prefix + number + "123500";
            int index = 0;
            long pz = -1;
            while (index <= buf.Length)
            {
                string dividend;
                if (pz < 0)
                {
                    dividend = buf.Substring(index, Math.Min(9, buf.Length - index));
                    index += 9;
                }
                else if (pz >= 0 && pz <= 9)
                {
                    dividend = pz + buf.Substring(index, Math.Min(8, buf.Length - index));
                    index += 8;
                }
                else
                {
                    dividend = pz + buf.Substring(index, Math.Min(7, buf.Length - index));
                    index += 7;
                }
                pz = long.Parse(dividend) % 97;
            }
            pz = 98 - pz;

            // assign the checksum
            string checksum = string.Format("{0:00}", pz);

            // build the IBAN number
            return "CZ" + checksum + bank + prefix + number;
        }
    }
}