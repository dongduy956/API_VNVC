using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
    public class CustomerRankController : ControllerBase
    {
        private readonly ICustomerRankServices customerRankServices;
        public CustomerRankController(ICustomerRankServices customerRankServices)
        {
            this.customerRankServices = customerRankServices;
        }
        [Authorize(Roles = Permissions.CustomerRank.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetCustomerRank(int id)
        {
            var customerRank = await customerRankServices.GetCustomerRank(id);
            if (customerRank != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerRank,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [Authorize(Roles = Permissions.CustomerRank.View)]
        [HttpGet("[Action]")]
        public IActionResult GetAllCustomerRanks()
        {
            var customerRanks = customerRankServices.GetCustomerRanks()
               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = customerRanks,
            });
        }
        [Authorize(Roles = Permissions.CustomerRank.View)]
        [HttpGet("[Action]/{customerId}")]
        public IActionResult GetCustomerRankByCustomerId(int customerId)
        {
            var customerRank = customerRankServices.GetCustomerRankByCustomerId(customerId);
            if (customerRank != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerRank,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [Authorize(Roles = Permissions.CustomerRank.View)]
        [HttpGet("[Action]")]
        public IActionResult GetCustomerRanks(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customerRanks = customerRankServices.GetCustomerRanks()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customerRanks.PageCount,
                PageNumber = customerRanks.PageNumber,
                TotalItems = customerRanks.TotalItemCount,
                Data = customerRanks,
            });
        }
        [Authorize(Roles = Permissions.CustomerRank.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchCustomerRanks(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customerRanks = customerRankServices.SearchCustomerRanks(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customerRanks.PageCount,
                PageNumber = customerRanks.PageNumber,
                TotalItems = customerRanks.TotalItemCount,
                Data = customerRanks,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.CustomerRank.Delete)]
        public async Task<IActionResult> DeleteCustomerRank(int id)
        {
            var result = await customerRankServices.DeleteCustomerRankAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá xếp loại khách hàng thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá xếp loại khách hàng." }
            });
        }
        [Authorize(Roles = Permissions.CustomerRank.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertCustomerRank(CustomerRankModel customerRankModel)
        {
            var result = await customerRankServices.InsertCustomerRankAsync(customerRankModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerRankModel,
                    Messages = new string[] { "Thêm xếp loại khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm xếp loại khách hàng thất bại." }
            });
        }
        [Authorize(Roles = Permissions.CustomerRank.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertCustomerRanksRange(IList<CustomerRankModel> customerRankModels)
        {
            var result = await customerRankServices.InsertCustomerRanksRange(customerRankModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerRankModels,
                    Messages = new string[] { "Thêm xếp loại khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm xếp loại khách hàng thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.CustomerRank.Edit)]
        public async Task<IActionResult> UpdateCustomerRank(int id, CustomerRankModel customerRankModel)
        {
            var result = await customerRankServices.UpdateCustomerRankAsync(id, customerRankModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa xếp loại khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa xếp loại khách hàng thất bại." }
            });
        }
    }
}
