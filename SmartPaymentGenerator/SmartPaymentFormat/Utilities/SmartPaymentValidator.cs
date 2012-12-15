/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartPaymentFormat.Utilities
{
    public class SmartPaymentValidator
    {
        private static readonly string[] allowedKeys =
        {
            "ACC",
            "ALT-ACC",
            "AM",
            "CC",
            "RF",
            "RN",
            "DT",
            "PT",
            "MSG",
            "NT",
            "NTA",
            "CRC32"
        };

        private static readonly CultureInfo enUS = new CultureInfo("en-US"); 

        public static List<SmartPaymentValidationError> ValidatePaymentString(string paymentString)
        {
            List<SmartPaymentValidationError> errors = new List<SmartPaymentValidationError>();

            // TODO charset validation
/*            if (!Charset.forName("ISO-8859-1").newEncoder().canEncode(paymentString))
            {
                // check encoding
                SmartPaymentValidationError error = new SmartPaymentValidationError();
                error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_CHARSET;
                error.ErrorDescription = "Invalid charset - only ISO-8859-1 characters must be used";
                errors.Add(error);
                return errors;
            }*/

            if (!Regex.IsMatch(paymentString, "^SPD\\*[0-9]+\\.[0-9]+\\*.*"))
            {
                SmartPaymentValidationError error = new SmartPaymentValidationError();
                error.ErrorCode = SmartPaymentValidationError.ERROR_NOT_SPAYD;
                error.ErrorDescription = "Invalid data prefix - SPD*{$VERSION}* expected.";
                errors.Add(error);
                return errors;
            }

            if (!Regex.IsMatch(paymentString, "^SPD\\*[0-9]+\\.[0-9]+(\\*[0-9A-Z $%*+-.]+:[^\\*]+)+\\*?$"))
            {
                SmartPaymentValidationError error = new SmartPaymentValidationError();
                error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_STRUCTURE;
                error.ErrorDescription = "Payment string code didn't pass the basic regexp validation.";
                errors.Add(error);
                return errors;
            }

            IList<string> allowedKeyList = allowedKeys;
            string[] components = paymentString.Split(new[] {"*"}, StringSplitOptions.None);

            bool ibanFound = false;

            // skip the header and version => start with 2
            for (int i = 2; i < components.Length; i++)
            {
                int index = components[i].IndexOf(":");
                if (index == -1)
                {
                    // missing pair between two stars ("**")
                    SmartPaymentValidationError error = new SmartPaymentValidationError();
                    error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_STRUCTURE;
                    error.ErrorDescription = "Payment string code didn't pass the basic regexp validation.";
                    errors.Add(error);
                    continue;
                }
                string key = components[i].Substring(0, index);
                string value = components[i].Substring(index + 1);

                if (!allowedKeyList.Contains(key) && !key.StartsWith("X-"))
                {
                    SmartPaymentValidationError error = new SmartPaymentValidationError();
                    error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_KEY_FOUND;
                    error.ErrorDescription = "Unknown key detected. Use 'X-' prefix to create your own key.";
                    errors.Add(error);
                    continue;
                }

                if (key.Equals("ACC"))
                {
                    ibanFound = true;
                    if (!Regex.IsMatch(value, "^([A-Z]{2,2}[0-9]+)(\\+([A-Z0-9]+))?$"))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_IBAN;
                        error.ErrorDescription = "IBAN+BIC pair was not in the correct format.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("ALT-ACC"))
                {
                    ibanFound = true;
                    if (!Regex.IsMatch(value, "^([A-Z]{2,2}[0-9]+)(\\+([A-Z0-9]+))?(,([A-Z]{2,2}[0-9]+)(\\+([A-Z0-9]+))?)*$"))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_ALTERNATE_IBAN;
                        error.ErrorDescription = "Alternate accounts are not properly formatted - should be IBAN+BIC list with items separated by ',' character.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("AM"))
                {
                    if (!Regex.IsMatch(value, "^[0-9]{0,10}(\\.[0-9]{0,2})?$") || value.Equals("."))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_AMOUNT;
                        error.ErrorDescription = "Amount must be a number with at most 2 decimal digits.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("CC"))
                {
                    decimal test;
                    if (!decimal.TryParse(value, out test))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_CURRENCY;
                        error.ErrorDescription = "Currency must be a valid currency from ISO 4271.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("RF"))
                {
                    if (value.Length > 16 || value.Length < 1 || !Regex.IsMatch(value, "^[0-9]+$"))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_SENDERS_REFERENCE;
                        error.ErrorDescription = "Senders reference must be a decimal string with length between 1 and 16 characters.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("RN"))
                {
                    if (value.Length > 40 || value.Length < 1)
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_RECIPIENT_NAME;
                        error.ErrorDescription = "Recipient name must be a string with length between 1 and 40 characters.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("NT"))
                {
                    if (!value.Equals("E") && !value.Equals("P"))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_NOTIFICATION_TYPE;
                        error.ErrorDescription = "Notification type must be 'E' (e-mail) or 'P' (phone).";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("DT"))
                {
                    if (!Regex.IsMatch(value, "^[0-9]{8,8}$"))
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_DUE_DATE;
                        error.ErrorDescription = "Due date must be represented as a decimal string in YYYYmmdd format.";
                        errors.Add(error);
                    }
                    else
                    {
                        DateTime test;
                        if (!DateTime.TryParseExact(value, "yyyyMMdd", enUS, DateTimeStyles.AssumeLocal, out test))
                        {
                            SmartPaymentValidationError error = new SmartPaymentValidationError();
                            error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_DUE_DATE;
                            error.ErrorDescription = "Due date must be represented as a decimal string in YYYYmmdd format.";
                            errors.Add(error);
                        }
                    }
                }
                else if (key.Equals("PT"))
                {
                    if (value.Length > 3 || value.Length < 1)
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_PAYMENT_TYPE;
                        error.ErrorDescription = "Payment type must be at represented as a string with length between 1 and 3 characters.";
                        errors.Add(error);
                    }
                }
                else if (key.Equals("MSG"))
                {
                    if (value.Length > 60 || value.Length < 1)
                    {
                        SmartPaymentValidationError error = new SmartPaymentValidationError();
                        error.ErrorCode = SmartPaymentValidationError.ERROR_INVALID_MESSAGE;
                        error.ErrorDescription = "Message must be at represented as a string with length between 1 and 60 characters.";
                        errors.Add(error);
                    }
                }
            }
            if (!ibanFound)
            {
                SmartPaymentValidationError error = new SmartPaymentValidationError();
                error.ErrorCode = SmartPaymentValidationError.ERROR_IBAN_NOT_FOUND;
                error.ErrorDescription = "You must specify an account number.";
                errors.Add(error);
            }
            return errors.Any() ? errors : null;
        }
    }
}