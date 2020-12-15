using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Areas.Admin.ViewModels
{
    public class CustomerAddDeclarationViewModel
    {

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
     
        public string DeliveryOffice { get; set; }
   
        public string DayOfOrder { get; set; }
        public string MonthOfOrder { get; set; }
        public string YearOfOrder { get; set; }
        public string PackageNote { get; set; }
        public decimal Weight { get; set; }
        public string OrderNo { get; set; }
        public string UserId { get; set; }
    }
}
