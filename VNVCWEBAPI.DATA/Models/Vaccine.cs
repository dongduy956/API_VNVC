using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Vaccine : BaseEntity
    {
        public Vaccine()
        {
            VaccinePrices = new HashSet<VaccinePrice>();
            InjectionScheduleDetails = new HashSet<InjectionScheduleDetail>();
            InjectionIncidents = new HashSet<InjectionIncident>();
            PayDetails = new HashSet<PayDetail>();
            RegulationInjections = new HashSet<RegulationInjection>();
            VaccinePackageDetails = new HashSet<VaccinePackageDetails>();
            OrderDetails = new HashSet<OrderDetails>();
            EntrySlipDetails = new HashSet<EntrySlipDetails>();
            Shipments=new HashSet<Shipment>();
            Carts=new HashSet<Cart>();
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public string DiseasePrevention { get; set; }
        public string InjectionSite { get; set; }
        public string SideEffects { get; set; }
        public int Amount { get; set; }
        public string Storage { get; set; }
        public string StorageTemperatures { get; set; }
        public string? Content { get; set; }
        public int TypeOfVaccineId { get; set; }
        [ForeignKey("TypeOfVaccineId")]
        public TypeOfVaccine TypeOfVaccine { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
        public ICollection<InjectionScheduleDetail> InjectionScheduleDetails { get; set; }
        public ICollection<VaccinePrice> VaccinePrices { get; set; }
        public ICollection<InjectionIncident> InjectionIncidents { get; set; }
        public ICollection<PayDetail> PayDetails { get; set; }
        public ICollection<RegulationInjection> RegulationInjections { get; set; }
        public ICollection<VaccinePackageDetails> VaccinePackageDetails { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<EntrySlipDetails> EntrySlipDetails { get; set; }
        public ICollection<Cart> Carts { get; set; }

    }
}
