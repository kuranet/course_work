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
            decimal tempSum = 0;
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
                    if (pay is Transfer)
                    {
                        Transfer p = (Transfer)pay;
                        if(p.from.name == wal.name)
                            Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum +  "  To  " + p.to.name);
                        else
                            Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name); 
                    }

                    else
                        Console.WriteLine(pay.time + "      " + pay.GetType().Name + "    " + pay.sum);
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
                    Console.WriteLine(pay.time + "      " + pay.GetType() + "    " + pay.sum);
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
                        if (pay is Transfer)
                        {
                            Transfer p = (Transfer)pay;
                            if (p.from.name == wal.name)
                                Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  To  " + p.to.name);
                            else
                                Console.WriteLine(p.time + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name);
                        }

                        else
                            Console.WriteLine(pay.time + "      " + pay.GetType() + "    " + pay.sum);
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
                        if (pay is Transfer)
                        {
                            Transfer p = (Transfer)pay;
                            if (p.from.name == wal.name)
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + "  To  " + p.to.name);
                            else
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name);
                        }

                        else
                            Console.WriteLine(pay.time.ToShortTimeString() + "   " + pay.GetType().Name + "   " + +pay.sum);
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
                        if (pay is Transfer)
                        {
                            Transfer p = (Transfer)pay;
                            if (p.from.name == wal.name)
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + "  To  " + p.to.name);
                            else
                                Console.WriteLine(p.time.ToShortTimeString() + "      " + pay.GetType().Name + "    " + pay.sum + "  From  " + p.from.name);
                        }
                        else
                            Console.WriteLine(pay.time.ToShortDateString() + "    " + pay.time.ToShortTimeString() + "   " + pay.sum);
                    }
                }
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
            for(int i = 0; i< expenses.Length/2; i++)
            {
                expenses[i,1] = expenses[i,0] / allExpenses;
                Console.WriteLine((OutputPurpose)i + "    " + expenses[i, 0] + " y.e " + expenses[i, 1] * 100 + "%");
            }
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
            for (int i = 0; i < funds.Length/2; i++)
            {
                funds[i,1] = Math.Round( funds[i,0] / allFunds, 5);
                Console.WriteLine((InputPurpose)i + "    " + funds[i,0] + " y.e " + funds[i,1]*100 + "%");
            }
        }
    }
}
