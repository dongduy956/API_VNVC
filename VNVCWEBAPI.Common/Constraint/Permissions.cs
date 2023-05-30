using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Constraint
{
    public static class Permissions
    {
        public static string claimType = "Permission";
        private const string Administrator = $"{DefaultRoles.SuperAdmin},{DefaultRoles.Admin}";
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }
        public static class AdditionalCustomerInformation
        {
            public const string View = $"Permissions.AdditionalCustomerInformation.View,{Administrator}";
            public const string Create = $"Permissions.AdditionalCustomerInformation.Create,{Administrator}";
            public const string Edit = $"Permissions.AdditionalCustomerInformation.Edit,{Administrator}";
            public const string Delete = $"Permissions.AdditionalCustomerInformation.Delete,{Administrator}";

        }
        public static class Banner
        {
            public const string View = $"Permissions.Banner.View,{Administrator}";
            public const string Create = $"Permissions.Banner.Create,{Administrator}";
            public const string Edit = $"Permissions.Banner.Edit,{Administrator}";
            public const string Delete = $"Permissions.Banner.Delete,{Administrator}";

        }
        public static class ConditionPromotion
        {
            public const string View = $"Permissions.ConditionPromotion.View,{Administrator}";
            public const string Create = $"Permissions.ConditionPromotion.Create,{Administrator}";
            public const string Edit = $"Permissions.ConditionPromotion.Edit,{Administrator}";
            public const string Delete = $"Permissions.ConditionPromotion.Delete,{Administrator}";

        }
        public static class Customer
        {
            public const string View = $"Permissions.Customer.View,{Administrator}";
            public const string Create = $"Permissions.Customer.Create,{Administrator}";
            public const string Edit = $"Permissions.Customer.Edit,{Administrator}";
            public const string Delete = $"Permissions.Customer.Delete,{Administrator}";

        }
        public static class CustomerRankDetail
        {
            public const string View = $"Permissions.CustomerRankDetail.View,{Administrator}";
            public const string Create = $"Permissions.CustomerRankDetail.Create,{Administrator}";
            public const string Edit = $"Permissions.CustomerRankDetail.Edit,{Administrator}";
            public const string Delete = $"Permissions.CustomerRankDetail.Delete,{Administrator}";
        }
        public static class CustomerType
        {
            public const string View = $"Permissions.CustomerType.View,{Administrator}";
            public const string Create = $"Permissions.CustomerType.Create,{Administrator}";
            public const string Edit = $"Permissions.CustomerType.Edit,{Administrator}";
            public const string Delete = $"Permissions.CustomerType.Delete,{Administrator}";
        }
        public static class CustomerRank
        {
            public const string View = $"Permissions.CustomerRank.View,{Administrator}";
            public const string Create = $"Permissions.CustomerRank.Create,{Administrator}";
            public const string Edit = $"Permissions.CustomerRank.Edit,{Administrator}";
            public const string Delete = $"Permissions.CustomerRank.Delete,{Administrator}";
        }
        public static class EntrySlip
        {
            public const string View = $"Permissions.EntrySlip.View,{Administrator}";
            public const string Create = $"Permissions.EntrySlip.Create,{Administrator}";
            public const string Edit = $"Permissions.EntrySlip.Edit,{Administrator}";
            public const string Delete = $"Permissions.EntrySlip.Delete,{Administrator}";
        }
        public static class EntrySlipDetail
        {
            public const string View = $"Permissions.EntrySlipDetail.View,{Administrator}";
            public const string Create = $"Permissions.EntrySlipDetail.Create,{Administrator}";
            public const string Edit = $"Permissions.EntrySlipDetail.Edit,{Administrator}";
            public const string Delete = $"Permissions.EntrySlipDetail.Delete,{Administrator}";
        }
        public static class Login
        {
            public const string View = $"Permissions.Login.View,{Administrator}";
            public const string Create = $"Permissions.Login.Create,{Administrator}";
            public const string Edit = $"Permissions.Login.Edit,{Administrator}";
            public const string Delete = $"Permissions.Login.Delete,{Administrator}";
        }
        public static class LoginSession
        {
            public const string View = $"Permissions.LoginSession.View,{Administrator}";
            public const string Create = $"Permissions.LoginSession.Create,{Administrator}";
            public const string Edit = $"Permissions.LoginSession.Edit,{Administrator}";
            public const string Delete = $"Permissions.LoginSession.Delete,{Administrator}";
        }
        public static class Order
        {
            public const string View = $"Permissions.Order.View,{Administrator}";
            public const string Create = $"Permissions.Order.Create,{Administrator}";
            public const string Edit = $"Permissions.Order.Edit,{Administrator}";
            public const string Delete = $"Permissions.Order.Delete,{Administrator}";
        }
        public static class OrderDetail
        {
            public const string View = $"Permissions.OrderDetail.View,{Administrator}";
            public const string Create = $"Permissions.OrderDetail.Create,{Administrator}";
            public const string Edit = $"Permissions.OrderDetail.Edit,{Administrator}";
            public const string Delete = $"Permissions.OrderDetail.Delete,{Administrator}";
        }
        public static class Pay
        {
            public const string View = $"Permissions.Pay.View,{Administrator}";
            public const string Create = $"Permissions.Pay.Create,{Administrator}";
            public const string Edit = $"Permissions.Pay.Edit,{Administrator}";
            public const string Delete = $"Permissions.Pay.Delete,{Administrator}";
        }
        public static class PayDetail
        {
            public const string View = $"Permissions.PayDetail.View,{Administrator}";
            public const string Create = $"Permissions.PayDetail.Create,{Administrator}";
            public const string Edit = $"Permissions.PayDetail.Edit,{Administrator}";
            public const string Delete = $"Permissions.PayDetail.Delete,{Administrator}";
        }
        public static class PaymentMethod
        {
            public const string View = $"Permissions.PaymentMethod.View,{Administrator}";
            public const string Create = $"Permissions.PaymentMethod.Create,{Administrator}";
            public const string Edit = $"Permissions.PaymentMethod.Edit,{Administrator}";
            public const string Delete = $"Permissions.PaymentMethod.Delete,{Administrator}";

        }
        public static class Permission
        {
            public const string View = $"Permissions.Permission.View,{Administrator}";
            public const string Create = $"Permissions.Permission.Create,{Administrator}";
            public const string Edit = $"Permissions.Permission.Edit,{Administrator}";
            public const string Delete = $"Permissions.Permission.Delete,{Administrator}";
        }
        public static class PermissionDetails
        {
            public const string View = $"Permissions.PermissionDetails.View,{Administrator}";
            public const string Create = $"Permissions.PermissionDetails.Create,{Administrator}";
            public const string Edit = $"Permissions.PermissionDetails.Edit,{Administrator}";
            public const string Delete = $"Permissions.PermissionDetails.Delete,{Administrator}";
        }
        public static class Promotion
        {
            public const string View = $"Permissions.Promotion.View,{Administrator}";
            public const string Create = $"Permissions.Promotion.Create,{Administrator}";
            public const string Edit = $"Permissions.Promotion.Edit,{Administrator}";
            public const string Delete = $"Permissions.Promotion.Delete,{Administrator}";
        }
        public static class RegulationCustomer
        {
            public const string View = $"Permissions.RegulationCustomer.View,{Administrator}";
            public const string Create = $"Permissions.RegulationCustomer.Create,{Administrator}";
            public const string Edit = $"Permissions.RegulationCustomer.Edit,{Administrator}";
            public const string Delete = $"Permissions.RegulationCustomer.Delete,{Administrator}";
        }
        public static class RegulationInjection
        {
            public const string View = $"Permissions.RegulationInjection.View,{Administrator}";
            public const string Create = $"Permissions.RegulationInjection.Create,{Administrator}";
            public const string Edit = $"Permissions.RegulationInjection.Edit,{Administrator}";
            public const string Delete = $"Permissions.RegulationInjection.Delete,{Administrator}";
        }
        public static class ScreeningExamination
        {
            public const string View = $"Permissions.ScreeningExamination.View,{Administrator}";
            public const string Create = $"Permissions.ScreeningExamination.Create,{Administrator}";
            public const string Edit = $"Permissions.ScreeningExamination.Edit,{Administrator}";
            public const string Delete = $"Permissions.ScreeningExamination.Delete,{Administrator}";
        }
        public static class Shipment
        {
            public const string View = $"Permissions.Shipment.View,{Administrator}";
            public const string Create = $"Permissions.Shipment.Create,{Administrator}";
            public const string Edit = $"Permissions.Shipment.Edit,{Administrator}";
            public const string Delete = $"Permissions.Shipment.Delete,{Administrator}";
        }
        public static class Staff
        {
            public const string View = $"Permissions.Staff.View,{Administrator}";
            public const string Create = $"Permissions.Staff.Create,{Administrator}";
            public const string Edit = $"Permissions.Staff.Edit,{Administrator}";
            public const string Delete = $"Permissions.Staff.Delete,{Administrator}";
        }
        public static class Supplier
        {
            public const string View = $"Permissions.Supplier.View,{Administrator}";
            public const string Create = $"Permissions.Supplier.Create,{Administrator}";
            public const string Edit = $"Permissions.Supplier.Edit,{Administrator}";
            public const string Delete = $"Permissions.Supplier.Delete,{Administrator}";
        }
        public static class TypeOfVaccine
        {
            public const string View = $"Permissions.TypeOfVaccine.View,{Administrator}";
            public const string Create = $"Permissions.TypeOfVaccine.Create,{Administrator}";
            public const string Edit = $"Permissions.TypeOfVaccine.Edit,{Administrator}";
            public const string Delete = $"Permissions.TypeOfVaccine.Delete,{Administrator}";
        }
        public static class Upload
        {
            public const string View = $"Permissions.Upload.View,{Administrator}";
            public const string Create = $"Permissions.Upload.Create,{Administrator}";
            public const string Edit = $"Permissions.Upload.Edit,{Administrator}";
            public const string Delete = $"Permissions.Upload.Delete,{Administrator}";
        }

        public static class Vaccine
        {
            public const string View = $"Permissions.Vaccine.View,{Administrator}";
            public const string Create = $"Permissions.Vaccine.Create,{Administrator}";
            public const string Edit = $"Permissions.Vaccine.Edit,{Administrator}";
            public const string Delete = $"Permissions.Vaccine.Delete,{Administrator}";
        }
        public static class VaccinePackage
        {
            public const string View = $"Permissions.VaccinePackage.View,{Administrator}";
            public const string Create = $"Permissions.VaccinePackage.Create,{Administrator}";
            public const string Edit = $"Permissions.VaccinePackage.Edit,{Administrator}";
            public const string Delete = $"Permissions.VaccinePackage.Delete,{Administrator}";
        }
        public static class VaccinePackageDetail
        {
            public const string View = $"Permissions.VaccinePackageDetail.View,{Administrator}";
            public const string Create = $"Permissions.VaccinePackageDetail.Create,{Administrator}";
            public const string Edit = $"Permissions.VaccinePackageDetail.Edit,{Administrator}";
            public const string Delete = $"Permissions.VaccinePackageDetail.Delete,{Administrator}";
        }
        public static class VaccinePrice
        {
            public const string View = $"Permissions.VaccinePrice.View,{Administrator}";
            public const string Create = $"Permissions.VaccinePrice.Create,{Administrator}";
            public const string Edit = $"Permissions.VaccinePrice.Edit,{Administrator}";
            public const string Delete = $"Permissions.VaccinePrice.Delete,{Administrator}";
        }
        public static class InjectionSchedule
        {
            public const string View = $"Permissions.InjectionSchedule.View,{Administrator}";
            public const string Create = $"Permissions.InjectionSchedule.Create,{Administrator}";
            public const string Edit = $"Permissions.InjectionSchedule.Edit,{Administrator}";
            public const string Delete = $"Permissions.InjectionSchedule.Delete,{Administrator}";
        }
        public static class InjectionScheduleDetail
        {
            public const string View = $"Permissions.InjectionScheduleDetail.View,{Administrator}";
            public const string Create = $"Permissions.InjectionScheduleDetail.Create,{Administrator}";
            public const string Edit = $"Permissions.InjectionScheduleDetail.Edit,{Administrator}";
            public const string Delete = $"Permissions.InjectionScheduleDetail.Delete,{Administrator}";
        }
        public static class InjectionIncident
        {
            public const string View = $"Permissions.InjectionIncident.View,{Administrator}";
            public const string Create = $"Permissions.InjectionIncident.Create,{Administrator}";
            public const string Edit = $"Permissions.InjectionIncident.Edit,{Administrator}";
            public const string Delete = $"Permissions.InjectionIncident.Delete,{Administrator}";
        }

        public static class Notification
        {
            public const string View = $"Permissions.Notification.View,{Administrator}";
            public const string Create = $"Permissions.Notification.Create,{Administrator}";
            public const string Edit = $"Permissions.Notification.Edit,{Administrator}";
            public const string Delete = $"Permissions.Notification.Delete,{Administrator}";
        }
    }
}
