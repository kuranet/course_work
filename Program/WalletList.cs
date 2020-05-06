using System;
using System.Collections.Generic;

namespace Program
{
    class WalletList
    {
        public List<Wallet> wallet;

        public WalletList() { wallet = new List<Wallet>(); }

        public void AddWallet(Wallet wal)
        {
            wallet.Add(wal);
        }

        #region Show Current Amount
        // Amount for each wallet in WalletList
        public void ShowCurrentAmount()
        {
            foreach (Wallet wal in wallet)
            {
                Console.WriteLine(wal.name + "    " + wal.amount);
            }
        }

        // Amount for current wallet
        public void ShowCurrentAmount(Wallet wal)
        {
            Console.WriteLine(wal.name + "    " + wal.amount);
        }

        // Amount for each wallet in WalletList, that has current currency
        public void ShowCurrentAmount(Currency type)
        {
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == type)
                    Console.WriteLine(wal.name + "    " + wal.amount);
            }
        }

        // Sum of wallets' amount in he given currency 
        public void ShowAmountInCurrency(Currency type)
        {
            double tempSum = 0;
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == type)
                    tempSum += wal.amount;
            }
            Console.WriteLine("In your " + type + " wallets you have " + tempSum + " y.e.");

        }
        #endregion

        #region Show History
        // History for each wallet in WalletList
        public void ShowHistory()
        {
            foreach (Wallet wal in wallet)
            {
                Console.WriteLine(wal.name);
                foreach (Payment pay in wal.history)
                {
                    Console.WriteLine(pay.time + "      " + pay.type + "    " + pay.sum);
                }
                Console.WriteLine();
            }
        }

        // History for current wallet in WalletList
        public void ShowHistory(Wallet wal)
        {
            Console.WriteLine(wal.name);
            foreach (Payment pay in wal.history)
            {
                Console.WriteLine(pay.time + "      " + pay.type + "    " + pay.sum);
            }
            Console.WriteLine();
        }
                
        // History for each wallet in WalletList, that has current currency    
        public void ShowHistory(Currency type)
        {
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == type)
                {
                    Console.WriteLine(wal.name);
                    foreach (Payment pay in wal.history)
                    {
                        Console.WriteLine(pay.time + "      " + pay.type + "    " + pay.sum);
                    }
                    Console.WriteLine();
                }
            }
        }
        #endregion

        #region Show Payment by date
        public void ShowPaymentByDate(DateTime date)
        {
            foreach (Wallet wal in wallet)
            {
                Console.WriteLine(wal.name);
                foreach (Payment pay in wal.history)
                {
                    if (pay.time.Date == date)
                    {
                        Console.WriteLine(pay.time.ToShortTimeString() + "   " + pay.type + "   " + +pay.sum);
                    }
                }
                Console.WriteLine();
            }
        }

        public void ShowPaymentInPeriod(DateTime from, DateTime to)
        {
            foreach (Wallet wal in wallet)
            {
                foreach (Payment pay in wal.history)
                {
                    if (pay.time.Date >= from && pay.time.Date <= to)
                    {
                        Console.WriteLine(pay.time.ToShortDateString() + "    " + pay.time.ToShortTimeString() + "   " + pay.sum);
                    }
                }
            }
        }
        #endregion
    }
}
