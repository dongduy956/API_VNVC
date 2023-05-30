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
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentServices shipmentServices;
        public ShipmentController(IShipmentServices shipmentServices)
        {
            this.shipmentServices = shipmentServices;
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public async Task<IActionResult> GetShipmentsByIds(int[] ids)
        {

            var vaccines = (await shipmentServices.GetShipmentsByIds(ids))
                                        .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = vaccines,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public IActionResult GetShipments(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var shipments = shipmentServices.GetShipments()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = shipments.PageCount,
                PageNumber = shipments.PageNumber,
                TotalItems = shipments.TotalItemCount,
                Data = shipments,
            });
        }
        [HttpGet("[Action]/{supplierId}")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public IActionResult GetShipmentsBySupplierId(int supplierId)
        {
            var shipments = shipmentServices.GetShipmentsBySupplierId(supplierId)
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = shipments,
            });
        }
        [HttpGet("[Action]/{vaccineId}")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public IActionResult GetShipmentsByVaccineId(int vaccineId)
        {
            var shipments = shipmentServices.GetShipmentsByVaccineId(vaccineId)
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = shipments,
            });
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public IActionResult GetAllShipments()
        {
            var shipments = shipmentServices.GetShipments()
                .Where(x => x.ExpirationDate.CompareTo(DateTime.Now) >= 0)
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = shipments,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public IActionResult SearchShipments(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var shipments = shipmentServices.SearchShipments(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = shipments.PageCount,
                PageNumber = shipments.PageNumber,
                TotalItems = shipments.TotalItemCount,
                Data = shipments,
            });
        }

        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Shipment.Delete)]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var result = await shipmentServices.DeleteShipment(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá lô hàng thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá lô hàng." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Shipment.Create)]
        public async Task<IActionResult> InsertShipment(ShipmentModel shipmentModel)
        {
            var result = await shipmentServices.InsertShipment(shipmentModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = shipmentModel,
                    Messages = new string[] { "Thêm lô hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm lô hàng thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Shipment.Create)]
        public async Task<IActionResult> InsertShipmentsRange(IList<ShipmentModel> shipmentModels)
        {
            var result = await shipmentServices.InsertShipmentsRange(shipmentModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = shipmentModels,
                    Messages = new string[] { "Thêm lô hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm lô hàng thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Shipment.Edit)]
        public async Task<IActionResult> UpdateShipment(int id, ShipmentModel shipmentModel)
        {
            var result = await shipmentServices.UpdateShipment(id, shipmentModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa lô hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa lô hàng thất bại." }
            });

        }
        [HttpGet("[Action]/{id}")]
        [Authorize(Roles = Permissions.Shipment.View)]
        public async Task<IActionResult> GetShipment(int id)
        {
            var shipment = await shipmentServices.GetShipment(id);
            if (shipment != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    Data = shipment,
                    isSuccess = true,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
    }
}
