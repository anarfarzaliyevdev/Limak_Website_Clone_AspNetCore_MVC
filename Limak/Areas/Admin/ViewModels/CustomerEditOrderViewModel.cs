using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerEditOrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProductLink { get; set; }
        public decimal TurkeyCargoPrice { get; set; }
        public int OrderCount { get; set; }
        public string OrderNote { get; set; }
        public decimal Price { get; set; }
        public decimal OrderWeight { get; set; }
        public string Status { get; set; }
        public decimal OrderCargoPrice { get; set; }
    }
}
