using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerIncBalanceTRYViewModel
    {
        public string Id { get; set; }
        [Required]
        public decimal TRYAmount { get; set; }
    }
}
