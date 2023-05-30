using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices supplierServices;
        public SupplierController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Supplier.View)]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = supplierServices.GetSuppliers()
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = suppliers,
            });
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Supplier.View)]
        public IActionResult GetSuppliers(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var suppliers = supplierServices.GetSuppliers()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = suppliers.PageCount,
                PageNumber = suppliers.PageNumber,
                TotalItems = suppliers.TotalItemCount,
                Data = suppliers,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Supplier.View)]
        public IActionResult SearchSuppliers(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var suppliers = supplierServices.SearchSuppliers(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = suppliers.PageCount,
                PageNumber = suppliers.PageNumber,
                TotalItems = suppliers.TotalItemCount,
                Data = suppliers,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Supplier.Delete)]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await supplierServices.DeleteSupplier(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá nhà cung cấp thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá nhà cung cấp." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Supplier.Create)]
        public async Task<IActionResult> InsertSupplier(SupplierModel supplierModel)
        {
            var result = await supplierServices.InsertSupplier(supplierModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = supplierModel,
                    Messages = new string[] { "Thêm nhà cung cấp thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = result,
                Messages = new string[] { "Thêm nhà cung cấp thất bại." }
            });

        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Supplier.Create)]
        public async Task<IActionResult> InsertSuppliersRange(IList<SupplierModel> supplierModels)
        {
            var result = await supplierServices.InsertSuppliersRange(supplierModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = supplierModels,
                    Messages = new string[] { "Thêm nhà cung cấp thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm nhà cung cấp thất bại." }
            });

        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Supplier.Edit)]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierModel supplierModel)
        {
            var result = await supplierServices.UpdateSupplier(id, supplierModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa nhà cung cấp thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = result,
                Messages = new string[] { "Sửa nhà cung cấp thất bại." }
            });
        }

        [HttpGet("[Action]/{id}")]
        [Authorize(Roles = Permissions.Supplier.View)]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var data = await supplierServices.GetSupplier(id);
            if(data!=null)
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = data
            });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = data
            });
        }
    }
}
