using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
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
    [Authorize]
    public class InjectionScheduleDetailController : BaseController
    {
        private readonly IInjectionScheduleDetailServices injectionScheduleDetailServices;
        private readonly ICartServices cartServices;
        private readonly IShipmentServices shipmentServices;
        private readonly ILoginServices loginServices;
        public InjectionScheduleDetailController(
            IInjectionScheduleDetailServices injectionScheduleDetailServices,
            ICartServices cartServices,
            IShipmentServices shipmentServices,
            ILoginServices loginServices)
        {
            this.injectionScheduleDetailServices = injectionScheduleDetailServices;
            this.cartServices = cartServices;
            this.shipmentServices = shipmentServices;
            this.loginServices = loginServices;
        }
      //  [Authorize(Roles = Permissions.InjectionScheduleDetail.View)]
        [HttpGet("[Action]/{injectionScheduleId}")]
        public IActionResult GetInjectionScheduleDetails(int injectionScheduleId, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var injectionScheduleDetails = injectionScheduleDetailServices.GetInjectionScheduleDetails(injectionScheduleId)
                                    .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = injectionScheduleDetails,
            });
        }
        [HttpGet("[Action]/{customerId}")]
        
        public async Task<IActionResult> getHistoryInjectionDetails(int customerId, int? page, int? pageSize)
        {
            var StringUserId = User.Claims
               .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            var loginModel = await loginServices.GetLogin(int.Parse(StringUserId));
            if (!(loginModel.CustomerId == customerId || isAccess(Permissions.InjectionScheduleDetail.View)))
                return Forbid();
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var injectionScheduleDetails = injectionScheduleDetailServices.GetInjectionScheduleDetailsByCustomerId(customerId)
                .Where(x => x.Injection == true)
                                    .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = injectionScheduleDetails,
            });
        }
        [HttpGet("[Action]/{customerId}")]
        public async Task<IActionResult> getNextInjectionDetails(int customerId, int? page, int? pageSize)
        {
            var StringUserId = User.Claims
               .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            var loginModel = await loginServices.GetLogin(int.Parse(StringUserId));
            if (!(loginModel.CustomerId == customerId || isAccess(Permissions.InjectionScheduleDetail.View)))
                return Forbid();
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var injectionScheduleDetails = injectionScheduleDetailServices.GetInjectionScheduleDetailsByCustomerId(customerId)
                .Where(x => x.Injection == false && x.Pay == true)

                                    .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = injectionScheduleDetails,
            });
        }
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Delete)]
        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> DeleteInjectionScheduleDetail(int id)
        {
            var result = await injectionScheduleDetailServices.DeleteInjectionScheduleDetail(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chi tiết lịch tiêm thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá chi tiết lịch tiêm." }
            });
        }
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Delete)]
        [HttpDelete("[Action]/{injectionScheduleId}")]
        public async Task<IActionResult> DeleteInjectionScheduleDetailsByInjectionScheduleId(int injectionScheduleId)
        {
            var result = await injectionScheduleDetailServices.DeleteInjectionScheduleDetailsByInjectionScheduleId(injectionScheduleId);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chi tiết lịch tiêm thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá chi tiết lịch tiêm." }
            });
        }
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Create)]

        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertInjectionScheduleDetail(InjectionScheduleDetailModel injectionScheduleDetailModel)
        {
            
            var result = await injectionScheduleDetailServices.InsertInjectionScheduleDetail(injectionScheduleDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Thêm chi tiết lịch tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết lịch tiêm thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Create)]

        public async Task<IActionResult> InsertInjectionScheduleDetailsRange(int loginId, IList<InjectionScheduleDetailModel> injectionScheduleDetailModels)
        {
            if (!isAccess(loginId, Permissions.InjectionScheduleDetail.Create))
            {
                return Unauthorized();
            }
            
            var listExits = injectionScheduleDetailServices.GetInjectionScheduleDetails(injectionScheduleDetailModels.FirstOrDefault().InjectionScheduleId);
            if (listExits.Count() > 0)
            {
                return Ok(new ResponseAPI
                {
                    Data = HttpStatusCode.Found,
                    Messages = new[] { "Đã tồn tại, không thể thêm" }
                });
            }
            var result = await injectionScheduleDetailServices.InsertInjectionScheduleDetailsRange(injectionScheduleDetailModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = injectionScheduleDetailModels,
                    Messages = new string[] { "Thêm chi tiết lịch tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết lịch tiêm thất bại." }
            });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertInjectionScheduleDetailsFromCart(int loginId, InjectionScheduleDetailModel injectionScheduleDetailModels)
        {
            if (!isAccess(loginId, Permissions.InjectionScheduleDetail.Create))
            {
                return Unauthorized();
            }
            var listExits = injectionScheduleDetailServices.GetInjectionScheduleDetails(injectionScheduleDetailModels.InjectionScheduleId);
            if (listExits.Count() > 0)
            {
                return Ok(new ResponseAPI
                {
                    Data = HttpStatusCode.Found,
                    Messages = new[] { "Đã tồn tại, không thể thêm" }
                });
            }
            var listCart = cartServices.GetCarts().Where(x => x.LoginId == loginId);
            var listItems = new List<InjectionScheduleDetailModel>();
            foreach (var item in listCart)
            {
                var lastShippment = await shipmentServices.GetShipmentsByVaccineId(item.VaccineId.Value).FirstOrDefaultAsync();
                if (lastShippment == null)
                    continue;
                listItems.Add(new InjectionScheduleDetailModel
                {
                    
                    Address = injectionScheduleDetailModels.Address,
                    Amount = injectionScheduleDetailModels.Amount,
                    Created = DateTime.Now,
                    Injection = false,
                    Injections = injectionScheduleDetailModels.Injections,
                    InjectionScheduleId = injectionScheduleDetailModels.InjectionScheduleId,
                    Pay = false,
                    VaccineId = item.VaccineId,
                    VaccinePackageId = item.PackageId,
                    ShipmentId = lastShippment.Id
                });
            }
            var result = await injectionScheduleDetailServices.InsertInjectionScheduleDetailsRange(listItems);
            if (result)
            {
                await cartServices.DeleteCartRangeAsync(listCart.Select(x => x.Id).ToArray(), loginId);
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = injectionScheduleDetailModels,
                    Messages = new string[] { "Thêm chi tiết lịch tiêm thành công." }
                });
            }
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết lịch tiêm thất bại." }
            });
        }
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Edit)]
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateInjectionInjectionScheduleDetail(int id)
        {
            var result = await injectionScheduleDetailServices.UpdateInjectionInjectionScheduleDetail(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật thời gian tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật thời tiêm thất bại." }
            });
        }
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Edit)]
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateAddressInjectionStaffInjectionScheduleDetail(int id, string address, int injectionStaffId)
        {
            var result = await injectionScheduleDetailServices.UpdateAddressInjectionStaffInjectionScheduleDetail(id, address, injectionStaffId);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật chi tiết lịch tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật chi tiết lịch tiêm thất bại." }
            });
        }
        [Authorize(Roles = Permissions.InjectionScheduleDetail.Edit)]
        [HttpPut("[Action]")]
        public async Task<IActionResult> UpdatePayInjectionScheduleDetails(int[] ids)
        {
            var result = await injectionScheduleDetailServices.UpdatePayInjectionScheduleDetails(ids);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật chi tiết lịch tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật chi tiết lịch tiêm thất bại." }
            });
        }
    }
}
