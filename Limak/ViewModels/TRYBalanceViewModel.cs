using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.ViewModels
{
    public class TRYBalanceViewModel
    {
        public decimal TRYBalance { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
