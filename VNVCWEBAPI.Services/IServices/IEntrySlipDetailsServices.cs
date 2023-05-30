using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IEntrySlipDetailsServices
    {
        IQueryable<EntrySlipDetailsModel> GetEntrySlipDetails();
        IList<EntrySlipDetailsModel> GetEntrySlipDetailsByEntrySlipIds(int[] entrySlipIds);
        IQueryable<EntrySlipDetailsModel> GetEntrySlipDetailsByEntrySlipId(int entrySlipId);
        Task<EntrySlipDetailsModel> GetEntrySlipDetailsAsync(int id);
        EntrySlipDetailsModel? GetEntrySlipDetailByShipmentId(int shipmentId);
        Task<bool> InsertEntrySlipDetailsAsync(EntrySlipDetailsModel model);
        Task<bool> InsertEntrySlipDetailsRangesAsync(IList<EntrySlipDetailsModel> models);
        Task<bool> UpdateEntrySlipDetailsAsync(int id,EntrySlipDetailsModel model);
        Task<bool> DeleteEntrySlipDetailsAsync(int id);
        Task<bool> DeleteEntrySlipDetailsRangeAsync(int[] ids);
        Task<bool> DeleteEntrySlipDetailsByEntrySlipId(int entrySlipId);
    }
}
