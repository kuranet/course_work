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

        public Wallet(string str, Currency val, double sum)
        {
            if (str == null || str.Length < 1)
                throw new ArgumentException ("Such name can't be used");
            name = str;
            currency = val;
            if (sum < 0)
                throw new ArgumentException("Amount of your wallet can't be less than 0");
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
        #endregion

        #region Operations with money
        public void Deposit(double sum)
        {
            amount += sum;
            history.Add(new Payment(TypeOfPayment.Deposit, sum));
        }

        public void Withdrawal(double sum)
        {
            if (sum > this.amount)
                throw new ArgumentException("You have enought money to do it");
            amount -= sum;
            history.Add(new Payment(TypeOfPayment.Withdrawal, sum));
        }

        public void TransferFrom(double sum, Wallet direction)
        {
            if (sum > this.amount)
                throw new ArgumentException("You have enought money to do it");
            this.amount -= sum;
            this.history.Add(new Payment(TypeOfPayment.Deposit, sum));
            double tempSum = Metrics.metrics[(int)this.currency, (int)direction.currency] * sum;
            direction.amount += tempSum;
            direction.history.Add(new Payment(TypeOfPayment.Deposit, tempSum));
        }

        public void TransferTo(double sum, Wallet direction)
        {
            double tempSum = Metrics.metrics[(int)direction.currency, (int)this.currency] * sum;
            if (tempSum > this.amount)
                throw new ArgumentException("You have enought money to do it");
            this.amount -= tempSum;
            this.history.Add(new Payment(TypeOfPayment.Deposit, tempSum));
            direction.amount += sum;
            direction.history.Add(new Payment(TypeOfPayment.Deposit, sum));
        }
        #endregion
    }
}
