using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public decimal AZN { get; set; }
        public decimal TL { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
