using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeServices customerTypeService;
        public CustomerTypeController(ICustomerTypeServices customerTypeService)
        {
            this.customerTypeService = customerTypeService;
        }
        [Authorize(Roles = Permissions.CustomerType.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetCustomerType(int id)
        {
            var customerType =await customerTypeService.GetCustomerType(id);
            if (customerType != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerType,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }

        [Authorize(Roles = Permissions.CustomerType.View)]
        [HttpGet("[Action]")]
        public IActionResult GetCustomerTypes(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customerTypes = customerTypeService.GetCustomerTypes()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customerTypes.PageCount,
                PageNumber = customerTypes.PageNumber,
                TotalItems = customerTypes.TotalItemCount,
                Data = customerTypes,
            });
        }

        [Authorize(Roles = Permissions.CustomerType.View)]
        [HttpGet("[Action]")]
        public IActionResult GetAllCustomerTypes()
        {
            var customerTypes = customerTypeService.GetCustomerTypes()
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = customerTypes,
            });
        }

        [Authorize(Roles = Permissions.CustomerType.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchCustomerTypes(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customerTypes = customerTypeService.SearchCustomerTypes(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customerTypes.PageCount,
                PageNumber = customerTypes.PageNumber,
                TotalItems = customerTypes.TotalItemCount,
                Data = customerTypes,
            });
        }
       
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.CustomerType.Delete)]
        public async Task<IActionResult> DeleteCustomerType(int id)
        {
            var result = await customerTypeService.DeleteCustomerTypeAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá loại khách hàng thành công." },
                    StatusCode = Ok().StatusCode
                });

            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá loại khách hàng." }
            });
        }

        [Authorize(Roles = Permissions.CustomerType.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertCustomerType(CustomerTypeModel customerTypeModel)
        {
            var result = await customerTypeService.InsertCustomerTypeAsync(customerTypeModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerTypeModel,
                    Messages = new string[] { "Thêm loại khách hàng thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm loại khách hàng thất bại." }
            });
        }
        
        [Authorize(Roles = Permissions.CustomerType.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertCustomerTypesRange(IList<CustomerTypeModel> customerTypeModels)
        {
            var result = await customerTypeService.InsertCustomerTypesRange(customerTypeModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerTypeModels,
                    Messages = new string[] { "Thêm loại khách hàng thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm loại khách hàng thất bại." }
            });
        }
       
        [Authorize(Roles = Permissions.CustomerType.Edit)]
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateCustomerType(int id, CustomerTypeModel customerTypeModel)
        {
            var result = await customerTypeService.UpdateCustomerTypeAsync(id, customerTypeModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa loại khách hàng thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa loại khách hàng thất bại." }
            });
        }
    }
}
