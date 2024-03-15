using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplication.Models
{
    public class BaseModel
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string MUserId { get; set; }
        public DateTime? MDate { get; set; }
    }
}
