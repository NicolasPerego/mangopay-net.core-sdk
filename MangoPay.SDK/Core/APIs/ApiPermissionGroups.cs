﻿using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.POST;
using MangoPay.SDK.Entities.PUT;
using System;
using System.Threading.Tasks;

namespace MangoPay.SDK.Core.APIs
{
	public class ApiPermissionGroups : ApiBase
	{
		/// <summary>Instantiates new ApiPermissionGroups object.</summary>
		/// <param name="root">Root/parent instance that holds the OAuthToken and Configuration instance.</param>
		public ApiPermissionGroups(MangoPayApi root) : base(root) { }

		/// <summary>Gets permission group by ID.</summary>
		/// <param name="permissionGroupId">Permission group identifier.</param>
		/// <returns>Permission group instance returned from API.</returns>
		public async Task<PermissionGroupDTO> Get(String permissionGroupId)
		{
			return await this.GetObject<PermissionGroupDTO>(MethodKey.PermissionGroupGet, permissionGroupId);
		}

		/// <summary>Creates new permission group.</summary>
		/// <param name="permissionGroup">Permission group object to be created.</param>
		/// <returns>Permission group instance returned from API.</returns>
		public async Task<PermissionGroupDTO> Create(PermissionGroupPostDTO permissionGroup)
		{
			return await Create(null, permissionGroup);
		}

		/// <summary>Creates new permission group.</summary>
		/// <param name="idempotencyKey">Idempotency key for this request.</param>
		/// <param name="permissionGroup">Permission group object to be created.</param>
		/// <returns>Permission group instance returned from API.</returns>
		public async Task<PermissionGroupDTO> Create(String idempotencyKey, PermissionGroupPostDTO permissionGroup)
		{
			return await this.CreateObject<PermissionGroupDTO, PermissionGroupPostDTO>(idempotencyKey, MethodKey.PermissionGroupCreate, permissionGroup);
		}

		/// <summary>Gets permission groups.</summary>
		/// <param name="pagination">Pagination.</param>
		/// <param name="sort">Sort.</param>
		/// <returns>Collection of permission group instances.</returns>
		public async Task<ListPaginated<PermissionGroupDTO>> GetAll(Pagination pagination, Sort sort = null)
		{
			return await this.GetList<PermissionGroupDTO>(MethodKey.PermissionGroupAll, pagination, sort);
		}

		/// <summary>Gets first page of permission groups.</summary>
		/// <returns>Collection of permission group instances.</returns>
		public async Task<ListPaginated<PermissionGroupDTO>> GetAll()
		{
			return await GetAll(null);
		}

		/// <summary>Gets SSOs for a permission group.</summary>
		/// <param name="permissionGroupId">Permission group identifier.</param>
		/// <param name="pagination">Pagination.</param>
		/// <param name="sort">Sort.</param>
		/// <returns>Collection of permission group instances.</returns>
		public async Task<ListPaginated<SingleSignOnDTO>> GetSingleSignOns(String permissionGroupId, Pagination pagination, Sort sort = null)
		{
			return await this.GetList<SingleSignOnDTO>(MethodKey.PermissionGroupAllSsos, pagination, permissionGroupId, sort);
		}

		/// <summary>Updates the permission group.</summary>
		/// <param name="permissionGroup">Instance of permission group class to be updated.</param>
		/// <param name="permissionGroupId">Permission group user identifier.</param>
		/// <returns>Updated permission group object returned from API.</returns>
		public async Task<PermissionGroupDTO> Update(PermissionGroupPutDTO permissionGroup, String permissionGroupId)
		{
			return await this.UpdateObject<PermissionGroupDTO, PermissionGroupPutDTO>(MethodKey.PermissionGroupSave, permissionGroup, permissionGroupId);
		}
	}
}
