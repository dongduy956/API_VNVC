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
    public class AdditionalCustomerInformationController : ControllerBase
    {
        private readonly IAdditionalCustomerInformationServices additionalCustomerInformationService;
        public AdditionalCustomerInformationController(IAdditionalCustomerInformationServices additionalCustomerInformationService)
        {
            this.additionalCustomerInformationService = additionalCustomerInformationService;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.View)]
        public IActionResult GetAllAdditionalCustomerInformations()
        {
            var additionalCustomers = additionalCustomerInformationService.GetAdditionalCustomerInformations()
                                       .OrderBy(x => x.CustomerId);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,              
                Data = additionalCustomers,
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.View)]
        public IActionResult GetAdditionalCustomerInformationByIds(int[] ids, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var additionalCustomers = additionalCustomerInformationService
                                       .GetAdditionalCustomerInformationByIds(ids)
                                       .OrderBy(x => x.CustomerId)
                                       .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = additionalCustomers.PageCount,
                PageNumber = additionalCustomers.PageNumber,
                TotalItems = additionalCustomers.TotalItemCount,
                Data = additionalCustomers,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.View)]
        public IActionResult GetAdditionalCustomerInformations(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var additionalCustomers = additionalCustomerInformationService.GetAdditionalCustomerInformations()
                .OrderBy(x => x.CustomerId)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = additionalCustomers.PageCount,
                PageNumber = additionalCustomers.PageNumber,
                TotalItems = additionalCustomers.TotalItemCount,
                Data = additionalCustomers,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.View)]
        public IActionResult SearchAdditionalCustomerInformations(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var additionalCustomers = additionalCustomerInformationService.SearchAdditionalCustomerInformations(q)
                .OrderBy(x => x.CustomerId)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = additionalCustomers.PageCount,
                PageNumber = additionalCustomers.PageNumber,
                TotalItems = additionalCustomers.TotalItemCount,
                Data = additionalCustomers,
            });
        }
        [HttpDelete("[Action]/{cusomerId}")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.Delete)]
        public async Task<IActionResult> DeleteAdditionalCustomerInformation(int cusomerId)
        {
            var result = await additionalCustomerInformationService
                               .DeleteAdditionalCustomerInformationAsync(cusomerId);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    StatusCode = Ok().StatusCode
                });

            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.Create)]
        public async Task<IActionResult> InsertAdditionalCustomerInformation(AdditionalCustomerInformationModel additionalCustomerInformationModel)
        {
            var result = await additionalCustomerInformationService
                .InsertAdditionalCustomerInformationAsync(additionalCustomerInformationModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = additionalCustomerInformationModel,
                    Messages = new string[] { "Thêm thông tin khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm thông tin khách hàng thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.Create)]
        public async Task<IActionResult> InsertAdditionalCustomerInformationsRange(IList<AdditionalCustomerInformationModel> additionalCustomerInformationModels)
        {
            var result = await additionalCustomerInformationService
                .InsertAdditionalCustomerInformationsRange(additionalCustomerInformationModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = additionalCustomerInformationModels,
                    Messages = new string[] { "Thêm thông tin khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm thông tin khách hàng thất bại." }
            });
        }
        [HttpPut("[Action]/{customerId}")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.Edit)]
        public async Task<IActionResult> UpdateAdditionalCustomerInformation(int customerId, AdditionalCustomerInformationModel additionalCustomerInformationModel)
        {
            var result = await additionalCustomerInformationService
                .UpdateAdditionalCustomerInformationAsync(customerId, additionalCustomerInformationModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa thông tin khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa thông tin khách hàng thất bại." }
            });
        }
        [HttpPut("[Action]")]
        [Authorize(Roles = Permissions.AdditionalCustomerInformation.Edit)]
        public async Task<IActionResult> UpdateAdditionalCustomerInformationsRange(IList<AdditionalCustomerInformationModel> additionalCustomerInformationModels)
        {
            var result = await additionalCustomerInformationService
                .UpdateAdditionalCustomerInformationsRange( additionalCustomerInformationModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa thông tin khách hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa thông tin khách hàng thất bại." }
            });
        }
    }
}
