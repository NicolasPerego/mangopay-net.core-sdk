using System;
using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.POST;
using MangoPay.SDK.Entities.PUT;
using System.Threading.Tasks;

namespace MangoPay.SDK.Core.APIs
{
	public class ApiUboDeclarations : ApiBase
	{
		public ApiUboDeclarations(MangoPayApi root)
		  : base(root)
		{
		}

		public async Task<UboDeclarationDTO> Create(UboDeclarationPostDTO uboDeclaration)
		{
			return await Create(null, uboDeclaration);
		}

		public async Task<UboDeclarationDTO> Create(String idempotencyKey, UboDeclarationPostDTO uboDeclaration)
		{
			return await CreateObject<UboDeclarationDTO, UboDeclarationPostDTO>(
			  idempotencyKey,
			  MethodKey.UboDeclarationCreate,
			  uboDeclaration,
			  uboDeclaration.UserId
			);
		}

		public async Task<UboDeclarationDTO> Update(UboDeclarationPutDTO uboDeclaration, String UboDeclarationId)
		{
			return await UpdateObject<UboDeclarationDTO, UboDeclarationPutDTO>(
			  MethodKey.UboDeclarationUpdate,
			  uboDeclaration,
			  UboDeclarationId
			);
		}		
	}
}