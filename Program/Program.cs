using System;
using WalletSystem;
using EnumLibrary;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            WalletList maWallet = new WalletList();

            maWallet.AddWallet(new Wallet("Russian Wallet", Currency.Ruble, 15, InputPurpose.Salary));
            maWallet.wallet[0].MoneyOperation += new Wallet.OperationHandler(Display);
            maWallet.AddWallet(new Wallet("Hrivna Wallet", Currency.Hryvnia, 15, InputPurpose.Salary));
            maWallet.wallet[1].MoneyOperation += new Wallet.OperationHandler(Display);
            maWallet.AddWallet(new Wallet("Dollar Wallet", Currency.Dollar, 15, InputPurpose.Gift));
            maWallet.wallet[2].MoneyOperation += new Wallet.OperationHandler(Display);
            maWallet.AddWallet(new Wallet("Euro Wallet", Currency.Euro, 15, InputPurpose.Salary));
            maWallet.wallet[3].MoneyOperation += new Wallet.OperationHandler(Display);

            maWallet.wallet[1].Deposit(1555, InputPurpose.Gift);
            maWallet.wallet[2].Withdrawal(21, OutputPurpose.Clothes);
            maWallet.wallet[1].TransferTo(10, maWallet.wallet[2]);
            maWallet.wallet[1].Withdrawal(13, OutputPurpose.Food);
            maWallet.wallet[1].Withdrawal(13, OutputPurpose.Alcohol);

            maWallet.wallet[3].ChangeName("new Wallet");

            maWallet.FundsWithdrawn(Currency.Hryvnia);
            maWallet.ShowHistory();
            maWallet.ShowPaymentByDate(DateTime.Now.Date);
            maWallet.ShowAmountInCurrency((Currency)1);
            Console.ReadKey();
        }
        static void Display(string mes, decimal sum)
        {
            Console.WriteLine(mes + sum);
        }
    }
}
