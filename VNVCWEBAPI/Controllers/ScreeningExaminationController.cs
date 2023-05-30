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
    public class ScreeningExaminationController : ControllerBase
    {
        private readonly IScreeningExaminationServices screeningExaminationServices;
        public ScreeningExaminationController(IScreeningExaminationServices screeningExaminationServices)
        {
            this.screeningExaminationServices = screeningExaminationServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.ScreeningExamination.View)]
        public IActionResult GetScreeningExaminations(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var screeningExaminations = screeningExaminationServices.GetScreeningExaminations()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = screeningExaminations.PageCount,
                PageNumber = screeningExaminations.PageNumber,
                TotalItems = screeningExaminations.TotalItemCount,
                Data = screeningExaminations,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.ScreeningExamination.View)]
        public IActionResult SearchScreeningExaminations(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var screeningExaminations = screeningExaminationServices.SearchScreeningExaminations(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = screeningExaminations.PageCount,
                PageNumber = screeningExaminations.PageNumber,
                TotalItems = screeningExaminations.TotalItemCount,
                Data = screeningExaminations,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.ScreeningExamination.Delete)]
        public async Task<IActionResult> DeleteScreeningExamination(int id)
        {
            var result = await screeningExaminationServices.DeleteScreeningExamination(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá phiếu khám thành công." },
                    StatusCode = Ok().StatusCode
                });

            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá phiếu khám." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.ScreeningExamination.Create)]
        public async Task<IActionResult> InsertScreeningExamination(ScreeningExaminationModel screeningExaminationModel)
        {
            var result = await screeningExaminationServices.InsertScreeningExamination(screeningExaminationModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = screeningExaminationModel,
                    Messages = new string[] { "Thêm phiếu khám thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm phiếu khám thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.ScreeningExamination.Edit)]
        public async Task<IActionResult> UpdateScreeningExamination(int id, ScreeningExaminationModel screeningExaminationModel)
        {
            var result = await screeningExaminationServices.UpdateScreeningExamination(id, screeningExaminationModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa phiếu khám thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa phiếu khám thất bại." }
            });
        }
    }
}
