using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
