using MangoPay.SDK.Core;
using MangoPay.SDK.Core.APIs;
using System;

namespace MangoPay.SDK
{
    public interface IMangoPayApi
    {
        AuthorizationTokenManager OAuthTokenManager { get; set; }

        Configuration Config { get; set; }
        
        LastRequestInfo LastRequestInfo { get; set; }
        
        ApiOAuth AuthenticationManager { get; set; }
        
        ApiClients Clients { get; set; }
        
        ApiUsers Users { get; set; }
        
        ApiWallets Wallets { get; set; }

        ApiPayIns PayIns { get; set; }
        
        ApiPayOuts PayOuts { get; set; }
        
        ApiTransfers Transfers { get; set; }
        
        ApiCardRegistrations CardRegistrations { get; set; }
        
        ApiCardPreAuthorizations CardPreAuthorizations { get; set; }
        
        ApiCards Cards { get; set; }
        
        ApiRefunds Refunds { get; set; }
        
        ApiEvents Events { get; set; }
        
        ApiHooks Hooks { get; set; }
        
        ApiKyc Kyc { get; set; }
        
        ApiDisputes Disputes { get; set; }
        
        ApiIdempotency Idempotency { get; set; }
        
        ApiMandates Mandates { get; set; }
        
        ApiReports Reports { get; set; }
        
        ApiBankingAliases BankingAlias { get; set; }

        ApiSingleSignOns SingleSignOns { get; set; }

        ApiPermissionGroups PermissionGroups { get; set; }

        ApiUboDeclarations UboDeclarations { get; set; }
    }

    /// <summary>
    /// MangoPay API main entry point. 
    /// Provides managers to connect, send and read data from MangoPay API as well as it holds configuration/authorization data.
    /// </summary>
    public class MangoPayApi : IMangoPayApi
    {
        /// <summary>Instantiates new MangoPayApi object.</summary>
        public MangoPayApi()
        {
            // default config setup
            Config = new Configuration();
            OAuthTokenManager = new AuthorizationTokenManager(this);

            // API managers initialization
            AuthenticationManager = new ApiOAuth(this);
            Clients = new ApiClients(this);
            Users = new ApiUsers(this);
            Wallets = new ApiWallets(this);
            PayIns = new ApiPayIns(this);
            PayOuts = new ApiPayOuts(this);
            Refunds = new ApiRefunds(this);
            Transfers = new ApiTransfers(this);
            CardRegistrations = new ApiCardRegistrations(this);
            Cards = new ApiCards(this);
            Events = new ApiEvents(this);
            CardPreAuthorizations = new ApiCardPreAuthorizations(this);
            Hooks = new ApiHooks(this);
            Kyc = new ApiKyc(this);
			Disputes = new ApiDisputes(this);
			Idempotency = new ApiIdempotency(this);
			Mandates = new ApiMandates(this);
			Reports = new ApiReports(this);
			SingleSignOns = new ApiSingleSignOns(this);
			PermissionGroups = new ApiPermissionGroups(this);
			BankingAlias = new ApiBankingAliases(this);
			UboDeclarations = new ApiUboDeclarations(this);
		}

        /// <summary>Provides authorization token methods.</summary>
        public AuthorizationTokenManager OAuthTokenManager { get; set; }

        /// <summary>Configuration instance with default settings (to be reset if required).</summary>
        public Configuration Config { get; set; }

        /// <summary>Stores the raw request and response of the last call from this Api instance, including information about rate-limiting.</summary>
        public LastRequestInfo LastRequestInfo { get; set; }

        #region API managers

        /// <summary>Provides OAuth methods.</summary>
        public ApiOAuth AuthenticationManager { get; set; }

        /// <summary>Provides Clients methods.</summary>
        public ApiClients Clients { get; set; }

        /// <summary>Provides Users methods.</summary>
        public ApiUsers Users { get; set; }

        /// <summary>Provides Wallets methods.</summary>
        public ApiWallets Wallets { get; set; }

        /// <summary>Provides PayIns methods.</summary>
        public ApiPayIns PayIns { get; set; }

        /// <summary>Provides PayOuts methods.</summary>
        public ApiPayOuts PayOuts { get; set; }

        /// <summary>Provides Transfer methods.</summary>
        public ApiTransfers Transfers { get; set; }

        /// <summary>Provides CardRegistrations methods.</summary>
        public ApiCardRegistrations CardRegistrations { get; set; }

        /// <summary>Provides CardPreAuthorizations methods.</summary>
        public ApiCardPreAuthorizations CardPreAuthorizations { get; set; }

        /// <summary>Provides Cards methods.</summary>
        public ApiCards Cards { get; set; }

        /// <summary>Provides Refunds methods.</summary>
        public ApiRefunds Refunds { get; set; }

        /// <summary>Provides Events methods.</summary>
        public ApiEvents Events { get; set; }

        /// <summary>Provides Hooks methods.</summary>
        public ApiHooks Hooks { get; set; }

		/// <summary>Provides KYC methods.</summary>
		public ApiKyc Kyc { get; set; }

		/// <summary>Provides Disputes methods.</summary>
		public ApiDisputes Disputes { get; set; }

		/// <summary>Provides Idempotency methods.</summary>
		public ApiIdempotency Idempotency { get; set; }

		/// <summary>Provides Mandates methods.</summary>
		public ApiMandates Mandates { get; set; }

		/// <summary>Provides Reports methods.</summary>
		public ApiReports Reports { get; set; }

		/// <summary>Provides Users methods.</summary>
		public ApiBankingAliases BankingAlias { get; set; }

        /// <summary>Provides SingleSignOns methods.</summary>
        public ApiSingleSignOns SingleSignOns { get; set; }

		/// <summary>Provides ApiPermissionGroups methods.</summary>
		public ApiPermissionGroups PermissionGroups { get; set; }

		public ApiUboDeclarations UboDeclarations { get; set; }
		#endregion

		#region Internal and private

		private Version Version { get; set; }

		/// <summary>
		/// Gets the current SDK <see cref="Version"/>
		/// </summary>
		/// <returns>The current SDK <see cref="Version"/></returns>
		internal Version GetVersion()
		{
			// Get the cached version to avoid using reflection
			if (Version != null)
			{
				return Version;
			}

			Version = typeof(MangoPayApi).Assembly?.GetName()?.Version ?? new Version(0, 0, 0);

			return Version;
		}
		
		#endregion
	}
}
