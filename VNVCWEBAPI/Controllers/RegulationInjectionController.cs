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
    public class RegulationInjectionController : ControllerBase
    {
        private readonly IRegulationInjectionServices regulationInjectionServices;
        public RegulationInjectionController(IRegulationInjectionServices regulationInjectionServices)
        {
            this.regulationInjectionServices = regulationInjectionServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.RegulationInjection.View)]
        public IActionResult GetAllRegulationInjections()
        {
            var regulationInjections = regulationInjectionServices.GetRegulationInjections()
               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = regulationInjections,
            });
        }
        [Authorize(Roles = Permissions.RegulationInjection.View)]
        [HttpGet("[Action]")]
        public IActionResult GetRegulationInjectionByVaccineId(int vaccineId)
        {

            var regulationInjection = regulationInjectionServices.GetRegulationInjectionByVaccineId(vaccineId);
            if (regulationInjection == null)
                return Ok(new ResponseAPI
                {
                    isSuccess = false,
                    StatusCode = BadRequest().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = regulationInjection,
            });
        }
        [Authorize(Roles = Permissions.RegulationInjection.View)]
        [HttpGet("[Action]")]
        public IActionResult GetRegulationInjections(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var regulationInjections = regulationInjectionServices.GetRegulationInjections()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = regulationInjections.PageCount,
                PageNumber = regulationInjections.PageNumber,
                TotalItems = regulationInjections.TotalItemCount,
                Data = regulationInjections,
            });
        }
        [Authorize(Roles = Permissions.RegulationInjection.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchRegulationInjections(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var regulationInjections = regulationInjectionServices.SearchRegulationInjections(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = regulationInjections.PageCount,
                PageNumber = regulationInjections.PageNumber,
                TotalItems = regulationInjections.TotalItemCount,
                Data = regulationInjections,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.RegulationInjection.Delete)]
        public async Task<IActionResult> DeleteRegulationInjection(int id)
        {
            var result = await regulationInjectionServices.DeleteRegulationInjection(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá loại quy định tiêm thành công." },
                    StatusCode = Ok().StatusCode
                });

            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá quy định tiêm." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.RegulationInjection.Create)]
        public async Task<IActionResult> InsertRegulationInjection(RegulationInjectionModel regulationInjectionModel)
        {
            var result = await regulationInjectionServices.InsertRegulationInjection(regulationInjectionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = regulationInjectionModel,
                    Messages = new string[] { "Thêm quy định tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm quy định tiêm thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.RegulationInjection.Create)]
        public async Task<IActionResult> InsertRegulationInjectionsRange(IList<RegulationInjectionModel> regulationInjectionModels)
        {
            var result = await regulationInjectionServices.InsertRegulationInjectionsRange(regulationInjectionModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = regulationInjectionModels,
                    Messages = new string[] { "Thêm quy định tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm quy định tiêm thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.RegulationInjection.Edit)]
        public async Task<IActionResult> UpdateRegulationInjection(int id, RegulationInjectionModel regulationInjectionModel)
        {
            var result = await regulationInjectionServices.UpdateRegulationInjection(id, regulationInjectionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa quy định tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa quy định tiêm thất bại." }
            });
        }
    }
}
