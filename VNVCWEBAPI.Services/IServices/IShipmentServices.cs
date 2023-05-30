using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IShipmentServices
    {
        Task<IList<ShipmentModel>> GetShipmentsByIds(int[] ids);

        IQueryable<ShipmentModel> GetShipments();
        IQueryable<ShipmentModel> GetShipmentsBySupplierId(int supplierID);
        IQueryable<ShipmentModel> GetShipmentsByVaccineId(int vaccineId);
        IQueryable<ShipmentModel> SearchShipments(string q="");
        Task<ShipmentModel?> GetShipment(int id);
        Task<bool> InsertShipment(ShipmentModel shipmentModel);
        Task<bool> InsertShipmentsRange(IList<ShipmentModel> shipmentModels);

        Task<bool> UpdateShipment(int id, ShipmentModel shipmentModel);
        Task<bool> DeleteShipment(int id);
    }
}
