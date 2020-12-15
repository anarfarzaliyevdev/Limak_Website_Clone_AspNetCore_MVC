using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class Declaration
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string StoreName { get; set; }
        public string PackageProductType { get; set; }
        public int PackageProductNumber { get; set; }
        public decimal Price { get; set; }
        public string TrackId { get; set; }
        public string DeliveryOffice { get; set; }
        public string DayOfOrder { get; set; }
        public string MonthOfOrder { get; set; }
        public string YearOfOrder { get; set; }
        public string PackageNote { get; set; }
       
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; }
        public string OrderNo { get; set; }
        public decimal CargoPrice { get; set; }
        public bool IsCargoPaid { get; set; }

    }
}
