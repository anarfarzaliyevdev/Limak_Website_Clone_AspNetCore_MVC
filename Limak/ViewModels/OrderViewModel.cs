using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.ViewModels
{
    public class OrderViewModel
    {

        public string ProductLink { get; set; }
        public decimal Price { get; set; }
        public int OrderCount { get; set; }
        public string OrderNote { get; set; }
        public decimal TurkeyCargoPrice { get; set; }
     
    }
}
