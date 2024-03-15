using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplication.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public DateTime RequestDate { get; set; }
        public List<Repayment> ScheduledRepayments { get; set; }
        public LoanStatus Status { get; set; }

        public Loan(int userId, decimal amount, int term, DateTime requestDate)
        {
            LoanId = GenerateLoanId();
            UserId = userId;
            Amount = amount;
            Term = term;
            RequestDate = requestDate;
            Status = LoanStatus.PENDING;

            GenerateScheduledRepayments();
        }

        private void GenerateScheduledRepayments()
        {
            ScheduledRepayments = new List<Repayment>();

            decimal weeklyRepayment = Math.Ceiling(Amount / Term);
            for (int i = 0; i < Term; i++)
            {
                DateTime dueDate = RequestDate.AddDays((i + 1) * 7);
                ScheduledRepayments.Add(new Repayment(LoanId, weeklyRepayment, dueDate));
            }
        }
        private int GenerateLoanId()
        {
            // Implement logic to generate a unique loan id
            return new Random().Next(1000, 9999);
        }
    }

    public class Repayment
    {
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public RepaymentStatus Status { get; set; }

        public Repayment(int loanId, decimal amount, DateTime dueDate)
        {
            LoanId = loanId;
            Amount = amount;
            DueDate = dueDate;
            Status = RepaymentStatus.PENDING;
        }
    }

    public enum LoanStatus
    {
        PENDING,
        APPROVED,
        PAID
    }
    public enum RepaymentStatus
    {
        PENDING,
        PAID
    }
}
}
