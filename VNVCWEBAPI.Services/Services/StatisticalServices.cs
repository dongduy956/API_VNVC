using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class StatisticalServices : IStatisticalServices
    {

        private readonly IShipmentServices shipmentServices;
        private readonly IInjectionScheduleServices injectionScheduleServices;
        private readonly IPayServices payServices;
        private readonly IEntrySlipServices entrySlipServices;
        private readonly IOrderServices orderServices;
        private readonly IInjectionIncidentServices injectionIncidentServices;
        public StatisticalServices(IShipmentServices shipmentServices, IInjectionScheduleServices injectionScheduleServices, IPayServices payServices, IEntrySlipServices entrySlipServices, IOrderServices orderServices, IInjectionIncidentServices injectionIncidentServices)
        {
            this.shipmentServices = shipmentServices;
            this.injectionScheduleServices = injectionScheduleServices;
            this.payServices = payServices;
            this.entrySlipServices = entrySlipServices;
            this.orderServices = orderServices;
            this.injectionIncidentServices = injectionIncidentServices;
        }
        private Revenue GetRevenue(DateTime date, int month = 0, int year = 0)
        {
            double totalPay, totalEntrySlip;
            totalPay = totalEntrySlip = 0;
            var pays = payServices.GetPays().Where(x => x.Created.Day == date.Day && x.Created.Month == date.Month && x.Created.Year == date.Year);
            var entrySlips = entrySlipServices.GetEntrySlips().Where(x => x.Created.Day == date.Day && x.Created.Month == date.Month && x.Created.Year == date.Year);
            if (month != 0)
            {
                pays = payServices.GetPays().Where(x => x.Created.Month == month && x.Created.Year == year);
                entrySlips = entrySlipServices.GetEntrySlips().Where(x => x.Created.Month == month && x.Created.Year == year);
            }
            else
             if (year != 0)
            {
                pays = payServices.GetPays().Where(x => x.Created.Year == year);
                entrySlips = entrySlipServices.GetEntrySlips().Where(x => x.Created.Year == year);
            }
            totalPay = pays.ToList().Sum(x => x.Total);
            totalEntrySlip = entrySlips.ToList().Sum(x => x.Total);
            return new Revenue
            {
                TotalPay = totalPay,
                TotalEntrySlip = totalEntrySlip
            };
        }
        public IList<RevenueEachMonthOfYear> GetRevenuesEachMonthOfYear(int year)
        {
            var list = new List<RevenueEachMonthOfYear>();
            for (int i = 1; i < 13; i++)
            {
                var revenue = GetRevenue(DateTime.Now, i, year);
                list.Add(new RevenueEachMonthOfYear
                {
                    Month = i,
                    TotalEntrySlip = revenue.TotalEntrySlip,
                    TotalPay = revenue.TotalPay
                });
            }
            return list;
        }

        public IList<RevenueOther> GetRevenuesOther(DateTime dateFrom, DateTime dateTo)
        {
            dateFrom = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day);
            dateTo = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day);
            var list = new List<RevenueOther>();
            for (DateTime date = dateFrom; date.CompareTo(dateTo) <= 0; date = date.AddDays(1))
            {
                var revenue = GetRevenue(date);
                list.Add(new RevenueOther
                {
                    Date = date,
                    TotalEntrySlip = revenue.TotalEntrySlip,
                    TotalPay = revenue.TotalPay
                });
            }
            return list;
        }
        public IList<RevenueQuarterOfYear> GetRevenuesQuarterOfYear(int year)
        {
            var list = new List<RevenueQuarterOfYear>();
            double totalPay, totalEntrySlip;
            for (int i = 0; i < 4; i++)
            {
                totalPay = totalEntrySlip = 0;
                for (int j = i * 3 + 1; j <= i * 3 + 3; j++)
                {
                    var revenue = GetRevenue(DateTime.Now, j, year);
                    totalPay += revenue.TotalPay;
                    totalEntrySlip += revenue.TotalEntrySlip;
                }
                list.Add(new RevenueQuarterOfYear
                {
                    Quarter = i + 1,
                    TotalPay = totalPay,
                    TotalEntrySlip = totalEntrySlip
                });
            }
            return list;
        }

        public Revenue GetRevenueByDate(DateTime date)
        {
            return GetRevenue(date);
        }
        public Revenue GetRevenueByYear(int year)
        {
            return GetRevenue(DateTime.Now, 0, year);
        }
        public Revenue GetRevenueByMonth(int month, int year)
        {
            return GetRevenue(DateTime.Now, month, year);
        }
        public IQueryable<ShipmentExpires> GetShipmentExpires()
        {
            return shipmentServices.GetShipments()
                   .Where(x => x.ExpirationDate.CompareTo(DateTime.Now.AddDays(2)) <= 0)
                   .Select(x => new ShipmentExpires
                   {
                       Date = x.ExpirationDate,
                       Shipment = x.ShipmentCode,
                       Vaccine = x.VaccineName
                   });
        }

        public IQueryable<ShipmentInventory> GetShipmentInventorys()
        {
            return shipmentServices.GetShipments()
                     .Where(x => x.QuantityRemain >= 100)
                     .Select(x => new ShipmentInventory
                     {
                         QuantityRemain = x.QuantityRemain ?? 0,
                         Shipment = x.ShipmentCode,
                         Vaccine = x.VaccineName
                     });
        }

        public IEnumerable<TopCustomerPay> GetTopCustomerPays()
        {
            var injectionSchedules = injectionScheduleServices.GetInjectionSchedules();
            var pays = payServices.GetPays();
            var listTopCustomer = new List<TopCustomerPay>();
            foreach (var injectionSchedule in injectionSchedules)
            {
                var item = listTopCustomer.SingleOrDefault(x => x.CustomerId == injectionSchedule.CustomerId);
                var total = pays.SingleOrDefault(x => x.InjectionScheduleId == injectionSchedule.Id)?.Total ?? 0;
                if (total > 0)
                {
                    if (item != null)
                        item.Total += total;
                    else
                        listTopCustomer.Add(new TopCustomerPay
                        {
                            CustomerId = injectionSchedule.CustomerId ?? 0,
                            CustomerName = injectionSchedule.CustomerName ?? "",
                            Total = total
                        });
                }
            }
            return listTopCustomer.OrderByDescending(x => x.Total).Take(5);
        }



        public IQueryable<VaccineOutOfStock> GetVaccineOutOfStocks()
        {
            return shipmentServices.GetShipments()
                    .Where(x => x.QuantityRemain <= 5)
                    .Select(x => new VaccineOutOfStock
                    {
                        QuantityRemain = x.QuantityRemain ?? 0,
                        Shipment = x.ShipmentCode,
                        Vaccine = x.VaccineName
                    });
        }

        public IList<Incident> GetInjectionIncidentByMonth(int month, int year)
        {
            var list = new List<Incident>();
            var incidents = injectionIncidentServices.GetInjectionIncidents().Where(x => x.Created.Month == month && x.Created.Year == year);
            foreach (var incident in incidents)
            {
                if (list.SingleOrDefault(x => x.Shipment.Equals(incident.ShipmentCode)) == null)
                    list.Add(new Incident
                    {
                        Shipment = incident.ShipmentCode,
                        Vaccine = incident.VaccineName
                    });
            }
            return list;
        }

        private OrderEntrySlip GetOrderEntrySlip(DateTime date, int month = 0, int year = 0)
        {
            double totalOrder, totalEntrySlip;
            totalOrder = totalEntrySlip = 0;
            var orders = orderServices.GetOrders().Where(x => x.Created.Day == date.Day && x.Created.Month == date.Month && x.Created.Year == date.Year);
            var entrySlips = entrySlipServices.GetEntrySlips().Where(x => x.Created.Day == date.Day && x.Created.Month == date.Month && x.Created.Year == date.Year);
            if (month != 0)
            {
                orders = orderServices.GetOrders().Where(x => x.Created.Month == month && x.Created.Year == year);
                entrySlips = entrySlipServices.GetEntrySlips().Where(x => x.Created.Month == month && x.Created.Year == year);
            }
            else
             if (year != 0)
            {
                orders = orderServices.GetOrders().Where(x => x.Created.Year == year);
                entrySlips = entrySlipServices.GetEntrySlips().Where(x => x.Created.Year == year);
            }
            totalOrder = orders.ToList().Sum(x => x.Total) ?? 0;
            totalEntrySlip = entrySlips.ToList().Sum(x => x.Total);
            return new OrderEntrySlip
            {
                TotalOrder = totalOrder,
                TotalEntrySlip = totalEntrySlip
            };
        }
        public OrderEntrySlip GetOrderEntrySlipByDate(DateTime date)
        {
            return GetOrderEntrySlip(date);
        }

        public OrderEntrySlip GetOrderEntrySlipByYear(int year)
        {
            return GetOrderEntrySlip(DateTime.Now, 0, year);

        }

        public OrderEntrySlip GetOrderEntrySlipByMonth(int month, int year)
        {
            return GetOrderEntrySlip(DateTime.Now, month, year);
        }

        public IList<OrderEntrySlipEachMonthOfYear> GetOrderEntrySlipsEachMonthOfYear(int year)
        {
            var list = new List<OrderEntrySlipEachMonthOfYear>();
            for (int i = 1; i < 13; i++)
            {
                var orderEntrySlip = GetOrderEntrySlip(DateTime.Now, i, year);
                list.Add(new OrderEntrySlipEachMonthOfYear
                {
                    Month = i,
                    TotalEntrySlip = orderEntrySlip.TotalEntrySlip,
                    TotalOrder = orderEntrySlip.TotalOrder
                });
            }
            return list;
        }

        public IList<OrderEntrySlipOther> GetOrderEntrySlipsOther(DateTime dateFrom, DateTime dateTo)
        {
            dateFrom = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day);
            dateTo = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day);
            var list = new List<OrderEntrySlipOther>();
            for (DateTime date = dateFrom; date.CompareTo(dateTo) <= 0; date = date.AddDays(1))
            {
                var orderEntrySlip = GetOrderEntrySlip(date);
                list.Add(new OrderEntrySlipOther
                {
                    Date = date,
                    TotalEntrySlip = orderEntrySlip.TotalEntrySlip,
                    TotalOrder = orderEntrySlip.TotalOrder
                });
            }
            return list;
        }

        public IList<OrderEntrySlipQuarterOfYear> GetOrderEntrySlipsQuarterOfYear(int year)
        {
            var list = new List<OrderEntrySlipQuarterOfYear>();
            double totalOrder, totalEntrySlip;
            for (int i = 0; i < 4; i++)
            {
                totalOrder = totalEntrySlip = 0;
                for (int j = i * 3 + 1; j <= i * 3 + 3; j++)
                {
                    var orderEntrySlip = GetOrderEntrySlip(DateTime.Now, j, year);
                    totalOrder += orderEntrySlip.TotalOrder;
                    totalEntrySlip += orderEntrySlip.TotalEntrySlip;
                }
                list.Add(new OrderEntrySlipQuarterOfYear
                {
                    Quarter = i + 1,
                    TotalOrder = totalOrder,
                    TotalEntrySlip = totalEntrySlip
                });
            }
            return list;
        }

        public IList<EntrySlipOfMonthByOrder> GetEntrySlipsOfMonthByOrder(int month, int year)
        {
            var list = new List<EntrySlipOfMonthByOrder>();
            var orders = orderServices.GetOrders().Where(x => x.Created.Month == month && x.Created.Year == year);

            foreach (var order in orders)
            {
                double totalEntrySlip = entrySlipServices.GetEntrySlipsByOrderId(order.Id).ToList().Sum(x => x.Total);
                list.Add(new EntrySlipOfMonthByOrder
                {
                    OrderId = order.Id,
                    TotalOrder = order.Total ?? 0,
                    TotalEntrySlip = totalEntrySlip
                });
            }
            return list;
        }
    }
}
