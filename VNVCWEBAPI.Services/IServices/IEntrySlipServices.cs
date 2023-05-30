using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IEntrySlipServices
    {
        IQueryable<EntrySlipModel> GetEntrySlips();
        IQueryable<EntrySlipModel> GetEntrySlipsByOrderId(int orderId);
        IQueryable<EntrySlipModel> SearchEntrySlips(string q="");
        Task<EntrySlipModel> GetEntrySlip(int id);
        Task<bool> InsertEntrySlipAsync(EntrySlipModel model);
        Task<bool> DeleteEntrySlipAsync(int id);


    }
}
