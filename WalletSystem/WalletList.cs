using System;
using System.Collections.Generic;
using EnumLibrary;

namespace WalletSystem
{
    public class WalletList
    {
        public List<Wallet> wallet;

        public WalletList() { wallet = new List<Wallet>(); }

        public void AddWallet(Wallet wal)
        {
            wallet.Add(wal);
        }

        public void RemoveWallet(Wallet wal)
        {
            if (wal.amount > 0)
                Console.WriteLine("Firstly, you should transfer money from yhis wallet to another one\n" +
                    $"Amount of this wallet is: {wal.amount}\n");
            else
            {
                wallet.Remove(wal);
                Console.WriteLine("Succes!\n" +
                    $"Wallet \"{wal.name}\" has been removed\n");
            }
                
        }

        #region Show Current Amount
        // Amount for each wallet in WalletList
        public void ShowCurrentAmount()
        {
            Console.WriteLine("Amount of your wallets are: ");
            foreach (Wallet wal in wallet)
            {
                string val = FindCurrency(wal.currency);
                Console.WriteLine(wal.name + "    " + wal.amount + $" {val}");
            }
            Console.WriteLine();
        }

        // Amount for current wallet
        public void ShowCurrentAmount(Wallet wal)
        {
            string val = FindCurrency(wal.currency);
            Console.WriteLine(wal.name + "    " + wal.amount + $" {val}\n");
            Console.WriteLine();
        }

        // Amount for each wallet in WalletList, that has current currency
        public void ShowCurrentAmount(Currency type)
        {
            string val = FindCurrency(type);
            int numOfWallets = 0;
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == type)
                {
                    numOfWallets += 1;
                    Console.WriteLine(wal.name + "    " + wal.amount + $" {val}");
                }
            }
            if (numOfWallets == 0)
                Console.WriteLine("You don't have any wallet of this currency\n");
            Console.WriteLine();
        }

        // Sum of wallets' amount in he given currency 
        public void ShowAmountInCurrency(Currency type)
        {
            int numOfWallets = 0;
            decimal tempSum = 0;
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == type)
                {
                    tempSum += wal.amount;
                    numOfWallets += 1;
                }
                    
            }
            if (numOfWallets == 0)
                Console.WriteLine("You don't have any wallet of this currency");
            else
            {
                string val = FindCurrency(type);
                Console.WriteLine("In your " + type + " wallets you have " + tempSum + $" {val}");
            }
            Console.WriteLine();
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
                    if (pay is Transfer)
                    {
                        Transfer p = (Transfer)pay;
                        if(p.from.name == wal.name)
                            Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum +  "  To  " + p.to.name);
                        else
                            Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name); 
                    }

                    else
                        Console.WriteLine(pay.time + "      " + pay.GetType().Name + "    " + pay.sum );
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
                if (pay is Transfer)
                {
                    Transfer p = (Transfer)pay;
                    if (p.from.name == wal.name)
                        Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  To  " + p.to.name);
                    else
                        Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name);
                }

                else
                    Console.WriteLine(pay.time + "      " + pay.GetType().Name + "    " + pay.sum);
            }
            Console.WriteLine();
        }
                
        // History for each wallet in WalletList, that has current currency    
        public void ShowHistory(Currency type)
        {
            int numOfWallet = 0;
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == type)
                {
                    numOfWallet += 1;
                    Console.WriteLine(wal.name);
                    foreach (Payment pay in wal.history)
                    {
                        if (pay is Transfer)
                        {
                            Transfer p = (Transfer)pay;
                            if (p.from.name == wal.name)
                                Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  To  " + p.to.name);
                            else
                                Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name);
                        }

                        else
                            Console.WriteLine(pay.time + "      " + pay.GetType().Name + "    " + pay.sum);
                    }
                    Console.WriteLine();
                }
            }
            if (numOfWallet == 0)
                Console.WriteLine("You don't have any wallet of this currency");
        }
        #endregion

        #region Show Payment by date
        public void ShowPaymentByDate(DateTime date)
        {
            int numOfPay = 0;
            foreach (Wallet wal in wallet)
            {
                string val = FindCurrency(wal.currency);

                Console.WriteLine(wal.name);
                foreach (Payment pay in wal.history)
                {
                    if (pay.time.Date == date)
                    {
                        numOfPay += 1;
                        if (pay is Transfer)
                        {
                            Transfer p = (Transfer)pay;
                            if (p.from.name == wal.name)
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + $" {val}" +  "  To  " + p.to.name);
                            else
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + $" {val}" + "  From  " + p.from.name);
                        }

                        else
                            Console.WriteLine(pay.time.ToShortTimeString() + "   " + pay.GetType().Name + "   " + +pay.sum + $" {val}");
                    }
                }
                if (numOfPay == 0)
                    Console.WriteLine("There was not done any payment this date");
                numOfPay = 0;
                Console.WriteLine();
            }
        }

        public void ShowPaymentInPeriod(DateTime from, DateTime to)
        {
            int numOfPay = 0;
            foreach (Wallet wal in wallet)
            {
                string val = FindCurrency(wal.currency);
                Console.WriteLine(wal.name);
                foreach (Payment pay in wal.history)
                {
                    if (pay.time.Date >= from && pay.time.Date <= to)
                    {
                        numOfPay += 1;
                        if (pay is Transfer)
                        {
                            Transfer p = (Transfer)pay;
                            if (p.from.name == wal.name)
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + $" {val} " + "  To  " + p.to.name);
                            else
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + $" {val} " + "  From  " + p.from.name);
                        }
                        else
                            Console.WriteLine(pay.time.ToShortDateString() + "    " + pay.time.ToShortTimeString() + "   " + pay.sum + $" {val} ");
                    }
                }
                if (numOfPay == 0)
                    Console.WriteLine("There was not done any payment this date");
                Console.WriteLine();
                numOfPay = 0;
            }            
        }
        #endregion

        public void FundsWithdrawn(Currency currency)
        {
            int count = 0;
            for(OutputPurpose i = OutputPurpose.Food; i <= OutputPurpose.Else; i++)
            {
                count++;
            }
            decimal[,] expenses = new decimal[count,2];
            decimal allExpenses = 0;
            foreach(Wallet wal in wallet)
            {
                if (wal.currency == currency)
                {
                    int n = wal.history.Count;
                    for (int i = 0; i < n; i++)
                    {
                        if (wal.history[i] is OutputPayment)
                        {
                            OutputPayment pay = (OutputPayment)wal.history[i];
                            expenses[(int)pay.purpose,0] += wal.history[i].sum;
                            allExpenses += wal.history[i].sum;
                        }
                    }
                }
            }
            string val = FindCurrency(currency);
            if (allExpenses == 0)
                Console.WriteLine("There was not any payment in this currency");
            else
                for (int i = 0; i < expenses.Length / 2; i++)
                {
                    expenses[i, 1] = expenses[i, 0] / allExpenses;
                    Console.WriteLine((OutputPurpose)i + "    " + expenses[i, 0] + $" {val} " + expenses[i, 1] * 100 + "%");
                }
            Console.WriteLine();
        }

        public void FundsRecived(Currency currency)
        {
            int count = 0;
            for (InputPurpose i = InputPurpose.Salary; i <= InputPurpose.Else; i++)
            {
                count++;
            }
            decimal[,] funds = new decimal[count,2];
            decimal allFunds = 0;
            foreach (Wallet wal in wallet)
            {
                if (wal.currency == currency)
                {
                    int n = wal.history.Count;
                    for (int i = 0; i < n; i++)
                    {
                        if (wal.history[i] is InputPayment)
                        {
                            InputPayment pay = (InputPayment)wal.history[i];
                            funds[(int)pay.purpose,0] += wal.history[i].sum;
                            allFunds += wal.history[i].sum;
                        }
                    }
                }
            }
            string val = FindCurrency(currency);
            if (allFunds == 0)
                Console.WriteLine("There was not any payment in this currency");
            else
                for (int i = 0; i < funds.Length / 2; i++)
                {
                    funds[i, 1] = Math.Round(funds[i, 0] / allFunds, 5);
                    Console.WriteLine((InputPurpose)i + "    " + funds[i, 0] + $" {val} " + funds[i, 1] * 100 + "%");
                }
            Console.WriteLine();
        }


        static string FindCurrency(Currency currency)
        {
            string val = "";
            switch (currency)
            {
                case Currency.Ruble: val = "RUB"; break;
                case Currency.Hryvnia: val = "UAH"; break;
                case Currency.Dollar: val = "USD"; break;
                case Currency.Euro: val = "EUR"; break;
            }
            return val;
        }
    }
}
