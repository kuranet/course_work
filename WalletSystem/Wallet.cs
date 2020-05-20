using System;
using System.Collections.Generic;
using EnumLibrary;

namespace WalletSystem
{
    public delegate void OperationHandler(string message, decimal sum);
    public class Wallet : IMoneyOperation, IWalletOperation
    {
        static event OperationHandler SuccesOperation, ErrorMessage;

        #region Properties
        public string name { get; private set; }
        public Currency currency { get; private set; }
        public decimal amount { get; private set; }
        public List<Payment> history { get; private set; }
        #endregion

        public Wallet(string str, Currency val)
        {
            if (str == null || str.Length < 1)
                throw new ArgumentException ("Such name can't be used");
            name = str;
            currency = val;
            amount = 0;
            history = new List<Payment>();
            if (SuccesOperation == null)
                SuccesOperation += new OperationHandler(DisplaySucces);
            if (ErrorMessage == null)
                ErrorMessage += new OperationHandler(DisplayError);

            SuccesOperation?.Invoke($"Wallet \"{this.name}\" has been created \n" +
                "Amount of this wallet is: ",amount);
            
        }

        public Wallet(string str, Currency val, decimal sum, InputPurpose pur)
        {
            if (SuccesOperation == null)
                SuccesOperation += new OperationHandler(DisplaySucces);
            if (ErrorMessage == null)
                ErrorMessage += new OperationHandler(DisplayError);
            
            if (str == null || str.Length < 1)
                throw new ArgumentException("Such name can't be used");
            name = str;
            currency = val;
            if (sum <= 0)
                throw new ArgumentException("Amount of your wallet can't be less than 0");
            amount = sum;            
            history = new List<Payment>();
            history.Add(new InputPayment( sum, pur));

            SuccesOperation?.Invoke($"Wallet \"{this.name}\" has been created \n" +
                "Amount of this wallet is: ", amount);

        }

        #region IWalletOperation
        public void ChangeName(string newName)
        {
            name = newName;
        }
        #endregion

        #region IMoneyOperation
        public void Deposit(decimal sum , InputPurpose pur)
        {
            amount += sum;
            history.Add(new InputPayment( sum, pur));
            SuccesOperation?.Invoke($"Amount of \"{this.name}\" wallet is: ", this.amount);
        }

        public void Withdrawal(decimal sum, OutputPurpose pur)
        {
            if (sum > this.amount)
                ErrorMessage?.Invoke("You hane not enought money to do it\n"+
                    $"Amount of \"{this.name}\" is: ", this.amount);
            else
            {
                amount -= sum;
                history.Add(new OutputPayment(sum, pur));
                SuccesOperation?.Invoke("Amount of \"{this.name}\" wallet is: ", this.amount);
            }
        }

        public void TransferFrom(decimal sum, Wallet direction)
        {
            if (sum > this.amount)
                ErrorMessage?.Invoke($"You hane not enought money to do it\n"+
                    $"Amount of \"{this.name}\" wallet is: ", this.amount);
            else
            {
                this.amount -= sum;
                Transfer transfer = new Transfer(sum, this, direction);
                this.history.Add(transfer);
                Metrics metr = new Metrics();
                decimal tempSum = metr.metrics[(int)this.currency, (int)direction.currency] * sum;
                direction.amount += tempSum;
                transfer = new Transfer(tempSum, this, direction);
                direction.history.Add(transfer);
                SuccesOperation?.Invoke($"Amount of \"{this.name}\" wallet is: {this.amount}\n"+
                    $"Amount of \"{direction.name}\" wallet is: ", direction.amount);
            }
        }

        public void TransferTo(decimal sum, Wallet direction)
        {
            Metrics metr = new Metrics();
            decimal tempSum = metr.metrics[(int)direction.currency, (int)this.currency] * sum;
            if (tempSum > this.amount)
                ErrorMessage?.Invoke("You hane not enought money to do it\n"+
                    $"Amount of \"{this.name}\" wallet is: ", this.amount);
            else
            {
                this.amount -= tempSum;
                Transfer transfer = new Transfer(tempSum, this, direction);
                this.history.Add(transfer);
                direction.amount += sum;
                transfer = new Transfer(sum, this, direction);
                direction.history.Add(transfer);
                SuccesOperation?.Invoke($"Amount of \"{this.name}\" wallet is: {this.amount}\n" +
                    $"Amount of \"{direction.name}\" wallet is: ", direction.amount);
            }
        }
        #endregion

        static void DisplaySucces(string mes, decimal sum)
        {
            Console.WriteLine();            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succes!");
            Console.ResetColor();            
            Console.WriteLine(mes + sum + "\n");
        }

        static void DisplayError(string mes, decimal sum)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error!");
            Console.ResetColor();
            Console.WriteLine(mes + sum + "\n");
        }
    }
}
