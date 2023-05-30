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
    public class InjectionIncidentController : ControllerBase
    {
        private readonly IInjectionIncidentServices injectionIncidentServices;
        public InjectionIncidentController(IInjectionIncidentServices injectionIncidentServices)
        {
            this.injectionIncidentServices = injectionIncidentServices;
        }

        [Authorize(Roles = Permissions.InjectionIncident.View)]
        [HttpGet("[Action]")]
        public IActionResult GetInjectionIncidents(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var injectionIncidents= injectionIncidentServices.GetInjectionIncidents()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = injectionIncidents.PageCount,
                PageNumber = injectionIncidents.PageNumber,
                TotalItems = injectionIncidents.TotalItemCount,
                Data = injectionIncidents,
            });
        }
        [Authorize(Roles = Permissions.InjectionIncident.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchInjectionIncidents(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var injectionIncidents = injectionIncidentServices.SearchInjectionIncidents(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = injectionIncidents.PageCount,
                PageNumber = injectionIncidents.PageNumber,
                TotalItems = injectionIncidents.TotalItemCount,
                Data = injectionIncidents,
            });
        }

        [Authorize(Roles = Permissions.InjectionIncident.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertInjectionIncident(InjectionIncidentModel injectionIncidentModel)
        {
            var result = await injectionIncidentServices.InsertInjectionIncidentAsync(injectionIncidentModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = injectionIncidentModel,
                    Messages = new string[] { "Báo cáo sự cố tiêm thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Báo cáo sự cố tiêm thất bại." }
            });
        }      
    }
}
