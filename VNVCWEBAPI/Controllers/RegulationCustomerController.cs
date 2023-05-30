using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.Services;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegulationCustomerController : ControllerBase
    {
        private readonly IRegulationCustomerServices regulationCustomerServices;
        public RegulationCustomerController(IRegulationCustomerServices regulationCustomerServices)
        {
            this.regulationCustomerServices = regulationCustomerServices;
        }
        [Authorize(Roles = Permissions.RegulationCustomer.View)]
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetRegulationCustomerByCustomerTypeIdAndVaccineId(int customerTypeId, int vaccineId)
        {
            var regulationCustomer = await regulationCustomerServices.GetRegulationCustomerByCustomerTypeIdAndVaccineId(customerTypeId, vaccineId);
            if (regulationCustomer == null)
                return Ok(new ResponseAPI
                {
                    isSuccess = false,
                    StatusCode = BadRequest().StatusCode
                });
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = regulationCustomer,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.RegulationCustomer.View)]
        public IActionResult GetAllRegulationCustomers()
        {
            var regulationCustomers = regulationCustomerServices.GetRegulationCustomers()
               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = regulationCustomers,
            });
        }
        [Authorize(Roles = Permissions.RegulationCustomer.View)]
        [HttpGet("[Action]")]
        public IActionResult GetRegulationCustomers(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var regulationCustomers = regulationCustomerServices.GetRegulationCustomers()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = regulationCustomers.PageCount,
                PageNumber = regulationCustomers.PageNumber,
                TotalItems = regulationCustomers.TotalItemCount,
                Data = regulationCustomers,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.RegulationCustomer.View)]
        public IActionResult SearchRegulationCustomers(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var regulationCustomers = regulationCustomerServices.SearchRegulationCustomers(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = regulationCustomers.PageCount,
                PageNumber = regulationCustomers.PageNumber,
                TotalItems = regulationCustomers.TotalItemCount,
                Data = regulationCustomers,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.RegulationCustomer.Delete)]
        public async Task<IActionResult> DeleteRegulationCustomer(int id)
        {
            var result = await regulationCustomerServices.DeleteRegulationCustomer(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá loại quy định khách hàng thành công." },
                    StatusCode = Ok().StatusCode
                });

            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá quy định khách hàng." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.RegulationCustomer.Create)]
        public async Task<IActionResult> InsertRegulationCustomer(RegulationCustomerModel regulationCustomerModel)
        {
            var result = await regulationCustomerServices.InsertRegulationCustomer(regulationCustomerModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = regulationCustomerModel,
                    Messages = new string[] { "Thêm quy định khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm quy định khách hàng thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.RegulationCustomer.Create)]
        public async Task<IActionResult> InsertRegulationCustomersRange(IList<RegulationCustomerModel> regulationCustomerModels)
        {
            var result = await regulationCustomerServices.InsertRegulationCustomersRange(regulationCustomerModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = regulationCustomerModels,
                    Messages = new string[] { "Thêm quy định khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm quy định khách hàng thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.RegulationCustomer.Edit)]
        public async Task<IActionResult> UpdateRegulationCustomer(int id, RegulationCustomerModel regulationCustomerModel)
        {
            var result = await regulationCustomerServices.UpdateRegulationCustomer(id, regulationCustomerModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa quy định khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa quy định khách hàng thất bại." }
            });
        }
    }
}
