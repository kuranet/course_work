using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Wallet> myWallets = new List<Wallet>();
            Wallet rub = new RubWallet("Russian Wallet", 15);
            myWallets.Add(rub);
            myWallets.Add(new Wallet("Hrivna Wallet", 15));
            myWallets.Add(new RubWallet("Dollar Wallet", 15));
            myWallets.Add(new Wallet("Euro Wallet", 15));
            myWallets[1].Deposit(1500);
            myWallets[1].Withdrawal(15);
            RubWallet k = (RubWallet) myWallets[2];
            myWallets[0].TransferFrom(15, ref k);
            myWallets[2] = k;
            //myWallets[1].TransferTo(10, myWallets[2]);

            foreach (Wallet wal in myWallets)
            {
                Console.WriteLine(wal.name + "    " + wal.amount);
                foreach (Payment pay in wal.history)
                {
                    Console.WriteLine(pay.time + "      " + pay.type + "    " + pay.sum);
                }
                Console.WriteLine();
                wal.PrintPaymentInPeriod(new DateTime(2020, 3, 20), new DateTime(2020, 5, 20));
                Console.WriteLine();
            }
        }
    }
}
