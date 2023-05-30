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
    public class VaccinePriceController : ControllerBase
    {
        private readonly IVaccinePriceServices vaccinePriceServices;
        public VaccinePriceController(IVaccinePriceServices vaccinePriceServices)
        {
            this.vaccinePriceServices = vaccinePriceServices;
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.VaccinePrice.View)]
        public IActionResult GetVaccinePrices(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccinePrices = vaccinePriceServices.GetVaccinePrices()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccinePrices.PageCount,
                PageNumber = vaccinePrices.PageNumber,
                TotalItems = vaccinePrices.TotalItemCount,
                Data = vaccinePrices,
            });
        }
        [Authorize(Roles = Permissions.VaccinePrice.View)]
        [HttpGet("[Action]")]
        public IActionResult GetVaccinePriceLastByVaccineIdAndShipmentId(int vaccineId, int shipmentId)
        {

            var vaccinePrice = vaccinePriceServices
                               .GetVaccinePriceLastByVaccineIdAndShipmentId(vaccineId, shipmentId);
            if (vaccinePrice == null)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                    Messages = new[] { "Có lỗi xảy ra khi lấy giá vaccine." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,

                Data = vaccinePrice,
            });
        }

        [Authorize(Roles = Permissions.VaccinePrice.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchVaccinePrices(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var vaccinePrices = vaccinePriceServices.SearchVaccinePrices(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = vaccinePrices.PageCount,
                PageNumber = vaccinePrices.PageNumber,
                TotalItems = vaccinePrices.TotalItemCount,
                Data = vaccinePrices,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.VaccinePrice.Delete)]
        public async Task<IActionResult> DeleteVaccinePrice(int id)
        {
            var result = await vaccinePriceServices.DeleteVaccinePrice(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá giá vac-xin thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá giá vac-xin." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.VaccinePrice.Create)]
        public async Task<IActionResult> InsertVaccinePrice(VaccinePriceModel vaccinePriceModel)
        {
            var result = await vaccinePriceServices.InsertVaccinePrice(vaccinePriceModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePriceModel,
                    Messages = new string[] { "Thêm giá vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm giá vac-xin thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.VaccinePrice.Create)]
        public async Task<IActionResult> InsertVaccinePricesRange(IList<VaccinePriceModel> vaccinePriceModels)
        {
            var result = await vaccinePriceServices.InsertVaccinePricesRange(vaccinePriceModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = vaccinePriceModels,
                    Messages = new string[] { "Thêm giá vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm giá vac-xin thất bại." }
            });
        }
        [HttpPut("[Action]")]
        [Authorize(Roles = Permissions.VaccinePrice.Edit)]
        public async Task<IActionResult> UpdateVaccinePrice(VaccinePriceModel vaccinePriceModel)
        {
            var result = await vaccinePriceServices.UpdateVaccinePrice(vaccinePriceModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật giá vac-xin thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật giá vac-xin thất bại." }
            });
        }
    }
}
