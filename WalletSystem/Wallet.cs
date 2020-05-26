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
        public string name { get; private set; } // name of wallet
        public Currency currency { get; private set; } // currency of wallet
        public decimal amount { get; private set; } // sum of money on this wallet
        public List<Payment> history { get; private set; } // history of all payments of this wallet
        #endregion

        // Create zero-balance wallet
        public Wallet(string str, Currency val)
        {
            if (str == null || str.Length < 1)
                throw new ArgumentException ("Such name can't be used");
            name = str;
            currency = val;
            amount = 0;
            history = new List<Payment>();
            // Add events
            if (SuccesOperation == null)
                SuccesOperation += new OperationHandler(DisplaySucces);
            if (ErrorMessage == null)
                ErrorMessage += new OperationHandler(DisplayError);
            // Show succes
            SuccesOperation?.Invoke($"Wallet \"{this.name}\" has been created \n" +
                "Amount of this wallet is: ",amount);
        }

        // Create wallet with non-zero balance
        public Wallet(string str, Currency val, decimal sum, InputPurpose pur)
        {
            if (str == null || str.Length < 1)
                throw new ArgumentException("Such name can't be used");
            name = str;
            currency = val;
            if (sum <= 0)
                throw new ArgumentException("Amount of your wallet can't be less than 0");
            amount = sum;            
            history = new List<Payment>();
            history.Add(new InputPayment( sum, pur));
            // Add events
            if(SuccesOperation == null)
                SuccesOperation += new OperationHandler(DisplaySucces);
            if (ErrorMessage == null)
                ErrorMessage += new OperationHandler(DisplayError);
            // Show succes
            SuccesOperation?.Invoke($"Wallet \"{this.name}\" has been created \n" +
                "Amount of this wallet is: ", amount);
        }

        #region IWalletOperation
        // Change name of this wallet
        public void ChangeName(string newName)
        {
            name = newName;
        }
        #endregion

        #region IMoneyOperation
        // Operation to put money on the wallet
        public void Deposit(decimal sum , InputPurpose pur)
        {
            amount += sum; // Add money to amount
            history.Add(new InputPayment( sum, pur)); // Add deposit payment into history
            SuccesOperation?.Invoke($"Amount of \"{this.name}\" wallet is: ", this.amount); // Show succes
        }

        // Operation to get money from the wallet
        public void Withdrawal(decimal sum, OutputPurpose pur)
        {
            if (sum > this.amount) // If sum to withdraw is greater than the amount of this wallet
                ErrorMessage?.Invoke("You hane not enought money to do it\n"+ // Show error
                    $"Amount of \"{this.name}\" is: ", this.amount);
            else
            {
                amount -= sum; // Withdraw this sum
                history.Add(new OutputPayment(sum, pur)); // Add withdrawal payment into history
                SuccesOperation?.Invoke($"Amount of \"{this.name}\" wallet is: ", this.amount); // Show succes  
            }
        }

        // Operation to transfer sum FROM wallet to wallet
        public void TransferFrom(decimal sum, Wallet direction)
        {
            if (sum > this.amount) // If sum to transfer is greater than the amount of this wallet
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

        // Operation to transfer sum TO wallet from wallet
        public void TransferTo(decimal sum, Wallet direction)
        {
            Metrics metr = new Metrics();
            decimal tempSum = metr.metrics[(int)direction.currency, (int)this.currency] * sum;
            if (tempSum > this.amount) // If sum to transfer is greater than the amount of this wallet
                ErrorMessage?.Invoke("You hane not enought money to do it\n"+ // Show error
                    $"Amount of \"{this.name}\" wallet is: ", this.amount);
            else
            {
                this.amount -= tempSum;
                Transfer transfer = new Transfer(tempSum, this, direction);
                this.history.Add(transfer);
                direction.amount += sum;
                transfer = new Transfer(sum, this, direction);
                direction.history.Add(transfer);
                SuccesOperation?.Invoke($"Amount of \"{this.name}\" wallet is: {this.amount}\n" + // Show succes
                    $"Amount of \"{direction.name}\" wallet is: ", direction.amount);
            }
        }
        #endregion

        #region Event methods
        // Event's operation to display succes
        static void DisplaySucces(string mes, decimal sum)
        {
            Console.WriteLine();            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succes!");
            Console.ResetColor();            
            Console.WriteLine(mes + sum + "\n");
        }
        // Event's operation to display error
        static void DisplayError(string mes, decimal sum)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error!");
            Console.ResetColor();
            Console.WriteLine(mes + sum + "\n");
        }
        #endregion
    }
}
