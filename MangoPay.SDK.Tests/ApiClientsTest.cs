﻿using MangoPay.SDK.Core;
using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.POST;
using MangoPay.SDK.Entities.PUT;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MangoPay.SDK.Tests
{
	[TestFixture]
	public class ApiClientsTest : BaseTest
	{

		[Test]
		public async Task Test_Client_GetKycDocuments()
		{
			ListPaginated<KycDocumentDTO> result = null;
			ListPaginated<KycDocumentDTO> result2 = null;

			try
			{
				result = await this.Api.Clients.GetKycDocuments(null, null);
				Assert.IsNotNull(result);
				Assert.IsTrue(result.Count > 0);

				Pagination pagination = new Pagination(1, 2);
				Sort sort = new Sort();
				sort.AddField("CreationDate", SortDirection.asc);
				result = await this.Api.Clients.GetKycDocuments(pagination, null, sort);
				Assert.IsNotNull(result);
				Assert.IsTrue(result.Count > 0);

				sort = new Sort();
				sort.AddField("CreationDate", SortDirection.desc);
				result2 = await this.Api.Clients.GetKycDocuments(pagination, null, sort);
				Assert.IsNotNull(result2);
				Assert.IsTrue(result2.Count > 0);

				Assert.IsTrue(result[0].Id != result2[0].Id);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public async Task Test_Client_GetWallets()
		{
			ListPaginated<WalletDTO> feesWallets = null;
			ListPaginated<WalletDTO> creditWallets = null;
			try
			{
				feesWallets = await this.Api.Clients.GetWallets(FundsType.FEES, new Pagination(1, 100));
				creditWallets = await this.Api.Clients.GetWallets(FundsType.CREDIT, new Pagination(1, 100));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
			Assert.IsNotNull(feesWallets);
			Assert.IsNotNull(creditWallets);
		}

		[Test]
		public async Task Test_Client_GetWallet()
		{
			ListPaginated<WalletDTO> feesWallets = null;
			ListPaginated<WalletDTO> creditWallets = null;
			try
			{
				feesWallets = await this.Api.Clients.GetWallets(FundsType.FEES, new Pagination(1, 1));
				creditWallets = await this.Api.Clients.GetWallets(FundsType.CREDIT, new Pagination(1, 1));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			if ((feesWallets == null || feesWallets.Count == 0) ||
				(creditWallets == null || creditWallets.Count == 0))
				Assert.Fail("Cannot test getting client's wallet because there is no any wallet for client.");

			WalletDTO wallet = null;
			WalletDTO result = null;
			if (feesWallets != null && feesWallets.Count > 0)
				wallet = feesWallets[0];
			else if (creditWallets != null && creditWallets.Count > 0)
				wallet = creditWallets[0];

			result = await this.Api.Clients.GetWallet(wallet.FundsType, wallet.Currency);

			Assert.IsNotNull(result);
			Assert.IsTrue(result.FundsType == wallet.FundsType);
			Assert.IsTrue(result.Currency == wallet.Currency);
		}

		[Test]
		public async Task Test_Client_GetWalletTransactions()
		{
			ListPaginated<WalletDTO> feesWallets = null;
			ListPaginated<WalletDTO> creditWallets = null;
			try
			{
				feesWallets = await this.Api.Clients.GetWallets(FundsType.FEES, new Pagination(1, 1));
				creditWallets = await this.Api.Clients.GetWallets(FundsType.CREDIT, new Pagination(1, 1));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			if ((feesWallets == null || feesWallets.Count == 0) ||
				(creditWallets == null || creditWallets.Count == 0))
				Assert.Fail("Cannot test getting client's wallet transactions because there is no any wallet for client.");

			WalletDTO wallet = null;
			ListPaginated<TransactionDTO> result = null;
			if (feesWallets != null && feesWallets.Count > 0)
				wallet = feesWallets[0];
			else if (creditWallets != null && creditWallets.Count > 0)
				wallet = creditWallets[0];

			result = await this.Api.Clients.GetWalletTransactions(wallet.FundsType, wallet.Currency, new Pagination(1, 1), null);

			Assert.IsNotNull(result);
			Assert.IsTrue(result.Count > 0);
		}

		[Test]
		public async Task Test_Client_GetTransactions()
		{
			ListPaginated<TransactionDTO> result = null;

			try
			{
				result = await this.Api.Clients.GetTransactions(null, null);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.IsNotNull(result);
		}

		[Test]
		public async Task Test_Client_CreateBankWireDirect()
		{
			try
			{
				ClientBankWireDirectPostDTO bankwireDirectPost = new ClientBankWireDirectPostDTO("CREDIT_EUR", new Money { Amount = 1000, Currency = CurrencyIso.EUR });

				PayInDTO result = await this.Api.Clients.CreateBankWireDirect(bankwireDirectPost);

				Assert.IsTrue(result.Id.Length > 0);
				Assert.AreEqual("CREDIT_EUR", result.CreditedWalletId);
				Assert.AreEqual(PayInPaymentType.BANK_WIRE, result.PaymentType);
				Assert.AreEqual(PayInExecutionType.DIRECT, result.ExecutionType);
				Assert.AreEqual(TransactionStatus.CREATED, result.Status);
				Assert.AreEqual(TransactionType.PAYIN, result.Type);
				Assert.IsNotNull(((PayInBankWireDirectDTO)result).WireReference);
				Assert.AreEqual(((PayInBankWireDirectDTO)result).BankAccount.Type, BankAccountType.IBAN);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public async Task Test_ClientGet()
		{
			ClientDTO client = await this.Api.Clients.Get();

			Assert.IsNotNull(client);
			Assert.IsTrue("sdk-unit-tests".Equals(client.ClientId));
		}

		[Test]
		public async Task Test_ClientSave()
		{
			ClientPutDTO client = new ClientPutDTO();

			Random rand = new Random();
			String color1 = (rand.Next(100000) + 100000).ToString();
			String color2 = (rand.Next(100000) + 100000).ToString();

			client.PrimaryButtonColour = "#" + color1;
			client.PrimaryThemeColour = "#" + color2;
			client.AdminEmails = new List<string> { "support@mangopay.com", "technical@mangopay.com" };
			client.BillingEmails = new List<string> { "support@mangopay.com", "technical@mangopay.com" };
			client.FraudEmails = new List<string> { "support@mangopay.com", "technical@mangopay.com" };
			client.TechEmails = new List<string> { "support@mangopay.com", "technical@mangopay.com" };
			client.TaxNumber = "123456";
			client.PlatformDescription = "Description";
			client.PlatformType = PlatformType.MARKETPLACE;
			client.PlatformURL = "http://test.com";
			client.HeadquartersAddress = new Address
			{
				AddressLine1 = "AddressLine1",
				AddressLine2 = "AddressLine2",
				City = "City",
				Country = CountryIso.FR,
				PostalCode = "51234",
				Region = "Region"
			};

			ClientDTO clientNew = await this.Api.Clients.Save(client);

			Assert.IsNotNull(clientNew);
			Assert.AreEqual(client.PrimaryButtonColour, clientNew.PrimaryButtonColour);
			Assert.AreEqual(client.PrimaryThemeColour, clientNew.PrimaryThemeColour);
			Assert.AreEqual(client.AdminEmails.Count, 2);
			Assert.AreEqual(client.AdminEmails[0], "support@mangopay.com");
			Assert.AreEqual(client.AdminEmails[1], "technical@mangopay.com");
			Assert.AreEqual(client.BillingEmails.Count, 2);
			Assert.AreEqual(client.BillingEmails[0], "support@mangopay.com");
			Assert.AreEqual(client.BillingEmails[1], "technical@mangopay.com");
			Assert.AreEqual(client.FraudEmails.Count, 2);
			Assert.AreEqual(client.FraudEmails[0], "support@mangopay.com");
			Assert.AreEqual(client.FraudEmails[1], "technical@mangopay.com");
			Assert.AreEqual(client.TechEmails.Count, 2);
			Assert.AreEqual(client.TechEmails[0], "support@mangopay.com");
			Assert.AreEqual(client.TechEmails[1], "technical@mangopay.com");
			Assert.AreEqual(client.TaxNumber, "123456");
			Assert.AreEqual(client.PlatformDescription, "Description");
			Assert.AreEqual(client.PlatformType, PlatformType.MARKETPLACE);
			Assert.AreEqual(client.PlatformURL, "http://test.com");
			Assert.IsNotNull(client.HeadquartersAddress);
			Assert.AreEqual(client.HeadquartersAddress.AddressLine1, "AddressLine1");
			Assert.AreEqual(client.HeadquartersAddress.AddressLine2, "AddressLine2");
			Assert.AreEqual(client.HeadquartersAddress.City, "City");
			Assert.AreEqual(client.HeadquartersAddress.Country, CountryIso.FR);
			Assert.AreEqual(client.HeadquartersAddress.PostalCode, "51234");
			Assert.AreEqual(client.HeadquartersAddress.Region, "Region");
		}

		[Test]
		public async Task Test_Client_SaveAddressNull()
		{
			ClientPutDTO client = new ClientPutDTO();

			Random rand = new Random();
			String color1 = (rand.Next(100000) + 100000).ToString();
			String color2 = (rand.Next(100000) + 100000).ToString();

			client.PrimaryButtonColour = "#" + color1;
			client.PrimaryThemeColour = "#" + color2;
			client.HeadquartersAddress = new Address();

			ClientDTO clientNew = await this.Api.Clients.Save(client);

			Assert.IsNotNull(clientNew);			
		}

		[Test]
		public async Task Test_ClientLogo()
		{
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileInfo assemblyFileInfo = new FileInfo(assembly.Location);
            FileInfo fi = assemblyFileInfo.Directory.GetFiles("TestKycPageFile.png").Single();

			await this.Api.Clients.UploadLogo(fi.FullName);
			await this.Api.Clients.UploadLogo(File.ReadAllBytes(fi.FullName));
		}
	}
}