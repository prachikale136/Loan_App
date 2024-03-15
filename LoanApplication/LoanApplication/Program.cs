using LoanApplication.Models;

namespace LoanApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);
            miniLoanApp loanApp = new miniLoanApp();

            // Example usage
            loanApp.CreateLoan(1, 10000, 3, new DateTime(2022, 2, 7));
            loanApp.ApproveLoan(1234);
            loanApp.ViewUserLoans(1);
            loanApp.AddRepayment(1234, 3333.33m);
            loanApp.ViewUserLoans(1);
            //Console.WriteLine("Hello, World!");
        }
    }
}
