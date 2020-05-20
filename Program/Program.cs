using System;
using WalletSystem;
using EnumLibrary;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            WalletList myWallet = new WalletList();

            myWallet.AddWallet(new Wallet("Russian Wallet", Currency.Ruble, 15, InputPurpose.Salary));            
            myWallet.AddWallet(new Wallet("Hrivna Wallet", Currency.Hryvnia, 15, InputPurpose.Salary));            
            myWallet.AddWallet(new Wallet("Dollar Wallet", Currency.Dollar, 15, InputPurpose.Gift));           
            myWallet.AddWallet(new Wallet("Euro Wallet", Currency.Euro, 15, InputPurpose.Salary));
            
            bool start = true;
            Console.WriteLine("Welcome in program!\n" +
                "Choose what do you want to do: \n" +
                "1. Start\n" +
                "2. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice ==1)
            { 
                while (start)
                {
                    choice = 0;
                    bool chang = false;
                    while (!chang)
                    {
                        if (myWallet.wallet.Count == 0)
                        {

                            Console.WriteLine("Now you have not got any wallet. You can:\n" +
                                "1. Create wallet with zero balance\n" +
                                "2. Create wallet with nonzero balance\n" +
                                "10. Exit");
                            chang = Int32.TryParse(Console.ReadLine(), out choice);
                            if (choice != 1 && choice != 2 && choice != 10)
                                chang = false;
                        }
                        else
                        {
                            Console.WriteLine("Operations that you can do:\n" +
                                "1. Create wallet with zero balance\n" +
                                "2. Create wallet with nonzero balance\n" +
                                "3. Do money operations\n" +
                                "4. Show Current Amount\n" +
                                "5. Show History\n" +
                                "6. Show Payment by date\n" +
                                "7. FundsWithdrawn\n" +
                                "8. FundsRecived\n" +
                                "9. Remove wallet\n" +
                                "10. Exit");
                            chang = Int32.TryParse(Console.ReadLine(), out choice);
                        }
                        if (!chang)
                            DisplayError();
                    }

                    switch (choice)
                    {
                        case 1: // Create wallet with zero balance
                            {
                                string name = NameOfWallet(myWallet);
                                Currency cur = ChooseCurrency();
                                myWallet.AddWallet(new Wallet(name, cur));
                                break;
                            }
                        case 2: // Create wallet with nonzero balance
                            {
                                   
                                string name = NameOfWallet(myWallet);
                                Currency cur = ChooseCurrency();
                                decimal amount = SumCheck("Write initial amount of your wallet: ");
                                InputPurpose pur = InputPurpose.Else;
                                bool changed = false;
                                choice = 0;
                                while (!changed)
                                {
                                    Console.WriteLine("Choose the purpose of this amount:\n" +
                                    "1. Salary\n" +
                                    "2. Gift\n" +
                                    "3. Else");
                                    changed = Int32.TryParse(Console.ReadLine(), out choice);

                                    switch (choice)
                                    {
                                        case 1: pur = InputPurpose.Salary; changed = true; break;
                                        case 2: pur = InputPurpose.Gift; changed = true; break;
                                        case 3: pur = InputPurpose.Else; changed = true; break;
                                        default:
                                            DisplayError(); changed = false; break;
                                    }
                                }
                                myWallet.AddWallet(new Wallet(name, cur, amount, pur));
                                break;
                            }
                        case 3: // Do money operations
                            {
                                choice = 0;
                                bool changed = false;
                                while (!changed)
                                {
                                    Console.WriteLine("Choose money operation:\n" +
                                    "1. Put money on your wallet\n" +
                                    "2. Transfer current sum FROM one wallet to another\n" +
                                    "3. Transfer current sum TO one wallet from another\n" +
                                    "4. Get monet from your wallet");
                                    changed = Int32.TryParse(Console.ReadLine(), out choice);
                                    if (choice <= 0 || choice > 4)
                                    {
                                        changed = false;
                                        DisplayError();
                                    }
                                }
                                switch (choice)
                                {
                                    case 1: // Put money on your wallet
                                        {
                                            choice = 0;
                                            changed = false;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose on which wallet you would put money:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            int to = choice - 1;
                                            decimal sum = SumCheck("Write what sum whould you put on this wallet: "); ;
                                            InputPurpose pur = InputPurpose.Else;
                                            changed = false;
                                            choice = 0;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose the purpose of this amount:\n" +
                                                "1. Salary\n" +
                                                "2. Gift\n" +
                                                "3. Else");
                                                changed = Int32.TryParse(Console.ReadLine(), out choice);

                                                switch (choice)
                                                {
                                                    case 1: pur = InputPurpose.Salary; changed = true; break;
                                                    case 2: pur = InputPurpose.Gift; changed = true; break;
                                                    case 3: pur = InputPurpose.Else; changed = true; break;
                                                    default:
                                                        DisplayError(); changed = false; break;
                                                }
                                            }
                                            myWallet.wallet[to].Deposit(sum, pur);
                                            break; 
                                        }
                                    case 2: // Transfer current sum FROM one wallet to another
                                        {
                                            changed = false;
                                            choice = 0;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose from which wallet would be taken money:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            int from = choice-1;
                                            changed = false;
                                            choice = 0;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose to which wallet would be put money:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            int to = choice-1;
                                            decimal sum = SumCheck("Write what sum would you like to transfer: ");
                                            myWallet.wallet[from].TransferFrom(sum, myWallet.wallet[to]);
                                            break; 
                                        }
                                    case 3: // Transfer current sum TO one wallet from another
                                        {
                                            choice = 0;
                                            changed = false;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose from which wallet would be taken money:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            int from = choice - 1;
                                            changed = false;
                                            choice = 0;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose to which wallet would be put money:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            int to = choice - 1;
                                            decimal sum = SumCheck("Write what sum would you like to transfer: ");
                                            myWallet.wallet[from].TransferTo(sum, myWallet.wallet[to]);
                                            break;
                                        }
                                    case 4: // Get monet from your wallet
                                        {
                                            choice = 0;
                                            changed = false;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose from which wallet you would take money:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            int to = choice - 1;
                                            decimal sum = SumCheck("Write sum of withdraw: ");
                                            OutputPurpose pur = OutputPurpose.Else;
                                            changed = false;
                                            choice = 0;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose the purpose of this amount:\n" +
                                                "1. Food\n" +
                                                "2. Alcohol\n" +
                                                "3. Clothes\n" +
                                                "4. Leisure\n" +
                                                "5. Travel expenses\n" +
                                                "6. Else");
                                                changed = Int32.TryParse(Console.ReadLine(), out choice);

                                                switch (choice)
                                                {
                                                    case 1: pur = OutputPurpose.Food; changed = true; break;
                                                    case 2: pur = OutputPurpose.Alcohol; changed = true; break;
                                                    case 3: pur = OutputPurpose.Clothes; changed = true; break;
                                                    case 4: pur = OutputPurpose.Leisure; changed = true; break;
                                                    case 5: pur = OutputPurpose.TravelExpenses; changed = true; break;
                                                    case 6: pur = OutputPurpose.Else; changed = true; break;
                                                    default:
                                                        DisplayError(); changed = false; break;
                                                }
                                            }
                                            myWallet.wallet[to].Withdrawal(sum,pur);

                                            break;
                                        }
                                }
                                break;
                            }
                        case 4: // Show Current Amount
                            {
                                choice = 0;
                                bool changed = false;
                                while (!changed)
                                {
                                    Console.WriteLine("There are some info that I can show you:\n" +
                                      "1. Amount for each wallet in your wallets\n" +
                                      "2. Amount for current wallet\n" +
                                      "3. Amount for each wallet in your wallets, that has current currency\n" +
                                      "4. How much money does I have in current currency?\n" +
                                      "5. Go back");
                                    changed = Int32.TryParse(Console.ReadLine(), out choice);
                                    if (choice <= 0 || choice > 5)
                                    {
                                        changed = false;
                                        DisplayError();
                                    }
                                }
                                switch (choice)
                                {
                                    case 1: // Amount for each wallet in your wallets
                                        { 
                                            myWallet.ShowCurrentAmount(); 
                                            break; 
                                        }
                                    case 2: // Amount for current wallet
                                        {
                                            changed = false;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose amount of which wallet wounl be shown:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            myWallet.ShowCurrentAmount(myWallet.wallet[choice - 1]);
                                            break;
                                        }
                                    case 3: // Amount for each wallet in your wallets, that has current currency
                                        {
                                            Currency cur = ChooseCurrency();
                                            myWallet.ShowCurrentAmount(cur);
                                            break;
                                        }
                                    case 4: // How much money does I have in current currency?
                                        {
                                            Currency cur = ChooseCurrency();
                                            myWallet.ShowAmountInCurrency(cur);
                                            break;
                                        }
                                    case 5: // Go back
                                        break;
                                }

                                break;
                            }
                        case 5: // Show History
                            {
                                choice = 0;
                                bool changed = false;
                                while (!changed)
                                {
                                    Console.WriteLine("There is some info that I can show you:\n" +
                                      "1. History for each wallet in your wallets\n" +
                                      "2. History for current wallet\n" +
                                      "3. History for each wallet in your wallets, that has current currency\n" +
                                      "4. Go back");
                                    changed = Int32.TryParse(Console.ReadLine(), out choice);
                                    if (choice <= 0 || choice > 4)
                                    {
                                        changed = false;
                                        DisplayError();
                                    } 
                                }
                                switch (choice)
                                {
                                    case 1: // History for each wallet in your wallets
                                        {
                                            myWallet.ShowHistory();
                                            break; 
                                        }
                                    case 2: // History for current wallet
                                        {
                                            changed = false;
                                            while (!changed)
                                            {
                                                Console.WriteLine("Choose amount of which wallet wounl be shown:");
                                                (choice, changed) = ShowWalletAndChoose(myWallet);
                                            }
                                            myWallet.ShowHistory(myWallet.wallet[choice - 1]);
                                            break;
                                        }
                                    case 3: // History for each wallet in WalletList, that has current currency
                                        {
                                            Currency cur = ChooseCurrency();
                                            myWallet.ShowHistory(cur);
                                            break;
                                        }
                                    case 4: // Go back
                                        break;
                                }
                                break; 
                            }
                        case 6: // Show Payment by date
                            {
                                choice = 0;
                                bool changed = false;
                                while (!changed)
                                {
                                    Console.WriteLine("There is some info that I can show you:\n" +
                                      "1. Show payments by date\n" +
                                      "2. Show payments in period\n" +
                                      "3. Go back");
                                    changed = Int32.TryParse(Console.ReadLine(), out choice);
                                    if (choice <= 0 || choice > 3)
                                    {
                                        changed = false;
                                        DisplayError();
                                    }
                                }
                                switch (choice)
                                {
                                    case 1: // Show payments by date
                                        {
                                            Console.Write("Write what date you would like to see all payments (dd.mm.yyyy): ");
                                            DateTime date = Convert.ToDateTime(Console.ReadLine());
                                            Console.WriteLine();
                                            myWallet.ShowPaymentByDate(date);
                                            break; 
                                        }
                                    case 2: // Show payments in period
                                        {
                                            Console.Write("Write what date you would like to see all payments from (dd.mm.yyyy): ");
                                            DateTime datefrom = Convert.ToDateTime(Console.ReadLine());
                                            Console.Write("Write what date you would like to see all payments to (dd.mm.yyyy): ");
                                            DateTime dateto = Convert.ToDateTime(Console.ReadLine());
                                            Console.WriteLine();
                                            myWallet.ShowPaymentInPeriod(datefrom, dateto);
                                            break; 
                                        }
                                    case 3: // Go back
                                        break;
                                }
                                
                                break; 
                            }
                        case 7: // FundsWithdrawn
                            {
                                Currency cur = ChooseCurrency();
                                myWallet.FundsWithdrawn(cur);
                                break;
                                 
                            }
                        case 8: // FundsRecived
                            {
                                Currency cur = ChooseCurrency();
                                myWallet.FundsRecived(cur);
                                break;                                 
                            }
                        case 9: // Remove wallet
                            {
                                choice = 0;
                                bool changed = false;
                                while (!changed)
                                {
                                    Console.WriteLine("Choose which wallet to remove:");
                                    (choice, changed) = ShowWalletAndChoose(myWallet);
                                }
                                myWallet.RemoveWallet(myWallet.wallet[choice - 1]);
                                break;
                            }
                        case 10: // Exit
                            { 
                                start = false; 
                                break; 
                            }
                        default: 
                            {
                                DisplayError();
                                break; 
                            }
                    }
                }                                   
            }
        }
        static string NameOfWallet(WalletList myWallet)
        {
            bool changed = false;
            string name = "";
            while (!changed)
            {
                Console.Write("Write name of your wallet: ");
                name = Console.ReadLine();
                bool isFound = false;
                int i = 0;
                while(i < myWallet.wallet.Count && !isFound)
                {
                    if (myWallet.wallet[i].name == name)
                    {
                        Console.WriteLine("This name alredy exsists\n" +
                            "Try again\n");
                        isFound = true;
                    }
                    i++;
                }

                if (i == myWallet.wallet.Count && !isFound)
                    changed = true;
            }            
            return name;
        }

        static decimal SumCheck(string mes)
        {
            bool changed = false;
            decimal sum = 0;
            while (!changed)
            {
                Console.Write(mes);
                changed = Decimal.TryParse(Console.ReadLine(), out sum);
                if (!changed)
                    Console.WriteLine("Sum must countain only numbers\n" +
                        "Try again\n");
            }
            return sum;
        }

        static (int, bool) ShowWalletAndChoose(WalletList myWallet)
        {
            bool changed = false;
            for (int i = 0; i < myWallet.wallet.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {myWallet.wallet[i].name}");
            }
            int choice = 0;
            changed = Int32.TryParse(Console.ReadLine(), out choice);
            if (choice > myWallet.wallet.Count || choice <= 0)
            {
                changed = false;
                DisplayError();
            }
            return (choice, changed);
        }

        static Currency ChooseCurrency()
        {
            Currency cur = Currency.Euro;
            bool changed = false;
            int choice = 0;
            while (!changed)
            {
                Console.WriteLine("Choose currency of your wallet:\n" +
                "1. Ruble\n" +
                "2. Hrywnia\n" +
                "3. Dollar\n" +
                "4. Euro");
                changed = Int32.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1: cur = Currency.Ruble; changed = true; break;
                    case 2: cur = Currency.Hryvnia; changed = true; break;
                    case 3: cur = Currency.Dollar; changed = true; break;
                    case 4: cur = Currency.Euro; changed = true; break;
                    default:
                        DisplayError(); changed = false; break;
                }
            }
            return cur;
        }

        static void DisplayError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There is no such option");
            Console.ResetColor();
            Console.WriteLine("Try it again\n");
        }
    }
}
