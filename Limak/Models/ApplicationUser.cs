using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SeriaNumber { get; set; }

        public string CitizenShip { get; set; }

        public string DayOfBirth { get; set; }

        public string MonthOfBirth { get; set; }

        public string YearOfBirth { get; set; }

        public string Gender { get; set; }

        public string FinCode { get; set; }

        public string IdAddress { get; set; }
        public string CustomerId { get; set; }
        public string DeliveryOffice { get; set; }
        public bool IsActive { get; set; }
        public List<Declaration> Declarations { get; set; }
        public List<Order> Orders { get; set; }
        public Balance Balance { get; set; }
        public List<Operation>  Operations { get; set; }
        
    }
}
