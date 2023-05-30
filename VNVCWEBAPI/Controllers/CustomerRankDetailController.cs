using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class CustomerRankDetailController : ControllerBase
    {
        private readonly ICustomerRankDetailsServices customerRankDetailServices;
        public CustomerRankDetailController(ICustomerRankDetailsServices customerRankDetailServices)
        {
            this.customerRankDetailServices = customerRankDetailServices;
        }

        [Authorize(Roles = Permissions.CustomerRankDetail.View)]

        [HttpGet("[Action]/{payId}")]
        public IActionResult GetCustomerRankDetailByPayId(int payId)
        {
            var customerRankDetail = customerRankDetailServices
                                     .GetCustomerRankDetailByPayId(payId);
            if (customerRankDetail == null)
                return BadRequest(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = customerRankDetail,
            });
        }
        [Authorize(Roles = Permissions.CustomerRankDetail.Edit)]

        [HttpPut("[Action]")]
        public async Task<IActionResult> UpdateCustomerRankDetail(int id, int point)
        {
            var customerRankDetail = await customerRankDetailServices
                                           .UpdateCustomerRankDetail(id, point);
            if (customerRankDetail)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerRankDetail,
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.CustomerRankDetail.View)]
        public IActionResult GetCustomerRankDetails(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customerRankDetails = customerRankDetailServices.GetCustomerRankDetails()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customerRankDetails.PageCount,
                PageNumber = customerRankDetails.PageNumber,
                TotalItems = customerRankDetails.TotalItemCount,
                Data = customerRankDetails,
            });
        }
        [Authorize(Roles = Permissions.CustomerRankDetail.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchCustomerRankDetails(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var customerRankDetails = customerRankDetailServices.SearchCustomerRankDetails(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = customerRankDetails.PageCount,
                PageNumber = customerRankDetails.PageNumber,
                TotalItems = customerRankDetails.TotalItemCount,
                Data = customerRankDetails,
            });
        }
        [Authorize(Roles = Permissions.CustomerRankDetail.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertCustomerRankDetail(CustomerRankDetailsModel customerRankDetailModel)
        {
            var result = await customerRankDetailServices.InsertCustomerRankDetailsAsync(customerRankDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = customerRankDetailModel,
                    Messages = new string[] { "Thêm chi tiết xếp loại khách hàng thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết xếp loại khách hàng thất bại." }
            });
        }
    }
}
