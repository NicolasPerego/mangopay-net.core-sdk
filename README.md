This is a .NET Core 2.0 compliant MangoPay API SDK (www.mangopay.com).

It's fully async.
All tests are OK.
I'll try to update it when the official .NET 4.5 release is updated.

Usage with .NET DI :

Startup.cs =>

		public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            [...]

            //MangoPay API
            services.AddSingleton<IMangoPayApi, MangoPayApi>(serviceProvider =>
            {
                var api = new MangoPayApi();
                api.Config.ClientId = "yourId";
                api.Config.ClientPassword = "yourPass";
                api.Config.BaseUrl = "https://api.sandbox.mangopay.com"; // or the prod url
                api.Config.ApiVersion = "v2.01";
                api.Config.Timeout = 0;

                return api;
            });
			
			[...]
		}
			
Then you can inject it in a controller like this :

		public class PaymentController : MojaProjectControllerBase
		{
			private readonly IMangoPayApi _mangoPayApi;

			public PaymentController(IMangoPayApi mangoPayApi)
			{
				_mangoPayApi = mangoPayApi;
			}

			[HttpPost]
			public async Task CreateMangoPayNaturalUser()
			{
				UserNaturalPostDTO user = new UserNaturalPostDTO("test@sample.org", "Jane", "Doe", new DateTime(1975, 12, 21, 0, 0, 0), CountryIso.FR, CountryIso.FR);

				var createdUser = await _mangoPayApi.Users.Create(Guid.NewGuid().ToString(), user);
			}
		}
