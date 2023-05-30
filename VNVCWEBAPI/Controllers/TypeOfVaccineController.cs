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
    public class TypeOfVaccineController : ControllerBase
    {
        private readonly ITypeOfVaccineServices typeOfVaccineServices;
        public TypeOfVaccineController(ITypeOfVaccineServices typeOfVaccineServices)
        {
            this.typeOfVaccineServices = typeOfVaccineServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.TypeOfVaccine.View)]
        public IActionResult GetTypeOfVaccines(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var typeOfVaccines = typeOfVaccineServices.GetTypeOfVaccines()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = typeOfVaccines.PageCount,
                PageNumber = typeOfVaccines.PageNumber,
                TotalItems = typeOfVaccines.TotalItemCount,
                Data = typeOfVaccines,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.TypeOfVaccine.View)]
        public IActionResult SearchTypeOfVaccines(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var typeOfVaccines = typeOfVaccineServices.SearchTypeOfVaccines(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = typeOfVaccines.PageCount,
                PageNumber = typeOfVaccines.PageNumber,
                TotalItems = typeOfVaccines.TotalItemCount,
                Data = typeOfVaccines,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.TypeOfVaccine.Delete)]
        public async Task<IActionResult> DeleteTypeOfVaccine(int id)
        {
            var result = await typeOfVaccineServices.DeleteTypeOfVaccine(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá loại vac-xin thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá loại vac-xin." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.TypeOfVaccine.Create)]
        public async Task<IActionResult> InsertTypeOfVaccine(TypeOfVaccineModel typeOfVaccineModel)
        {
            var result = await typeOfVaccineServices.InsertTypeOfVaccine(typeOfVaccineModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = typeOfVaccineModel,
                    Messages = new string[] { "Thêm loại vắc xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = result,
                Messages = new string[] { "Thêm loại vắc xin thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.TypeOfVaccine.Create)]
        public async Task<IActionResult> InsertTypeOfVaccinesRange(IList<TypeOfVaccineModel> typeOfVaccineModels)
        {
            var result = await typeOfVaccineServices.InsertTypeOfVaccinesRange(typeOfVaccineModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = typeOfVaccineModels,
                    Messages = new string[] { "Thêm loại vắc xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm loại vắc xin thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.TypeOfVaccine.Edit)]
        public async Task<IActionResult> UpdateTypeOfVaccine(int id, TypeOfVaccineModel typeOfVaccineModel)
        {
            var result = await typeOfVaccineServices.UpdateTypeOfVaccine(id, typeOfVaccineModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa loại vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = result,
                Messages = new string[] { "Sửa loại vắc xin thất bại." }
            });
        }

        [HttpGet("[Action]/{id}")]
        [Authorize(Roles = Permissions.TypeOfVaccine.View)]
        public async Task<IActionResult> GetTypeOfVaccine(int id)
        {
            var data = await typeOfVaccineServices.GetTypeOfVaccine(id);
            return Ok(new ResponseAPI
            {
                Data = data,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.TypeOfVaccine.View)]
        public IActionResult GetAllTypeOfVaccines()
        {
            var data = typeOfVaccineServices.GetTypeOfVaccines().OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                Data = data,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }
    }
}
