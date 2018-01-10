using System;
using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.POST;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using MangoPay.SDK.Entities.PUT;
using System.Threading.Tasks;

namespace MangoPay.SDK.Tests
{

	[TestFixture]
	public class ApiUboDeclarationTests : BaseTest
	{
		private MangoPayApi _mangopayApi;

		private List<UserNaturalDTO> _userNaturalCollection;

		public List<UserNaturalDTO> UserNaturallCollection
		{
			get
			{
				if (_userNaturalCollection == null)
				{
					_userNaturalCollection = new List<UserNaturalDTO>();
				}

				return _userNaturalCollection;
			}
			set
			{
				_userNaturalCollection = value;
			}
		}		

		private UboRefusedReasonType[] RefusedReasons
		{
			get
			{
				return new UboRefusedReasonType[] {
				  UboRefusedReasonType.INVALID_DECLARED_UBO,
				  UboRefusedReasonType.INVALID_UBO_DETAILS
				};
			}
		}

		[SetUp]
		public void SetUp()
		{
			_mangopayApi = BuildNewMangoPayApi();
		}

		[Test]
		public async Task ApiUboDeclaration_Create_UboDeclaration_Valid()
		{
            await CreateUserNaturalPost();

			var userLegal = await Api.Users.Create(CreateUserLegalPost());

			var uboDeclarationPost = CreateUboDeclarationPost(userLegal, RefusedReasons);

			UboDeclarationDTO result = null;

			Assert.DoesNotThrowAsync(async () => result = await Api.UboDeclarations.Create(null, uboDeclarationPost));
			Assert.That(result.Status == UboDeclarationType.CREATED);
			Assert.That(result.CreationDate != DateTime.MinValue);
		}

		[Test]
		public async Task ApiUboDeclaration_Update_UboDeclaration_Valid()
		{
            await CreateUserNaturalPost();

			var userLegal = await Api.Users.Create(CreateUserLegalPost());

			var uboDeclarationPost = CreateUboDeclarationPost(userLegal, RefusedReasons);

			var uboDeclaration = await Api.UboDeclarations.Create(null, uboDeclarationPost);

			UboDeclarationPutDTO ubodeclarationPut = new UboDeclarationPutDTO()
			{
				ID = uboDeclaration.Id,
				Status = UboDeclarationType.VALIDATION_ASKED,
				RefusedReasonMessage = "New Refused Message",
				RefusedReasonTypes = new UboRefusedReasonType[] { UboRefusedReasonType.MISSING_UBO }
			};

			UboDeclarationDTO result = null;

			Assert.DoesNotThrowAsync(async () => result = await Api.UboDeclarations.Update(ubodeclarationPut, ubodeclarationPut.ID));
			Assert.That(result != null);
			Assert.That(result.Status == UboDeclarationType.VALIDATION_ASKED);
			Assert.That(result.CreationDate != DateTime.MinValue);
		}

		private UboDeclarationPostDTO CreateUboDeclarationPost(UserLegalDTO userLegal, UboRefusedReasonType[] refusedResons)
		{
			return new UboDeclarationPostDTO()
			{
				UserId = userLegal.Id,
				Status = UboDeclarationType.CREATED,
				DeclaredUBOs = UserNaturallCollection.Select(x => x.Id).ToArray(),
				RefusedReasonTypes = refusedResons,
				RefusedReasonMessage = "Refused Reason Message"
			};
		}

		private async Task CreateUserNaturalPost()
		{
			List<UserNaturalPostDTO> userNaturalCollection = UserNaturalPostCollection;

			foreach (var user in userNaturalCollection)
			{
                user.Capacity = CapacityType.DECLARATIVE;
                var userNatural = await Api.Users.Create(user);                
                UserNaturallCollection.Add(userNatural);
			}
		}		

		[TearDown]
		public void TearDown()
		{
			UserNaturallCollection = null;
		}
	}
}
