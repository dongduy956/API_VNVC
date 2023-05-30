using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.Services;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VaccineController : ControllerBase
    {
        private readonly IVaccineServices vaccineServices;
        public VaccineController(IVaccineServices vaccineServices)
        {
            this.vaccineServices = vaccineServices;
        }
        [Authorize(Roles = Permissions.Vaccine.View)]
        [HttpGet("[Action]/{shipmentId}")]
        public IActionResult GetVaccineByShipmentId(int shipmentId)
        {
            var vaccine = vaccineServices.GetVaccineByShipmentId(shipmentId);
            if (vaccine == null)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = vaccine,
            });
        }

        [Authorize(Roles = Permissions.Vaccine.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetVaccine(int id)
        {
            var vaccine = await vaccineServices.GetVaccine(id);
            if (vaccine == null)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = vaccine,
            });
        }

        [Authorize(Roles = Permissions.Vaccine.View)]
        [HttpGet("[Action]")]
        public IActionResult GetAllVaccines()
        {
            var vaccines = vaccineServices.GetVaccines()
               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = vaccines,
            });
        }

        [Authorize(Roles = Permissions.Vaccine.View)]
        [HttpGet("[Action]")]
        public IActionResult GetVaccines(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccines = vaccineServices.GetVaccines()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccines.PageCount,
                PageNumber = vaccines.PageNumber,
                TotalItems = vaccines.TotalItemCount,
                Data = vaccines,
            });
        }

        [Authorize(Roles = Permissions.Vaccine.View)]
        [HttpGet("[Action]/{typeOfVaccineId}")]
        public IActionResult GetVaccines(int? page, int? pageSize, int typeOfVaccineId, string? q)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccines = vaccineServices.GetVaccines()
                .Where(x => x.TypeOfVaccineId == typeOfVaccineId && x.Name.Contains(q??""))
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccines.PageCount,
                PageNumber = vaccines.PageNumber,
                TotalItems = vaccines.TotalItemCount,
                Data = vaccines,
            });
        }

        [Authorize(Roles = Permissions.Vaccine.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchVaccines(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccines = vaccineServices.SearchVaccines(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccines.PageCount,
                PageNumber = vaccines.PageNumber,
                TotalItems = vaccines.TotalItemCount,
                Data = vaccines,
            });
        }

        [Authorize(Roles = Permissions.Vaccine.Delete)]
        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            var result = await vaccineServices.DeleteVaccine(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá vac-xin thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá vac-xin." }
            });
        }

        [Authorize(Roles = Permissions.Vaccine.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertVaccine(VaccineModel vaccineModel)
        {
            var result = await vaccineServices.InsertVaccine(vaccineModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccineModel,
                    Messages = new string[] { "Thêm vắc xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = result,
                Messages = new string[] { "Thêm vắc xin thất bại." }
            });
        }

        [Authorize(Roles = Permissions.Vaccine.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertVaccinesRange(IList<VaccineModel> vaccineModels)
        {
            var result = await vaccineServices.InsertVaccinesRange(vaccineModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccineModels,
                    Messages = new string[] { "Thêm vắc xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm vắc xin thất bại." }
            });
        }

        [Authorize(Roles = Permissions.Vaccine.Edit)]
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, VaccineModel vaccineModel)
        {
            var result = await vaccineServices.UpdateVaccine(id, vaccineModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Data = result,
                Messages = new string[] { "Thêm vắc xin thất bại." }
            });
        }
    }
}
