using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ISupplierServices
    {
        IQueryable<SupplierModel> GetSuppliers();
        IQueryable<SupplierModel> SearchSuppliers(string q="");
        Task<SupplierModel> GetSupplier(int id);
        Task<bool> InsertSupplier(SupplierModel supplierModel);
        Task<bool> InsertSuppliersRange(IList<SupplierModel> supplierModels);

        Task<bool> UpdateSupplier(int id, SupplierModel supplierModel);
        Task<bool> DeleteSupplier(int id);
    }
}
