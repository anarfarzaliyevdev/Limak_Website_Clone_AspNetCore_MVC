using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerEditViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SeriaNumber { get; set; }
        [Required]

        public string CitizenShip { get; set; }
        [Required]

        public string DayOfBirth { get; set; }
        [Required]

        public string MonthOfBirth { get; set; }
        [Required]

        public string YearOfBirth { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public string FinCode { get; set; }
        [Required]

        public string IdAddress { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string DeliveryOffice { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
