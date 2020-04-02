using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;

namespace TestConsole
{
    public class Program
    {
        //For testing use the following account info, this only exists on test network and may be wiped at any time...
        //Public: GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI
        //Secret: SCD74D46TJYXOUXFC5YOA72UTPCCVHK2GRSLKSPRB66VK6UJHQX2Y3R3

        public async void Main()
        {
            //await SendNativeAssets();
            //Network.UseTestNetwork();
            //using (var server = new Server("https://horizon-testnet.stellar.org"))
            //{
            //    var acc = KeyPair.Random().AccountId;
            //    var friendBot = await server.TestNetFriendBot
            //        .FundAccount(acc)
            //        .Execute().ConfigureAwait(true);

            //    //await GetLedgerTransactions(server);
            //    await ShowAccountTransactions(server);
            //    ShowTestKeyValue(server);
            //}
            //Network network = new Network("Test SDF Network ; September 2015");
            //Server server = new Server("https://horizon-testnet.stellar.org");
            //KeyPair keypair = KeyPair.Random();

            //Network.UseTestNetwork();

            //var sourceaccID = keypair.AccountId;
            //using (var server1 = new Server("https://horizon-testnet.stellar.org"))
            //{
            //    var friendBot = await server.TestNetFriendBot
            //    .FundAccount(sourceaccID)
            //    .Execute().ConfigureAwait(true);
            //}
            //var accTest = server.Accounts.Account("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");

            using (var client = new HttpClient())
            {
                var modelstellar = new Test_API.Models.ObjectAPI();
                var responses = client.GetAsync("https://horizon-testnet.stellar.org/accounts/GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT/operations").Result;
                if (responses.IsSuccessStatusCode)
                {
                    var result = responses.Content.ReadAsStringAsync().Result;
                    var jObjectRoot = JObject.Parse(result).ToString(); 
                    modelstellar = JsonConvert.DeserializeObject<Test_API.Models.ObjectAPI>(jObjectRoot);
                }

            }

            using (var servertest = new Server("https://horizon-testnet.stellar.org"))
            {
                Console.WriteLine("-- Streaming All Transaction for this Ledger --");
                //var abc =  servertest.Transactions
                //.ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT")
                //.Stream((sender, response) => { ShowPaymentResponse(servertest, sender, response); }).Connect();       
                await servertest.Operations                
            .Stream((sender, response) => { ShowPaymentResponse(servertest, sender, response); }).Connect();
            }
          
        }

     
        public static async Task ShowPaymentResponse(Server server, object sender, OperationResponse lr)
        {
            var test = lr.CreatedAt;
            var op = server.Operations.ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");
            var operations = await op.Execute().ConfigureAwait(true);
            foreach (var dr in operations.Records)
            {
                if (dr.Type == "payment")
                {
                    var abc = dr.CreatedAt;
                }
            }
        }

//            using (var server1 = new Server("https://horizon.stellar.org"))
//            {
//                Console.WriteLine("-- Streaming All New Ledgers On The Network --");
//                await server1.Ledgers
//                    .Cursor("now")
//                    .Stream((sender, response) => { ShowOperationResponse(server1, sender, response); })
//                    .Connect();
//}

        private static async Task ShowAccountTransactions(Server server)
            {
                Console.WriteLine("-- Show Account Transactions (ForAccount) --");

                var transactions = await server.Transactions
                    .ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT")
                    .Execute();
                var accTest = await server.Accounts.Account("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");

                ShowTransactionRecords(transactions.Records);
                Console.WriteLine();
            }

        private static async Task GetLedgerTransactions(Server server)
        {
            Console.WriteLine("-- Show Ledger Transactions (ForLedger) --");
            // get a list of transactions that occurred in ledger 1400
            var transactions = await server.Transactions
                .ForLedger(2365)
                .Execute();

            ShowTransactionRecords(transactions.Records);
            Console.WriteLine();
        }

        private static void ShowTransactionRecords(List<TransactionResponse> transactions)
        {
            foreach (var tran in transactions)
                ShowTransactionRecord(tran);
        }
        
        
        private static void ShowTransactionRecord(TransactionResponse tran)
        {
            
            Console.WriteLine($"Ledger: {tran.Ledger}, Hash: {tran.Hash}, Fee Paid: {tran.FeeCharged}, Pt:{tran.PagingToken}");
        }

        private static async void ShowOperationResponse(Server server, object sender, LedgerResponse lr)
        {
            var operationRequestBuilder = server.Operations.ForLedger(lr.Sequence);
            var operations = await operationRequestBuilder.Execute();

            var accts = 0;
            var payments = 0;
            var offers = 0;
            var options = 0;

            foreach (var op in operations.Records)
                switch (op.Type)
                {
                    case "create_account":
                        accts++;
                        break;
                    case "payment":
                        payments++;
                        break;
                    case "manage_offer":
                        offers++;
                        break;
                    case "set_options":
                        options++;
                        break;
                }

            Console.WriteLine($"id: {lr.Sequence}, tx/ops: {lr.SuccessfulTransactionCount + "/" + lr.OperationCount}, accts: {accts}, payments: {payments}, offers: {offers}, options: {options}");
            Console.WriteLine($"Uri: {((LedgersRequestBuilder)sender).Uri}");
        }
       
        //public delegate void EventHandler<OperationResponse>(object sender, OperationResponse e);
        private static void ShowTestKeyValue(Server server)
        {
            Console.WriteLine("-- Getting TestKey for Account --");

            var data = server.Accounts.AccountData("GDPFSTS6RMEJ4ATT4K72C2DWS7PIHPUIYC2RJWRTHWOJT2SXEG7P35BZ", "TestKey");

            var dataResult = data.Result;

            Console.WriteLine("Encoded Value: " + dataResult.Value);
            Console.WriteLine("Decoded Value: " + dataResult.ValueDecoded);

            Console.WriteLine();
        }
        public async Task SendNativeAssets()
        {
       

            //Set network and server
            Network network = new Network("Test SDF Network ; September 2015");
            Server server = new Server("https://horizon-testnet.stellar.org");
            KeyPair keypair = KeyPair.Random();

            Network.UseTestNetwork();

            var sourceaccID = keypair.AccountId;
            using (var server1 = new Server("https://horizon-testnet.stellar.org"))
            {
                var friendBot = await server.TestNetFriendBot
                .FundAccount(sourceaccID)
                .Execute().ConfigureAwait(true);
            }

            //Source keypair from the secret seed
            KeyPair sourceKeypair = KeyPair.FromSecretSeed(keypair.SecretSeed);
            var SourceaccTest = await server.Accounts.Account(sourceaccID);

            //Load source account data
            AccountResponse sourceAccountResponse = await server.Accounts.Account(sourceKeypair.AccountId);

            //Create source account object
            Account sourceAccount = new Account(sourceKeypair.AccountId, sourceAccountResponse.SequenceNumber);

            //Create asset object with specific amount
            //You can use native or non native ones.
            Asset asset = new AssetTypeNative();
            string amount = "30";

            //Load des account data
            AccountResponse descAccountResponse = await server.Accounts.Account("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");

            //Destination keypair from the account id
            KeyPair destinationKeyPair = KeyPair.FromAccountId("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");
            var transactions = await server.Transactions
              .ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT")
              .Execute().ConfigureAwait(true);
            var abc = new TransactionResponse();
            var test = new test.MyTestApp();
            test.Main();
            var tranDetail = server.Transactions.ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT").Cursor("now");
            var paymentDetail = server.Payments.ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");
            //paymentDetail.Stream(operationres);
            //OnhresholdReached?.Invoke(this, new OperationResponse());
            Program1 abcd = new Program1();
            abcd.OnhresholdReached += Abcd_OnhresholdReached;
            abcd.OnhresholdReached += new EventHandler<OperationResponse>(this.Abcd_OnhresholdReached);
            
            var OperateDetail = server.Operations.ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT");

            //Create payment operation
            PaymentOperation operation = new PaymentOperation.Builder(destinationKeyPair, asset, amount).SetSourceAccount(sourceAccount.KeyPair).Build();


            //Create transaction and add the payment operation we created
            Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();

            //Sign Transaction
            transaction.Sign(sourceKeypair);

            //Try to send the transaction
            try
            {
                Console.WriteLine("Sending Transaction");
                await server.SubmitTransaction(transaction).ConfigureAwait(true);
                Console.WriteLine("Success!");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Send Transaction Failed");
                Console.WriteLine("Exception: " + exception.Message);
            }
        }

        public void Abcd_OnhresholdReached(object sender, OperationResponse e)
        {
            var a = 0;
            a = a + 1;
            throw new NotImplementedException();
        }

        public delegate void resholdReached(OperationResponse ac);
      


    }


    namespace test
    {
        public class MyTestApp
        {
            //The Event Handler declaration
            public delegate void EventHandler();

            //The Event declaration
            public event EventHandler MyHandler;


            public event EventHandler<OperationResponse> OnhresholdReached;
            //The method to call
            public void Hello()
            {
                Console.WriteLine("Hello World of events!");
            }
            public void Hello1(object sender, OperationResponse e)
            {
                Console.WriteLine("Hello World of events!");
            }

            public void Main()
            {
                MyTestApp TestApp = new MyTestApp();            
                TestApp.OnhresholdReached = new EventHandler<OperationResponse>(TestApp.Hello1);
                Server server = new Server("https://horizon-testnet.stellar.org");
                var OperateDetail = server.Operations.ForAccount("GDLRM2QFV7GOOAZDELHFCC5RBRMX4NAZNBSTAKG6NGO4CFODY5PSCYWT").Stream(TestApp.OnhresholdReached);
               
            }

        }

    }
    public class Program1
    {
        public event EventHandler<TransactionResponse> ThresholdReached;
        public event EventHandler<OperationResponse> OnhresholdReached;
        static void Main()
        {
            Adder a = new Adder();
            a.OnMultipleOfFiveReached += a_MultipleOfFiveReached;
            int iAnswer = a.Add(4, 3);
            Console.WriteLine("iAnswer = {0}", iAnswer);
            iAnswer = a.Add(4, 6);
            Console.WriteLine("iAnswer = {0}", iAnswer);
            Console.ReadKey();
        }
        public void Abcd_OnhresholdReached(object sender, OperationResponse e)
        {

            throw new NotImplementedException();
        }

        static void a_MultipleOfFiveReached(object sender, MultipleOfFiveEventArgs e)
        {
            Console.WriteLine("Multiple of five reached: ", e.Total);
        }
    }

    public class Adder
    {
        public event EventHandler<MultipleOfFiveEventArgs> OnMultipleOfFiveReached;
        public int Add(int x, int y)
        {
            int iSum = x + y;
            if ((iSum % 5 == 0) && (OnMultipleOfFiveReached != null))
            { OnMultipleOfFiveReached(this, new MultipleOfFiveEventArgs(iSum)); }
            return iSum;
        }
    }

    public class MultipleOfFiveEventArgs : EventArgs
    {
        public MultipleOfFiveEventArgs(int iTotal)
        { Total = iTotal; }
        public int Total { get; set; }
    }
}