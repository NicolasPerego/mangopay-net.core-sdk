using MangoPay.SDK.Core;
using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities;
using MangoPay.SDK.Entities.GET;
using MangoPay.SDK.Entities.PUT;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MangoPay.SDK.Tests
{
    [TestFixture]
    public class ApiWalletsTest : BaseTest
    {
        [Test]
        public async Task Test_Wallets_Create()
        {
            UserNaturalDTO john = await this.GetJohn();
            WalletDTO wallet = await this.GetJohnsWallet();

            Assert.IsTrue(wallet.Id.Length > 0);
            Assert.IsTrue(wallet.Owners.Contains(john.Id));
        }

        [Test]
        public async Task Test_Wallets_Get()
        {
            UserNaturalDTO john = await this.GetJohn();
            WalletDTO wallet = await this.GetJohnsWallet();

            WalletDTO getWallet = await this.Api.Wallets.Get(wallet.Id);

            Assert.AreEqual(wallet.Id, getWallet.Id);
            Assert.IsTrue(wallet.Owners.Contains(john.Id));
        }

        [Test]
        public async Task Test_Wallets_Save()
        {
            WalletDTO wallet = await this.GetJohnsWallet();
            WalletPutDTO walletPut = new WalletPutDTO();
            walletPut.Description = wallet.Description + " - changed";
            walletPut.Owners = wallet.Owners;
            walletPut.Tag = wallet.Tag;

            WalletDTO saveWallet = await this.Api.Wallets.Update(walletPut, wallet.Id);

            Assert.AreEqual(wallet.Id, saveWallet.Id);
            Assert.AreEqual(wallet.Description + " - changed", saveWallet.Description);
        }

        [Test]
        public async Task Test_Wallets_Transactions()
        {
            UserNaturalDTO john = await GetJohn();
            
            WalletDTO wallet = await CreateJohnsWallet();
            PayInDTO payIn = await CreateJohnsPayInCardWeb(wallet.Id);

            Pagination pagination = new Pagination(1, 1);
            FilterTransactions filter = new FilterTransactions();
            filter.Type = TransactionType.PAYIN;
            ListPaginated<TransactionDTO> transactions = await Api.Wallets.GetTransactions(wallet.Id, pagination, filter, null);

            Assert.IsTrue(transactions.Count == 1);
            Assert.IsTrue(transactions[0] is TransactionDTO);
            Assert.AreEqual(transactions[0].AuthorId, john.Id);
            AssertEqualInputProps(transactions[0], payIn);
        }

        [Test]
        public async Task Test_Wallets_Transactions_With_Sorting()
        {
            WalletDTO wallet = await this.GetJohnsWallet();

            // create 2 payins
            await this.GetJohnsPayInCardWeb();
            await this.GetNewPayInCardWeb();
            Sort sort = new Sort();
            sort.AddField("CreationDate", SortDirection.desc);
            Pagination pagination = new Pagination(1, 20);
            FilterTransactions filter = new FilterTransactions();
            filter.Type = TransactionType.PAYIN;

            ListPaginated<TransactionDTO> transactions = await this.Api.Wallets.GetTransactions(wallet.Id, pagination, filter, sort);

            Assert.IsTrue(transactions[0].CreationDate > transactions[1].CreationDate);
        }
    }
}
