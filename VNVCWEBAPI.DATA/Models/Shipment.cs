using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Shipment : BaseEntity
    {
        public Shipment()
        {
            EntrySlipDetails = new HashSet<EntrySlipDetails>();
            VaccinePrices = new HashSet<VaccinePrice>();
            OrderDetails = new HashSet<OrderDetails>();
            PayDetails = new HashSet<PayDetail>();
            InjectionScheduleDetails = new HashSet<InjectionScheduleDetail>();
            VaccinePackageDetails = new HashSet<VaccinePackageDetails>();
            InjectionIncidents = new HashSet<InjectionIncident>();
        }
        public string ShipmentCode { get; set; }
        public int SupplierId { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int VaccineId { get; set; }
        public string Country { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine Vaccine { get; set; }
        public ICollection<EntrySlipDetails> EntrySlipDetails { get; set; }
        public ICollection<VaccinePackageDetails> VaccinePackageDetails { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<PayDetail> PayDetails { get; set; }
        public ICollection<InjectionScheduleDetail> InjectionScheduleDetails { get; set; }
        public ICollection<VaccinePrice> VaccinePrices { get; set; }
        public ICollection<InjectionIncident> InjectionIncidents { get; set; }
    }
}
