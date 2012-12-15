/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

namespace SmartPaymentFormat.Utilities
{
    public class SmartPaymentValidationError
    {
        public const string ERROR_INVALID_CHARSET = "ERROR_INVALID_CHARSET";
        public const string ERROR_NOT_SPAYD = "ERROR_NOT_SPAYD";
        public const string ERROR_INVALID_STRUCTURE = "ERROR_INVALID_STRUCTURE";
        public const string ERROR_INVALID_AMOUNT = "ERROR_INVALID_AMOUNT";
        public const string ERROR_INVALID_CURRENCY = "ERROR_INVALID_CURRENCY";
        public const string ERROR_INVALID_SENDERS_REFERENCE = "ERROR_INVALID_SENDERS_REFERENCE";
        public const string ERROR_INVALID_RECIPIENT_NAME = "ERROR_INVALID_RECIPIENT_NAME";
        public const string ERROR_INVALID_DUE_DATE = "ERROR_INVALID_DUE_DATE";
        public const string ERROR_INVALID_PAYMENT_TYPE = "ERROR_INVALID_PAYMENT_TYPE";
        public const string ERROR_INVALID_MESSAGE = "ERROR_INVALID_MESSAGE";
        public const string ERROR_INVALID_KEY_FOUND = "ERROR_INVALID_KEY_FOUND";
        public const string ERROR_INVALID_IBAN = "ERROR_INVALID_IBAN";
        public const string ERROR_INVALID_ALTERNATE_IBAN = "ERROR_INVALID_ALTERNATE_IBAN";
        public const string ERROR_IBAN_NOT_FOUND = "ERROR_IBAN_NOT_FOUND";
        public const string ERROR_INVALID_NOTIFICATION_TYPE = "ERROR_INVALID_NOTIFICATION_TYPE";
        public const string ERROR_REQUEST_GENERIC = "ERROR_REQUEST_GENERIC";

        /// <summary>
        /// ErrorCode
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// ErrorDescription
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}