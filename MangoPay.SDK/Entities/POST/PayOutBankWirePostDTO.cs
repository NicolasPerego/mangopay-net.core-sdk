﻿using MangoPay.SDK.Core;
using System;

namespace MangoPay.SDK.Entities.POST
{
    /// <summary>PayOut bank wire POST entity.</summary>
    public class PayOutBankWirePostDTO : EntityPostBase
    {
        public PayOutBankWirePostDTO(string authorId, string debitedWalletId, Money debitedFunds, Money fees, string bankAccountId, string bankWireRef)
        {
            AuthorId = authorId;
            DebitedWalletId = debitedWalletId;
            DebitedFunds = debitedFunds;
            Fees = fees;
            BankAccountId = bankAccountId;
            BankWireRef = bankWireRef;
        }

        /// <summary>Author identifier.</summary>
        public String AuthorId { get; set; }

        /// <summary>Debited wallet identifier.</summary>
        public String DebitedWalletId { get; set; }

        /// <summary>Debited funds.</summary>
        public Money DebitedFunds { get; set; }

        /// <summary>Fees.</summary>
        public Money Fees { get; set; }

        /// <summary>Bank account identifier.</summary>
        public String BankAccountId { get; set; }

        /// <summary>Credited user identifier.</summary>
        public String CreditedUserId { get; set; }

        /// <summary>A custom reference you wish to appear on the user’s bank statement (your ClientId is already shown).</summary>
        public String BankWireRef { get; set; }
    }
}
