using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplication.Models
{
    public class miniLoanApp
    {
        private List<Loan> loans = new List<Loan>();

        public void CreateLoan(int userId, decimal amount, int term, DateTime requestDate)
        {
            Loan newLoan = new Loan(userId, amount, term, requestDate);
            loans.Add(newLoan);
            Console.WriteLine("Loan created successfully.");
        }

        public void ApproveLoan(int loanId)
        {
            Loan loanToApprove = loans.Find(loan => loan.LoanId == loanId && loan.Status == LoanStatus.PENDING);

            if (loanToApprove != null)
            {
                loanToApprove.Status = LoanStatus.APPROVED;
                Console.WriteLine("Loan approved successfully.");
            }
            else
            {
                Console.WriteLine("Loan not found or already approved.");
            }
        }

        public void ViewUserLoans(int userId)
        {
            List<Loan> userLoans = loans.FindAll(loan => loan.UserId == userId);
            Console.WriteLine($"User {userId}'s Loans:");

            foreach (Loan loan in userLoans)
            {
                Console.WriteLine($"Loan ID: {loan.LoanId}, Amount: {loan.Amount}, Status: {loan.Status}");
            }
        }

        public void AddRepayment(int loanId, decimal repaymentAmount)
        {
            Loan loanToUpdate = loans.Find(loan => loan.LoanId == loanId && loan.Status == LoanStatus.APPROVED);

            if (loanToUpdate != null)
            {
                Repayment nextDueRepayment = loanToUpdate.ScheduledRepayments.Find(repayment => repayment.Status == RepaymentStatus.PENDING);
                if (nextDueRepayment != null && repaymentAmount >= nextDueRepayment.Amount)
                {
                    nextDueRepayment.Status = RepaymentStatus.PAID;

                    if (loanToUpdate.ScheduledRepayments.All(repayment => repayment.Status == RepaymentStatus.PAID))
                    {
                        loanToUpdate.Status = LoanStatus.PAID;
                    }

                    Console.WriteLine("Repayment added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid repayment amount or no pending repayments.");
                }
            }
            else
            {
                Console.WriteLine("Loan not found or not approved.");
            }
        }
    }
}
