//using stellar_dotnet_sdk;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBC_Stellar
{
    public class TestStellar
    {
        public async void CreateAccountStellarAsync()
        {
            KeyPair keypairtest = KeyPair.Random();
            var newAccount = new CreateAccountOperation(keypairtest, "1000");
            var Acc = newAccount.ToOperationBody();
            

            Network network = new Network("Test SDF Network ; September 2015");
            Server server = new Server("https://horizon-testnet.stellar.org");

            //Generate a keypair from the account id.
            KeyPair keypair = KeyPair.FromAccountId(keypairtest.AccountId);

            //Load the account
            AccountResponse accountResponse = await server.Accounts.Account(keypairtest.AccountId);

            //Get the balance
            Balance[] balances = accountResponse.Balances;

            //Show the balance
            for (int i = 0; i < balances.Length; i++)
            {
                Balance asset = balances[i];
                Console.WriteLine("Asset Code: " + asset.AssetCode);
                Console.WriteLine("Asset Amount: " + asset.BalanceString);
            }
        }
    }
}
