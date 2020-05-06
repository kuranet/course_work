using System;
using System.Collections.Generic;

namespace Program
{
    public class Wallet
    {
        #region Properties
        public string name { get; private set; }
        public Currency currency { get; private set; }
        public double amount { get; private set; }
        public List<Payment> history { get; private set; }
        #endregion

        public Wallet(string str, int val, double sum)
        {
            name = str;
            currency = (Currency)val;
            amount = sum;
            if (sum == 0)
                history = new List<Payment>();
            else
            {
                history = new List<Payment>();
                history.Add(new Payment(0, sum));
            }
        }

        #region Operations with wallet
        public void ChangeName(string newName)
        {
            name = newName;
        }

        public void PrintPaymentByDate(DateTime date)
        {
            foreach (Payment pay in history)
            {
                if (pay.time.Date == date)
                {
                    Console.WriteLine(pay.time.ToShortTimeString() + "   " + pay.sum);
                }
            }
        }

        public void PrintPaymentInPeriod(DateTime from, DateTime to)
        {
            foreach (Payment pay in history)
            {
                if (pay.time.Date >= from && pay.time.Date <= to)
                {
                    Console.WriteLine(pay.time.ToShortDateString() + "    " + pay.time.ToShortTimeString() + "   " + pay.sum);
                }
            }
        }

        #endregion

        #region Operations with money
        public void Deposit(double sum)
        {
            amount += sum;
            history.Add(new Payment(0, sum));
        }

        public void Withdrawal(double sum)
        {
            amount -= sum;
            history.Add(new Payment(2, sum));
        }

        public void TransferFrom(double sum, Wallet direction)
        {
            this.amount -= sum;
            this.history.Add(new Payment(1, sum));
            double tempSum = Metrics.metrics[(int)this.currency, (int)direction.currency] * sum;
            direction.amount += tempSum;
            direction.history.Add(new Payment(1, tempSum));
        }

        public void TransferTo(double sum, Wallet direction)
        {
            double tempSum = Metrics.metrics[(int)direction.currency, (int)this.currency] * sum;
            this.amount -= tempSum;
            this.history.Add(new Payment(1, tempSum));
            direction.amount += sum;
            direction.history.Add(new Payment(1, sum));
        }
        #endregion
    }
}
