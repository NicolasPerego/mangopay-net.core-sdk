﻿using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.POST;
using MangoPay.SDK.Entities.PUT;
using System;
using System.Threading.Tasks;

namespace MangoPay.SDK.Core.APIs
{
	/// <summary>API for single sign on users.</summary>
	public class ApiSingleSignOns : ApiBase
    {
		/// <summary>Instantiates new ApiSingleSignOns object.</summary>
		/// <param name="root">Root/parent instance that holds the OAuthToken and Configuration instance.</param>
		public ApiSingleSignOns(MangoPayApi root) : base(root) { }

		/// <summary>Gets single sign on user.</summary>
		/// <param name="singleSignOnId">Single sign on user identifier.</param>
		/// <returns>Single sign on user instance returned from API.</returns>
		public async Task<SingleSignOnDTO> Get(String singleSignOnId)
        {
            return await this.GetObject<SingleSignOnDTO>(MethodKey.SingleSignOnGet, singleSignOnId);
        }

		/// <summary>Creates new single sign on user.</summary>
		/// <param name="singleSignOn">Single sign on object to be created.</param>
		/// <returns>Single sign on instance returned from API.</returns>
		public async Task<SingleSignOnDTO> Create(SingleSignOnPostDTO singleSignOn)
		{
			return await Create(null, singleSignOn);
		}

		/// <summary>Creates new user.</summary>
		/// <param name="idempotencyKey">Idempotency key for this request.</param>
		/// <param name="singleSignOn">SingleSignOn object to be created.</param>
		/// <returns>UserNatural instance returned from API.</returns>
		public async Task<SingleSignOnDTO> Create(String idempotencyKey, SingleSignOnPostDTO singleSignOn)
		{
			return await this.CreateObject<SingleSignOnDTO, SingleSignOnPostDTO>(idempotencyKey, MethodKey.SingleSignOnCreate, singleSignOn);
		}

		/// <summary>Gets single sign on users.</summary>
		/// <param name="pagination">Pagination.</param>
		/// <param name="sort">Sort.</param>
		/// <returns>Collection of single sign on user instances.</returns>
		public async Task<ListPaginated<SingleSignOnDTO>> GetAll(Pagination pagination, Sort sort = null)
        {
            return await this.GetList<SingleSignOnDTO>(MethodKey.SingleSignOnAll, pagination, sort);
        }

		/// <summary>Gets first page of single sign on users.</summary>
		/// <returns>Collection of single sign on user instances.</returns>
		public async Task<ListPaginated<SingleSignOnDTO>> GetAll()
        {
            return await GetAll(null);
        }

		/// <summary>Updates the single sign on user.</summary>
		/// <param name="singleSignOn">Instance of single sign on class to be updated.</param>
		/// <param name="singleSignOnId">Single sign on user identifier.</param>
		/// <returns>Updated single sign on user object returned from API.</returns>
		public async Task<SingleSignOnDTO> Update(SingleSignOnPutDTO singleSignOn, String singleSignOnId)
        {
            return await this.UpdateObject<SingleSignOnDTO, SingleSignOnPutDTO>(MethodKey.SingleSignOnSave, singleSignOn, singleSignOnId);
        }

		/// <summary>Extend single sign on invitation.</summary>
		/// <param name="singleSignOnId">Single sign on user identifier.</param>
		/// <returns>Single sign on user object returned from API.</returns>
		public async Task<SingleSignOnDTO> ExtendInvitation(String singleSignOnId)
		{
			return await this.UpdateObject<SingleSignOnDTO, SingleSignOnPutDTO>(MethodKey.SingleSignOnExtendInvitation, new SingleSignOnPutDTO(), singleSignOnId);
		}

		/// <summary>Gets single sign on for the current user.</summary>
		/// <returns>Single sign on user instance returned from API.</returns>
		/// <remarks>Requires an API token associated with an SSO based authorization</remarks>
		public async Task<SingleSignOnDTO> GetSsoForCurrentUser()
		{
			return await this.GetObject<SingleSignOnDTO>(MethodKey.SingleSignOnsMe, null);
		}

		/// <summary>Gets permission group for the current user.</summary>
		/// <returns>Instance of permission group returned from API.</returns>
		/// <remarks>Requires an API token associated with an SSO based authorization</remarks>
		public async Task<PermissionGroupDTO> GetPermissionGroupForCurrentUser()
		{
			return await this.GetObject<PermissionGroupDTO>(MethodKey.SingleSignOnsMePermissionGroup, null);
		}
	}
}
