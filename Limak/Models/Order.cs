using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductLink { get; set; }
        public decimal TurkeyCargoPrice { get; set; }
        public int OrderCount { get; set; }
        public string OrderNote { get; set; }
        public decimal Price { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
       
        public decimal OrderWeight { get; set; }
        public decimal OrderCargoPrice { get; set; }

    }
}
