using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            WalletList maWallet = new WalletList();

            maWallet.AddWallet(new Wallet("Russian Wallet", Currency.Ruble, 15));
            maWallet.AddWallet(new Wallet("Hrivna Wallet", Currency.Hryvnia, 15));
            maWallet.AddWallet(new Wallet("Dollar Wallet", Currency.Dollar, 15));
            maWallet.AddWallet(new Wallet("Euro Wallet", Currency.Euro, 15));

            maWallet.wallet[1].Deposit(1555);
            maWallet.wallet[1].TransferTo(10, maWallet.wallet[2]);
            maWallet.ShowHistory();
            maWallet.ShowPaymentByDate(DateTime.Now.Date);
            maWallet.ShowAmountInCurrency((Currency)1);
            Console.ReadKey();
        }
    }
}
