﻿using MangoPay.SDK.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MangoPay.SDK.Tests
{
    [TestFixture]
    public class TokensTest : BaseTest
    {
        [Test]
        public async Task Test_ForceToken()
        {
            OAuthTokenDTO oldToken = await this.Api.OAuthTokenManager.GetToken();
            OAuthTokenDTO newToken = await this.Api.AuthenticationManager.CreateToken();

            Assert.IsFalse(oldToken.access_token == newToken.access_token);

            this.Api.OAuthTokenManager.StoreToken(newToken);
            OAuthTokenDTO storedToken = await this.Api.OAuthTokenManager.GetToken();

            Assert.AreEqual(newToken.access_token, storedToken.access_token);
        }

        [Test]
        public async Task Test_StandardUseToken()
        {
            await this.Api.Users.GetAll();
            OAuthTokenDTO token = await this.Api.OAuthTokenManager.GetToken();
            await this.Api.Users.GetAll();

            var temp = await this.Api.OAuthTokenManager.GetToken();
            Assert.AreEqual(token.access_token, temp.access_token);
        }

        [Test]
        public async Task Test_ShareTokenBetweenInstances()
        {
            MangoPayApi api = this.BuildNewMangoPayApi();

            OAuthTokenDTO token1 = await this.Api.OAuthTokenManager.GetToken();
            OAuthTokenDTO token2 = await api.OAuthTokenManager.GetToken();

            Assert.AreEqual(token1.access_token, token2.access_token);
        }

		[Test]
		public async Task Test_IsolateTokensBetweenEnvironments()
		{
			MangoPayApi api = new MangoPayApi();
			api.Config.ClientId = "sdk-unit-tests";
			api.Config.ClientPassword = "cqFfFrWfCcb7UadHNxx2C9Lo6Djw8ZduLi7J9USTmu8bhxxpju";
			api.Config.BaseUrl = "https://api.sandbox.mangopay.com";

			OAuthTokenDTO token1 = await api.OAuthTokenManager.GetToken();

			api.Config.ClientId = "sdk_example";
			api.Config.ClientPassword = "Vfp9eMKSzGkxivCwt15wE082pTTKsx90vBenc9hjLsf5K46ciF";
			api.Config.BaseUrl = "https://api.sandbox.mangopay.com";

			OAuthTokenDTO token2 = await api.OAuthTokenManager.GetToken();

			Assert.AreNotEqual(token1.access_token, token2.access_token);

			api.Config.ClientId = "sdk-unit-tests";
			api.Config.ClientPassword = "cqFfFrWfCcb7UadHNxx2C9Lo6Djw8ZduLi7J9USTmu8bhxxpju";
			api.Config.BaseUrl = "https://api.sandbox.mangopay.com";

			OAuthTokenDTO token3 = await api.OAuthTokenManager.GetToken();

			Assert.AreEqual(token1.access_token, token3.access_token);
		}
    }
}
