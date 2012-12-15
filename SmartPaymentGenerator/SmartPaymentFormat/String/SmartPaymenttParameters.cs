using System;
using System.Collections.Generic;
using SmartPaymentFormat.Account;

namespace SmartPaymentFormat.String
{
    public class SmartPaymentParameters
    {
        public enum PaymentNotificationType
        {
            Email,
            Phone
        }

        /// <summary>
        /// AlternateAccount
        /// </summary>
        public List<BankAccount> AlternateAccounts { get; set; }

        /// <summary>
        /// BankAccount
        /// </summary>
        public BankAccount BankAccount { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// RecipientName
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// SendersReference
        /// </summary>
        public string SendersReference { get; set; }

        /// <summary>
        /// PaymentType
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// NotificationType
        /// </summary>
        public PaymentNotificationType? NotificationType { get; set; }

        /// <summary>
        /// NotificationValue
        /// </summary>
        public string NotificationValue { get; set; }

        /// <summary>
        /// Crc32
        /// </summary>
        public string Crc32 { get; set; }
    }
}
