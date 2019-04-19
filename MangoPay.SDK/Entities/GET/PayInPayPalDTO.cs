﻿
namespace MangoPay.SDK.Entities.GET
{
	public class PayInPayPalDTO : PayInDTO
	{
		/// <summary>The shipping address for PayPal PayIn.</summary>
		public ShippingAddress ShippingAddress { get; set; }

		///<summary>The email address of the paypal buyer's account</summary>
		public string PaypalBuyerAccountEmail { get; set; }
	}
}