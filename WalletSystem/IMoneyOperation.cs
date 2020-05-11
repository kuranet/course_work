using EnumLibrary;

namespace WalletSystem
{
    interface IMoneyOperation
    {
        void Deposit(decimal sum, InputPurpose pur);
        void Withdrawal(decimal sum, OutputPurpose pur);
        void TransferFrom(decimal sum, Wallet direction);
        void TransferTo(decimal sum, Wallet direction);

    }
}
