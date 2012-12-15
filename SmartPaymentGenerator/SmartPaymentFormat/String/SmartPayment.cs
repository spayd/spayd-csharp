using System;
using System.Globalization;
using System.Linq;
using SmartPaymentFormat.Account;

namespace SmartPaymentFormat.String
{
    public class SmartPayment
    {
        private const string protocolVersion = "1.0";

        private static readonly CultureInfo enUS = new CultureInfo("en-US"); 

        public static string EscapeDisallowedCharacters(string originalString)
        {
            string working = "";
            for (int i = 0; i < originalString.Length; i++)
            {
                if (originalString[i] > 127)
                {
                    // escape non-ascii characters
                    working += Uri.EscapeDataString("" + originalString[i]);
                }
                else
                {
                    if (originalString[i] == '*')
                    {
                        // star is a special character for the SPAYD format
                        working += "%2A";
                    }
                    else if (originalString[i] == '+')
                    {
                        // plus is a special character for URL encode
                        working += "%2B";
                    }
                    else if (originalString[i] == '%')
                    {
                        // percent is an escape character
                        working += "%25";
                    }
                    else
                    {
                        working += originalString[i]; // ascii characters may be used as expected
                    }
                }
            }
            return working;
        }

        public static string PaymentStringFromAccount(SmartPaymentParameters parameters, SmartPaymentMap extendedParameters, bool transliterateParams)
        {
            string paymentString = "SPD*" + protocolVersion + "*";
            if (parameters.BankAccount.IBAN != null)
            {
                paymentString += "ACC:" + parameters.BankAccount.IBAN;
                if (parameters.BankAccount.BIC != null)
                {
                    paymentString += "+" + parameters.BankAccount.BIC;
                }
                paymentString += "*";
            }
            if (parameters.AlternateAccounts != null && parameters.AlternateAccounts.Any())
            {
                paymentString += "ALT-ACC:";
                bool firstItem = true;
                foreach (BankAccount bankAccount in parameters.AlternateAccounts)
                {
                    if (!firstItem)
                    {
                        paymentString += ",";
                    }
                    else
                    {
                        firstItem = false;
                    }
                    paymentString += bankAccount.IBAN;
                    if (bankAccount.BIC != null)
                    {
                        paymentString += "+" + bankAccount.BIC;
                    }
                }
                paymentString += "*";
            }
            if (parameters.Amount.HasValue)
            {
                paymentString += string.Format(enUS, "AM:{0}*", parameters.Amount.Value);
            }
            if (parameters.Currency != null)
            {
                paymentString += "CC:" + parameters.Currency + "*";
            }
            if (parameters.SendersReference != null)
            {
                paymentString += "RF:" + parameters.SendersReference + "*";
            }
            if (parameters.RecipientName != null)
            {
                if (transliterateParams)
                {
                    paymentString += "RN:"
                            + EscapeDisallowedCharacters(parameters.RecipientName.ToUpper()) // TODO diacritics removing
                            + "*";
                }
                else
                {
                    paymentString += "RN:"
                            + EscapeDisallowedCharacters(parameters.RecipientName)
                            + "*";
                }
            }
            if (parameters.Date.HasValue)
            {
                paymentString += string.Format("DT:{0:yyyyMMdd}*", parameters.Date.Value); // TODO validate format
            }
            if (parameters.Message != null)
            {
                if (transliterateParams)
                {
                    paymentString += "MSG:"
                            + EscapeDisallowedCharacters(parameters.Message.ToUpper()) // TODO diacritics removing
                            + "*";
                }
                else
                {
                    paymentString += "MSG:"
                            + EscapeDisallowedCharacters(parameters.Message)
                            + "*";
                }
            }
            if (parameters.NotificationType.HasValue)
            {
                switch (parameters.NotificationType.Value)
                {
                    case SmartPaymentParameters.PaymentNotificationType.Email:
                        paymentString += "NT:E*";
                        break;
                    case SmartPaymentParameters.PaymentNotificationType.Phone:
                        paymentString += "NT:P*";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (parameters.NotificationValue != null)
            {
                paymentString += "NTA:" + EscapeDisallowedCharacters(parameters.NotificationValue) + "*";
            }
            if (extendedParameters != null && extendedParameters.Any())
            {
                paymentString += extendedParameters.ToExtendedParams();
            }
            return paymentString.Substring(0, paymentString.Length - 1);
        }
    }
}