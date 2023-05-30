using Microsoft.AspNetCore.Authorization;
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
    public class VaccinePackageDetailController : ControllerBase
    {
        private readonly IVaccinePackageDetailServices vaccinePackageDetailServices;
        public VaccinePackageDetailController(IVaccinePackageDetailServices vaccinePackageDetailServices)
        {
            this.vaccinePackageDetailServices = vaccinePackageDetailServices;
        }
        [Authorize(Roles = Permissions.VaccinePackageDetail.View)]
        [HttpGet("[Action]")]
        public IActionResult GetVaccinePackageDetail(int vaccinePackageId, int shipmentId)
        {

            var vaccinePackageDetail =
                 vaccinePackageDetailServices
                .GetVaccinePackageDetail(vaccinePackageId, shipmentId);
            if (vaccinePackageDetail != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePackageDetail,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.VaccinePackageDetail.View)]
        public IActionResult GetAllVaccinePackageDetails()
        {
            var data = vaccinePackageDetailServices.GetVaccinePackageDetails().OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                Data = data,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }
        [Authorize(Roles = Permissions.VaccinePackageDetail.View)]
        [HttpGet("[Action]")]
        public IActionResult GetVaccinePackageDetailsByVaccinePackageId(int vaccinePackageId)
        {

            var vaccinePackageDetails =
                 vaccinePackageDetailServices
                .GetVaccinePackageDetailsByVaccinePackageId(vaccinePackageId)
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = vaccinePackageDetails,
            });
        }
        [Authorize(Roles = Permissions.VaccinePackageDetail.Delete)]
        [HttpDelete("[Action]/{vaccinePackageId}")]
        public async Task<IActionResult> DeleteVaccinePackageDetailByVaccinePackageId(int vaccinePackageId)
        {
            var result = await vaccinePackageDetailServices.DeleteVaccinePackageDetailByVaccinePackageId(vaccinePackageId);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chi tiết gói vac-xin thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá chi tiết gói vac-xin." }
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.VaccinePackageDetail.Delete)]
        public async Task<IActionResult> DeleteVaccinePackageDetail(int id)
        {
            var result = await vaccinePackageDetailServices.DeleteVaccinePackageDetail(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chi tiết gói vac-xin thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá chi tiết gói vac-xin." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.VaccinePackageDetail.Create)]
        public async Task<IActionResult> InsertVaccinePackageDetail(VaccinePackageDetailModel vaccinePackageDetailModel)
        {
            var result = await vaccinePackageDetailServices.InsertVaccinePackageDetail(vaccinePackageDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePackageDetailModel,
                    Messages = new string[] { "Thêm chi tiết gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết gói vac-xin thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.VaccinePackageDetail.Create)]
        public async Task<IActionResult> InsertVaccinePackageDetailsRange(IList<VaccinePackageDetailModel> vaccinePackageDetailModels)
        {
            var result = await vaccinePackageDetailServices.InsertVaccinePackageDetailsRange(vaccinePackageDetailModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Messages = new string[] { "Thêm chi tiết gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết gói vac-xin thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.VaccinePackageDetail.Edit)]
        public async Task<IActionResult> UpdateVaccinePackageDetail(int id, VaccinePackageDetailModel vaccinePackageDetailModel)
        {
            var result = await vaccinePackageDetailServices.UpdateVaccinePackageDetail(id, vaccinePackageDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa chi tiết gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa chi tiết gói vac-xin thất bại." }
            });
        }
        [HttpPut("[Action]")]
        [Authorize(Roles = Permissions.VaccinePackageDetail.Edit)]
        public async Task<IActionResult> UpdateVaccinePackageDetailsRange(IList<VaccinePackageDetailModel> vaccinePackageDetailModels)
        {
            var result = await vaccinePackageDetailServices.UpdateVaccinePackageDetailsRange(vaccinePackageDetailModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa chi tiết gói vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa chi tiết gói vac-xin thất bại." }
            });
        }
    }
}
