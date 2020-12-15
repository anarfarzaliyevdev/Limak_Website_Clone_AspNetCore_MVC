using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerDetailsViewModel
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
        public string Email { get; set; }
    }
}
