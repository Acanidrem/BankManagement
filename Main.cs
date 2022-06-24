using System;
using System.Collections.Generic;
using System.IO;
namespace BankAccount {
  class Account{
    public string name = "";
    public string type = "";
    private int balance = 0;
    private List<string> history = new List<string>();

    public void saveTransactions(){
      // TODO Write content
      string filename = (type + ".txt");
      
      using(StreamWriter summary = new StreamWriter(filename,true)){
        history.ForEach(elem => summary.WriteLine(elem));
      }
    }

    public void addToHistory(int value, bool isAdding){
      char symbol = '-';
      if(isAdding){
        symbol = '+';
      }
      DateTime date = new DateTime();
      history.Add("Transaction: " + symbol + "$" + value + " at " + date.ToString() + " Current Balance: $"+balance);
    }

    public void askDeposit(){
      Console.WriteLine("How much would you like to deposit?");
      string selectedOption = Console.ReadLine();
      try{
        int toDepose = int.Parse(selectedOption);
        deposit(toDepose);
        Console.WriteLine("You deposited: $"+toDepose);
        addToHistory(toDepose, true);
      }catch(FormatException e){
        Console.WriteLine("Invalid value !");
        Console.WriteLine(e.Message);
      }
      Program.hitToMenu();
    }

    public void askWithdraw(){
      Console.WriteLine("How much would you like to withdraw?");
      string selectedOption = Console.ReadLine();
      try{
        int toWithdraw = int.Parse(selectedOption);
        withdraw(toWithdraw);
        Console.WriteLine("You withdrew: $"+toWithdraw);
        addToHistory(toWithdraw, false);
      }catch(FormatException e){
        Console.WriteLine("Invalid value !");
        Console.WriteLine(e.Message);
      }
      Program.hitToMenu();
    }

    public void deposit(int value){
      balance += value;
    }

    public void withdraw(int value){
      balance -= value;
    }

    public void viewInformations(){
      Console.WriteLine("Account Holder: "+name);
      Program.hitToMenu();
    }

    public void viewBalance(){
      Console.WriteLine(type + " Account Balance: " + balance);
      Program.hitToMenu();
    }
  }

  class Checking:Account {
    public Checking(string newName){
      name = newName;
      type = "Checking";
      deposit(2000);
    }
  }

  class Savings:Account{
    public Savings(string newName){
      name = newName;
      type = "Savings";
      deposit(10000);
    }
  }

  class Program{
    public static void hitToMenu(){
      Console.WriteLine("Hit Enter to Display Banking Menu");
      Console.ReadKey();
    }

    static void Main(string[] args) {
      List<string> options = new List<string>(){
        "[I] View Account Holder Information",
        "[CB] Checking - View Balance",
        "[CD] Checking - Deposit Funds",
        "[CW] Checking - Withdraw Funds",
        "[SB] Savings - View Balance",
        "[SD] Savings - Deposit Funds",
        "[SW] Savings - Withdraw Funds",
        "[X] Exit"
      };

      Checking checkAccount = new Checking("Tom Bouteiller");
      Savings saveAccount = new Savings("Tom Bouteiller");
      string selectedOption;

      Program.hitToMenu();
      bool stillAsking = true;
      do{
        Console.WriteLine("Please select an option below:");
        for(int x = 0; x < options.Count; x++){
          Console.WriteLine(options[x]);
        }
        selectedOption = Console.ReadLine();

        switch(selectedOption){
          case "I": checkAccount.viewInformations();break;
          case "CB": checkAccount.viewBalance();break;
          case "CD": checkAccount.askDeposit();break;
          case "CW": checkAccount.askWithdraw();break;
          case "SB": saveAccount.viewBalance();break;
          case "SD": saveAccount.askDeposit();break;
          case "SW": saveAccount.askWithdraw();break;
          case "X": checkAccount.saveTransactions();
                    saveAccount.saveTransactions();
                    stillAsking = false; break;
        }
      }while(stillAsking);
      
      Console.WriteLine("Thanks and come again!");
    }
  }

}
