using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.ViewModels
{
    public class PackageUpdateViewModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string PackageProductType { get; set; }
        public int PackageProductNumber { get; set; }
        public decimal Price { get; set; }
        public string TrackId { get; set; }
        public string DeliveryOffice { get; set; }
        public string PackageNote { get; set; }
    }
}
