using EnumLibrary;

namespace WalletSystem
{
    interface IMoneyOperation
    {
        void Deposit(double sum, InputPurpose pur);
        void Withdrawal(double sum, OutputPurpose pur);
        void TransferFrom(double sum, Wallet direction);
        void TransferTo(double sum, Wallet direction);

    }
}
