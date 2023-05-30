using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IStatisticalServices
    {
        IQueryable<VaccineOutOfStock> GetVaccineOutOfStocks();
        IQueryable<ShipmentInventory> GetShipmentInventorys();
        IQueryable<ShipmentExpires> GetShipmentExpires();
        IEnumerable<TopCustomerPay> GetTopCustomerPays();
        Revenue GetRevenueByDate(DateTime date);
        Revenue GetRevenueByYear(int year);
        Revenue GetRevenueByMonth(int month,int year);
        IList<RevenueEachMonthOfYear> GetRevenuesEachMonthOfYear(int year);
        IList<RevenueOther> GetRevenuesOther(DateTime dateFrom,DateTime dateTo);
        IList<RevenueQuarterOfYear> GetRevenuesQuarterOfYear(int year);
        IList<Incident> GetInjectionIncidentByMonth(int month,int year);

        OrderEntrySlip GetOrderEntrySlipByDate(DateTime date);
        OrderEntrySlip GetOrderEntrySlipByYear(int year);
        OrderEntrySlip GetOrderEntrySlipByMonth(int month, int year);
        IList<EntrySlipOfMonthByOrder> GetEntrySlipsOfMonthByOrder(int month, int year);
        IList<OrderEntrySlipEachMonthOfYear> GetOrderEntrySlipsEachMonthOfYear(int year);
        IList<OrderEntrySlipOther> GetOrderEntrySlipsOther(DateTime dateFrom, DateTime dateTo);
        IList<OrderEntrySlipQuarterOfYear> GetOrderEntrySlipsQuarterOfYear(int year);
    }
}
