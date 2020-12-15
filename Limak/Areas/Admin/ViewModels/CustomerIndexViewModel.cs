using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerIndexViewModel
    {
        public List<ApplicationUser> Customers { get; set; }
    }
}
