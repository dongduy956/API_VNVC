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
    public class VaccinePackageController : ControllerBase
    {
        private readonly IVaccinePackageServices vaccinePackageServices;
        public VaccinePackageController(IVaccinePackageServices vaccinePackageServices)
        {
            this.vaccinePackageServices = vaccinePackageServices;
        }
        [Authorize(Roles = Permissions.VaccinePackage.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetVaccinePackage(int id)
        {
            var vaccinePackage =await vaccinePackageServices.GetVaccinePackage(id);
            if (vaccinePackage != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePackage
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [Authorize(Roles = Permissions.VaccinePackage.View)]
        [HttpGet("[Action]")]
        public IActionResult GetAllVaccinePackages()
        {
            var vaccinePackages = vaccinePackageServices.GetVaccinePackages()
                                 .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = vaccinePackages,
            });
        }
        [Authorize(Roles = Permissions.VaccinePackage.View)]
        [HttpGet("[Action]")]
        public IActionResult GetVaccinePackages(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccinePackages = vaccinePackageServices.GetVaccinePackages()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccinePackages.PageCount,
                PageNumber = vaccinePackages.PageNumber,
                TotalItems = vaccinePackages.TotalItemCount,
                Data = vaccinePackages,
            });
        }
        [Authorize(Roles = Permissions.VaccinePackage.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchVaccinePackages(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccinePackages = vaccinePackageServices.SearchVaccinePackages(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccinePackages.PageCount,
                PageNumber = vaccinePackages.PageNumber,
                TotalItems = vaccinePackages.TotalItemCount,
                Data = vaccinePackages,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.VaccinePackage.Delete)]
        public async Task<IActionResult> DeleteVaccinePackage(int id)
        {
            var result = await vaccinePackageServices.DeleteVaccinePackage(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá gói vac-xin thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá gói vac-xin." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.VaccinePackage.Create)]
        public async Task<IActionResult> InsertVaccinePackage(VaccinePackageModel vaccinePackageModel)
        {
            var result = await vaccinePackageServices.InsertVaccinePackage(vaccinePackageModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePackageModel,
                    Messages = new string[] { "Thêm gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm gói vac-xin thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.VaccinePackage.Create)]
        public async Task<IActionResult> InsertVaccinePackagesRange(IList<VaccinePackageModel> vaccinePackageModels)
        {
            var result = await vaccinePackageServices.InsertVaccinePackagesRange(vaccinePackageModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePackageModels,
                    Messages = new string[] { "Thêm gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm gói vac-xin thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.VaccinePackage.Edit)]
        public async Task<IActionResult> UpdateVaccinePackage(int id, VaccinePackageModel vaccinePackageModel)
        {
            var result = await vaccinePackageServices.UpdateVaccinePackage(id, vaccinePackageModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa gói vac-xin thất bại." }
            });
        }
    }
}
