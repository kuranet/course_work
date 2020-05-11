namespace WalletSystem
{
    class Transfer:Payment
    {
        public Wallet from { get; private set; }
        public Wallet to { get; private set; }
        public Transfer( double tempSum, Wallet f, Wallet t) : base( tempSum) { from = f; to = t; }
    }
}
