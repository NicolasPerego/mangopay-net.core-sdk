﻿
using System;

namespace MangoPay.SDK.Core.Enumerations
{
	/// <summary>KYC document type enumeration.</summary>
	[Flags]
	public enum KycDocumentType
	{
		/// <summary>Not specified.</summary>
		NotSpecified = 0x00,

		/// <summary>Only for natural users. ID of the individual duly empowered to act on behalf of the legal entity.</summary>
		IDENTITY_PROOF = 0x01,

		/// <summary>Only for legal users. Extract from the relevant register of commerce issued within the last three months.</summary>
		REGISTRATION_PROOF = 0x02,

		/// <summary>Only for legal users. It’s the Statute. Formal memorandum stated by the entrepreuneurs, in which the following information is mentioned:business name, activity, registered address, shareholding.</summary>
		ARTICLES_OF_ASSOCIATION = 0x04,

		/// <summary>Only for legal users (business company).</summary>
		SHAREHOLDER_DECLARATION = 0x08,

		/// <summary>Only for natural users.</summary>
		ADDRESS_PROOF = 0x10
	}
}
