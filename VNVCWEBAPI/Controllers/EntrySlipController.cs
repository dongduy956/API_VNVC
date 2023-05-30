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
    public class EntrySlipController : ControllerBase
    {
        private readonly IEntrySlipServices entrySlipServices;
        public EntrySlipController(IEntrySlipServices entrySlipServices)
        {
            this.entrySlipServices = entrySlipServices;
        }
        [HttpGet("[Action]/{orderId}")]
        [Authorize(Roles = Permissions.EntrySlip.View)]
        public IActionResult GetEntrySlipsByOrderId(int orderId)
        {
            var entrySlips = entrySlipServices.GetEntrySlipsByOrderId(orderId)
               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = entrySlips,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.EntrySlip.View)]
        public IActionResult GetEntrySlips(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var entrySlips = entrySlipServices.GetEntrySlips()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = entrySlips.PageCount,
                PageNumber = entrySlips.PageNumber,
                TotalItems = entrySlips.TotalItemCount,
                Data = entrySlips,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.EntrySlip.View)]
        public IActionResult SearchEntrySlips(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var entrySlips = entrySlipServices.SearchEntrySlips(q)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = entrySlips.PageCount,
                PageNumber = entrySlips.PageNumber,
                TotalItems = entrySlips.TotalItemCount,
                Data = entrySlips,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.EntrySlip.Delete)]
        public async Task<IActionResult> DeleteEntrySlip(int id)
        {
            var result = await entrySlipServices.DeleteEntrySlipAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá phiếu nhập thành công." },
                    StatusCode = Ok().StatusCode
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá phiếu nhập." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.EntrySlip.Create)]
        public async Task<IActionResult> InsertEntrySlip(EntrySlipModel entrySlipModel)
        {
            var result = await entrySlipServices.InsertEntrySlipAsync(entrySlipModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = entrySlipModel,
                    Messages = new string[] { "Thêm phiếu nhập thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm phiếu nhập thất bại." }
            });
        }
    }
}
