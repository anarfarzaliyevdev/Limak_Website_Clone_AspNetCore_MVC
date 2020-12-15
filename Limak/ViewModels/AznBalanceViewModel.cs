using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.ViewModels
{
    public class AznBalanceViewModel
    {
        public Balance Balance { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
