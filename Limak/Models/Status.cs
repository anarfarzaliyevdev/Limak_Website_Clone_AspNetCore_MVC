using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public abstract class Status
    {
        public int Id { get; set; }
        public bool Ordered { get; set; }
        public bool AbroadWarehouse { get; set; }
        public bool OnWay { get; set; }
        public bool CustomsControl { get; set; }
        public bool BakuWarehouse { get; set; }
        public bool Courier { get; set; }
        public bool Return { get; set; }
        public bool Completed { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime AbroadWarehouseDate { get; set; }
        public DateTime OnWayDate { get; set; }
        public DateTime CustomsControlDate { get; set; }
        public DateTime BakuWarehouseDate { get; set; }
        public DateTime CourierDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
