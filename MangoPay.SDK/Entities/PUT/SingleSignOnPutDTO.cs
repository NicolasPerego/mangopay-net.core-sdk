﻿using System;

namespace MangoPay.SDK.Entities.PUT
{
	/// <summary>User natural PUT entity.</summary>
	public class SingleSignOnPutDTO : EntityPutBase
    {
        /// <summary>Custom data.</summary>
        public String Tag { get; set; }

		/// <summary>The name of the user.</summary>
		public String FirstName { get; set; }

		/// <summary>The last name of the user.</summary>
		public String LastName { get; set; }

		/// <summary>Permission group ID assigned to this SSO.</summary>
		public String PermissionGroupId { get; set; }

		/// <summary>Wheter the SSO is active or not.</summary>
		public bool? Active { get; set; }
	}
}
