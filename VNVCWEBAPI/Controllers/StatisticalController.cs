using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.IServices;

namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticalController : BaseController
    {
        private readonly IStatisticalServices statisticalServices;
        public StatisticalController(IStatisticalServices statisticalServices)
        {
            this.statisticalServices = statisticalServices;
        }
        [HttpGet("[Action]")]
        public IActionResult GetVaccineOutOfStocks()
        {
            var data = statisticalServices.GetVaccineOutOfStocks();
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetShipmentExpires()
        {
            var data = statisticalServices.GetShipmentExpires();
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetShipmentInventorys()
        {
            var data = statisticalServices.GetShipmentInventorys();
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetTopCustomerPays()
        {
            var data = statisticalServices.GetTopCustomerPays();
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetRevenueByYear(int year)
        {
            var data = statisticalServices.GetRevenueByYear(year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetRevenueByDate(DateTime date)
        {
            var data = statisticalServices.GetRevenueByDate(date);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetRevenuesOther(DateTime dateFrom, DateTime dateTo)
        {
            var data = statisticalServices.GetRevenuesOther(dateFrom, dateTo);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetRevenuesEachMonthOfYear(int year)
        {
            var data = statisticalServices.GetRevenuesEachMonthOfYear(year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetRevenuesQuarterOfYear(int year)
        {
            var data = statisticalServices.GetRevenuesQuarterOfYear(year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetRevenueByMonth(int month,int year)
        {
            var data = statisticalServices.GetRevenueByMonth(month,year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetInjectionIncidentByMonth(int month, int year)
        {
            var data = statisticalServices.GetInjectionIncidentByMonth(month, year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }

        [HttpGet("[Action]")]
        public IActionResult GetOrderEntrySlipByYear(int year)
        {
            var data = statisticalServices.GetOrderEntrySlipByYear(year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetOrderEntrySlipByDate(DateTime date)
        {
            var data = statisticalServices.GetOrderEntrySlipByDate(date);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetOrderEntrySlipsOther(DateTime dateFrom, DateTime dateTo)
        {
            var data = statisticalServices.GetOrderEntrySlipsOther(dateFrom, dateTo);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetOrderEntrySlipsEachMonthOfYear(int year)
        {
            var data = statisticalServices.GetOrderEntrySlipsEachMonthOfYear(year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetOrderEntrySlipsQuarterOfYear(int year)
        {
            var data = statisticalServices.GetOrderEntrySlipsQuarterOfYear(year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetOrderEntrySlipByMonth(int month, int year)
        {
            var data = statisticalServices.GetOrderEntrySlipByMonth(month, year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
        [HttpGet("[Action]")]
        public IActionResult GetEntrySlipsOfMonthByOrder(int month, int year)
        {
            var data = statisticalServices.GetEntrySlipsOfMonthByOrder(month, year);
            return Ok(new ResponseAPI { isSuccess = true, StatusCode = Ok().StatusCode, Data = data });
        }
    }
}
