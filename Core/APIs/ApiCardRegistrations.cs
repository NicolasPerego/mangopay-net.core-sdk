﻿using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.POST;
using MangoPay.SDK.Entities.PUT;
using System;
using System.Threading.Tasks;

namespace MangoPay.SDK.Core.APIs
{
    /// <summary>API for card registrations.</summary>
    public class ApiCardRegistrations : ApiBase
    {
        /// <summary>Instantiates new ApiCardRegistration object.</summary>
        /// <param name="root">Root/parent instance that holds the OAuthToken and Configuration instance.</param>
        public ApiCardRegistrations(MangoPayApi root) : base(root) { }

        /// <summary>Creates new card registration.</summary>
        /// <param name="cardRegistration">Card registration object to create.</param>
        /// <returns>Card registration object returned from API.</returns>
        public async Task<CardRegistrationDTO> Create(CardRegistrationPostDTO cardRegistration)
        {
            return await Create(null, cardRegistration);
        }

		/// <summary>Creates new card registration.</summary>
		/// <param name="idempotencyKey">Idempotency key for this request.</param>
		/// <param name="cardRegistration">Card registration object to create.</param>
		/// <returns>Card registration object returned from API.</returns>
		public async Task<CardRegistrationDTO> Create(String idempotencyKey, CardRegistrationPostDTO cardRegistration)
		{
			return await this.CreateObject<CardRegistrationDTO, CardRegistrationPostDTO>(idempotencyKey, MethodKey.CardRegistrationCreate, cardRegistration);
		}

        /// <summary>Gets card registration.</summary>
        /// <param name="cardRegistrationId">Card registration identifier.</param>
        /// <returns>Card registration instance returned from API.</returns>
        public async Task<CardRegistrationDTO> Get(String cardRegistrationId)
        {
            return await this.GetObject<CardRegistrationDTO>(MethodKey.CardRegistrationGet, cardRegistrationId);
        }

        /// <summary>Updates card registration.</summary>
        /// <param name="cardRegistration">Card registration instance to be updated.</param>
        /// <param name="cardRegistrationId">Card registration identifier.</param>
        /// <returns>Card registration object returned from API.</returns>
        public async Task<CardRegistrationDTO> Update(CardRegistrationPutDTO cardRegistration, String cardRegistrationId)
        {
            return await this.UpdateObject<CardRegistrationDTO, CardRegistrationPutDTO>(MethodKey.CardRegistrationSave, cardRegistration, cardRegistrationId);
        }
    }
}
