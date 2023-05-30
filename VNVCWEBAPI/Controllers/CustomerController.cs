using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerServices customerService;
        public CustomerController(ICustomerServices customerService)
        {
            this.customerService = customerService;
        }
        [Authorize(Roles = Permissions.Customer.View)]
        [HttpGet("[Action]")]
        public IActionResult GetAllCustomers()
        {
            var customers = customerService.GetCustomers()
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = customers,
            });
        }
        [Authorize(Roles = Permissions.Customer.View)]
        [HttpGet("[Action]")]
        public IActionResult GetCustomersEligible()
        {
            var customers = customerService.GetCustomersEligible()
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = customers,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Customer.View)]
        public IActionResult GetCustomers(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customers = customerService.GetCustomers()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customers.PageCount,
                PageNumber = customers.PageNumber,
                TotalItems = customers.TotalItemCount,
                Data = customers,
            });
        }
        [Authorize(Roles = Permissions.Customer.View)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetCustomerByIds(int[] ids, int? page, int? pageSize)
        {

            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customers = (await customerService.GetCustomerByIds(ids))
                            .OrderBy(x => x.Id).OrderBy(x => x.Id)
                            .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customers.PageCount,
                PageNumber = customers.PageNumber,
                TotalItems = customers.TotalItemCount,
                Data = customers,
            });
        }
        [Authorize(Roles = Permissions.Customer.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await customerService.GetCustomer(id);
            if (customer != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customer,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetCustomerByLoginId(int id)
        {
            if (!isAccess(id, Permissions.Customer.View))
            {
                return Unauthorized();
            }
            var customer = await customerService.GetCustomerByLoginId(id);
            if (customer != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customer,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Customer.View)]
        public IActionResult SearchCustomers(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customers = customerService.SearchCustomers(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customers.PageCount,
                PageNumber = customers.PageNumber,
                TotalItems = customers.TotalItemCount,
                Data = customers,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Customer.Delete)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await customerService.DeleteCustomerAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá khách hàng thành công." },
                    StatusCode = Ok().StatusCode
                });

            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá khách hàng." }
            });
        }

        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Customer.Create)]
        public async Task<IActionResult> InsertCustomer(CustomerModel customerModel)
        {
            var result = await customerService.InsertCustomerAsync(customerModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerModel,
                    Messages = new string[] { "Thêm khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm khách hàng thất bại." }
            });
        }

        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Customer.Create)]
        public async Task<IActionResult> InsertCustomersRange(IList<CustomerModel> customerModels)
        {
            var result = await customerService.InsertCustomersRange(customerModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerModels,
                    Messages = new string[] { "Thêm khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm khách hàng thất bại." }
            });
        }
        [Authorize(Roles = Permissions.Customer.Edit)]
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerModel customerModel)
        {
            if (!this.isAccess(id, Permissions.Customer.Edit))
                return Unauthorized();

            var result = await customerService.UpdateCustomerAsync(id, customerModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa khách hàng thất bại." }
            });
        }
    }
}
