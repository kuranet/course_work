using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    sealed class RubWallet : Wallet
    {
        const double Rub2Dol = 0.0134;
        public RubWallet(string str, double sum) :base(str, sum) { }
        public RubWallet(Wallet wal):base(wal){ }

        override public void TransferFrom(double sum, ref RubWallet direction)
        {
            base.TransferFrom(sum, ref direction);           
            double tempSum = 1 * sum;
            direction.amount += tempSum;
            direction.history.Add(new Payment(1, tempSum));           
        }
        override public void TransferFrom(double sum, ref DolWallet direction)
        {
            base.TransferFrom(sum, ref direction);
            double tempSum = 1 * sum;
            
            direction.history.Add(new Payment(1, tempSum));
        }
    }
}
