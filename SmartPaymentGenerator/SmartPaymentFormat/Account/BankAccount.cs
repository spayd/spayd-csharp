/**
 *  Copyright (c) 2012, SmartPayment (www.SmartPayment.com).
 */

namespace SmartPaymentFormat.Account
{
    public abstract class BankAccount
    {
        /// <summary>
        /// IBAN
        /// </summary>
        public virtual string IBAN { get; set; }

        /// <summary>
        /// BIC
        /// </summary>
        public virtual string BIC { get; set; }

        protected BankAccount() {}

        protected BankAccount(string iban)
        {
            IBAN = iban;
        }

        protected BankAccount(string iban, string bic)
        {
            IBAN = iban;
            BIC = bic;
        }
    }
}