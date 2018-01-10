﻿using MangoPay.SDK.Core.APIs;
using MangoPay.SDK.Core.Interfaces;
using MangoPay.SDK.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MangoPay.SDK.Core
{
    /// <summary>Authorization token manager. This class cannot be inherited.</summary>
    public sealed class AuthorizationTokenManager : ApiBase
    {
        private IStorageStrategy _storageStrategy;

        /// <summary>Instantiates new AuthorizationTokenManager object.</summary>
        /// <param name="root">Root/parent instance that holds the OAuthToken and Configuration instances.</param>
        public AuthorizationTokenManager(MangoPayApi root)
            : base(root)
        {
            this.RegisterCustomStorageStrategy(new DefaultStorageStrategy());
        }

        /// <summary>Gets the current authorization token. 
        /// In the very first call, this method creates a new token before returning. 
        /// If currently stored token is expired, this method creates a new one.
        /// </summary>
        /// <returns>Valid OAuthToken instance.</returns>
        public async Task<OAuthTokenDTO> GetToken()
        {
			OAuthTokenDTO token = _storageStrategy.Get(GetEnvKey());

            if (token == null || token.IsExpired())
            {
                StoreToken(await this._root.AuthenticationManager.CreateToken());
            }

			return _storageStrategy.Get(GetEnvKey());
        }

        /// <summary>Stores authorization token passed as an argument in the underlying storage strategy implementation.</summary>
        /// <param name="token">Token instance to be stored.</param>
        public void StoreToken(OAuthTokenDTO token)
        {
			_storageStrategy.Store(token, GetEnvKey());
        }

        /// <summary>Registers custom storage strategy implementation.
        /// By default, the <code>DefaultStorageStrategy</code> instance is used. 
        /// There is no need to explicitly call this method until some more complex storage implementation is needed.
        /// </summary>
        /// <param name="customStorageStrategy">IStorageStrategy interface implementation.</param>
        public void RegisterCustomStorageStrategy(IStorageStrategy customStorageStrategy)
        {
            _storageStrategy = customStorageStrategy;
        }

		private string GetEnvKey()
		{
			string input = _root.Config.BaseUrl + _root.Config.ClientId + _root.Config.ClientPassword;

			using (MD5 md5Hash = MD5.Create())
			{
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder sBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
		}
    }
}
