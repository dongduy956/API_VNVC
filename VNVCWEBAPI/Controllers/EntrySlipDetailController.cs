using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.Services;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrySlipDetailController : ControllerBase
    {
        private readonly IEntrySlipDetailsServices entrySlipDetailsServices;
        public EntrySlipDetailController(IEntrySlipDetailsServices entrySlipDetailsServices)
        {
            this.entrySlipDetailsServices = entrySlipDetailsServices;
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.EntrySlipDetail.View)]
        public IActionResult GetEntrySlipDetailsByEntrySlipIds(int[] ids)
        {

            var entrySlipDetails = entrySlipDetailsServices
                                       .GetEntrySlipDetailsByEntrySlipIds(ids)
                                       .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = entrySlipDetails,
            });
        }
        [HttpGet("[Action]/{entrySlipId}")]
        [Authorize(Roles = Permissions.EntrySlipDetail.View)]
        public IActionResult GetEntrySlipDetailsByEntrySlipId(int entrySlipId)
        {

            var entrySlipDetails = entrySlipDetailsServices.GetEntrySlipDetailsByEntrySlipId(entrySlipId)
                .OrderBy(x => x.Id);

            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = entrySlipDetails,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.EntrySlipDetail.Delete)]
        public async Task<IActionResult> DeleteEntrySlipDetail(int id)
        {
            var result = await entrySlipDetailsServices.DeleteEntrySlipDetailsAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chi tiết phiếu nhập thành công." },
                    StatusCode = Ok().StatusCode
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Xoá chi tiết phiếu nhập thất bại." }
            });
        }
        [HttpDelete("[Action]/{entrySlipId}")]
        [Authorize(Roles = Permissions.EntrySlipDetail.Delete)]
        public async Task<IActionResult> DeleteEntrySlipDetailsByEntrySlipId(int entrySlipId)
        {
            var result = await entrySlipDetailsServices.DeleteEntrySlipDetailsByEntrySlipId(entrySlipId);
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
        [Authorize(Roles = Permissions.EntrySlipDetail.Create)]
        public async Task<IActionResult> InsertEntrySlipDetail(EntrySlipDetailsModel entrySlipDetailsModel)
        {
            var result = await entrySlipDetailsServices.InsertEntrySlipDetailsAsync(entrySlipDetailsModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = entrySlipDetailsModel,
                    Messages = new string[] { "Thêm chi tiết phiếu nhập thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết phiếu nhập thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.EntrySlipDetail.Create)]
        public async Task<IActionResult> InsertEntrySlipDetailsRange(IList<EntrySlipDetailsModel> entrySlipDetailsModel)
        {
            var result = await entrySlipDetailsServices.InsertEntrySlipDetailsRangesAsync(entrySlipDetailsModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = entrySlipDetailsModel,
                    Messages = new string[] { "Thêm chi tiết phiếu nhập thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết phiếu nhập thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.EntrySlipDetail.Edit)]
        public async Task<IActionResult> UpdateEntrySlipDetail(int id, EntrySlipDetailsModel entrySlipDetailsModel)
        {
            var result = await entrySlipDetailsServices.UpdateEntrySlipDetailsAsync(id, entrySlipDetailsModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa chi tiết phiếu nhập thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa chi tiết phiếu nhập thất bại." }
            });
        }

    }
}
