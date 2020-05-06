using System;
using System.Collections.Generic;

namespace Program
{
    class Wallet
    {
        #region Properties
        public string name { get; protected set; }
        public double amount { get; private set; }
        public List<Payment> history { get; protected set; }
        #endregion

        public Wallet(string str, double sum) 
        { 
            name = str; 
            amount = sum; 
>>>>>>> d0a60a1... Main logic of rhe program
            if (sum == 0)
                history = new List<Payment>();
            else
            {
                history = new List<Payment>();
                history.Add(new Payment(0, sum));
            }
        }

        public Wallet(Wallet wal) {
            this.amount = wal.amount;
            this.name = wal.name;
            this.history = wal.history;
        }

        #region Operations with wallet
<<<<<<< HEAD
        public void ChangeName(string newName)
=======
        public void ChangeName(string newName) 
>>>>>>> d0a60a1... Main logic of rhe program
        {
            name = newName;
        }

<<<<<<< HEAD
        public void PrintPaymentByDate(DateTime date)
        {
            foreach (Payment pay in history)
=======
        public void PrintPaymentByDate(DateTime date) 
        {
            foreach(Payment pay in history)
>>>>>>> d0a60a1... Main logic of rhe program
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
<<<<<<< HEAD
                if (pay.time.Date >= from && pay.time.Date <= to)
=======
                if (pay.time.Date >= from && pay.time.Date <= to )
>>>>>>> d0a60a1... Main logic of rhe program
                {
                    Console.WriteLine(pay.time.ToShortDateString() + "    " + pay.time.ToShortTimeString() + "   " + pay.sum);
                }
            }
        }

        #endregion

        #region Operations with money
        public void Deposit(double sum)
<<<<<<< HEAD
        {
=======
        {            
>>>>>>> d0a60a1... Main logic of rhe program
            amount += sum;
            history.Add(new Payment(0, sum));
        }

        public void Withdrawal(double sum)
        {
            amount -= sum;
            history.Add(new Payment(2, sum));
        }

        virtual public void TransferFrom(double sum,ref RubWallet direction) {
            this.amount -= sum;
            this.history.Add(new Payment(1, sum));
            
        }
        virtual public void TransferFrom(double sum, ref DolWallet direction) {
            this.amount -= sum;
            this.history.Add(new Payment(1, sum));
        }
        //virtual public void TransferFrom(double sum, ref Wallet direction, Type typeOfWallet) { }
        //virtual public void TransferFrom(double sum, ref Wallet direction, Type typeOfWallet) { }

        
        //public void TransferTo(double sum, Wallet direction)
        //{
        //    double tempSum = Metrics.metrics[(int)direction.currency, (int)this.currency] * sum;
        //    this.amount -= tempSum;
        //    this.history.Add(new Payment(1, tempSum));
        //    direction.amount += sum;
        //    direction.history.Add(new Payment(1, sum));
        //}
        #endregion
    }
}
