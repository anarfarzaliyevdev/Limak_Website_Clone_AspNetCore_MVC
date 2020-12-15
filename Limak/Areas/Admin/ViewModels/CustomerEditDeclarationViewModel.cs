using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerEditDeclarationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string PackageProductType { get; set; }
        [Required]
        public int PackageProductNumber { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string TrackId { get; set; }
        [Required]
        public string DeliveryOffice { get; set; }
        [Required]
        public string DayOfOrder { get; set; }
        [Required]
        public string MonthOfOrder { get; set; }
        [Required]
        public string YearOfOrder { get; set; }
 
        public string PackageNote { get; set; }
        [Required]
        public decimal Weight { get; set; }
         public string UserId { get; set; }
        public string Status { get; set; }
        public string OrderNo { get; set; }
        public decimal CargoPrice { get; set; }
        public bool IsCargoPaid { get; set; }
    }
}
