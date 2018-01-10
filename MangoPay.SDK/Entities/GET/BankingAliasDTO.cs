﻿using MangoPay.SDK.Core.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MangoPay.SDK.Entities.GET
{
	public class BankingAliasDTO : EntityBase
	{
		/// <summary>The user ID who was credited.</summary>
		public String CreditedUserId { get; set; }

		/// <summary>The ID of wallet.</summary>
		public String WalletId { get; set; }

		/// <summary>The type of banking alias.</summary>
		[JsonConverter(typeof(StringEnumConverter))]
		public BankingAliasType Type { get; set; }

		/// <summary>Owner name.</summary>
		public String OwnerName { get; set; }

		/// <summary>Whether the banking alias is active or not.</summary>
		public bool Active { get; set; }
	}
}
