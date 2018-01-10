using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.PUT;
using NUnit.Framework;
using System;
using MangoPay.SDK.Entities;
using MangoPay.SDK.Core;
using System.Threading.Tasks;

namespace MangoPay.SDK.Tests
{
    [TestFixture]
    public class ApiCardsTest : BaseTest
    {
		[Test]
		public async Task Test_Card_GetTransactionsForCard()
		{
			try
			{
				PayInCardDirectDTO payIn = await GetNewPayInCardDirect();

				var pagination = new Pagination(1, 1);
				var filter = new FilterTransactions();
				var sort = new Sort();
				sort.AddField("CreationDate", SortDirection.desc);

				var transactions = await Api.Cards.GetTransactionsForCard(payIn.CardId, pagination, filter, sort);

				Assert.IsTrue(transactions.Count > 0);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
