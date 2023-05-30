using Microsoft.AspNetCore.Authorization;
using Twilio.Clients;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.Services;

namespace VNVCWEBAPI.Helpers
{
    public static class IServicesCollectionExtensions
    {
        public static void AddServicesRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerTypeServices, CustomerTypeServices>();
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<IAdditionalCustomerInformationServices, AdditionalCustomerInformationServices>();
            services.AddScoped<IPermissionServices, PermissionServices>();
            services.AddScoped<ISupplierServices, SupplierServices>();
            services.AddScoped<ITypeOfVaccineServices, TypeOfVaccineServices>();
            services.AddScoped<IShipmentServices, ShipmentServices>();
            services.AddScoped<IPaymentMethodServices, PaymentMethodServices>();
            services.AddScoped<IVaccineServices, VaccineServices>();
            services.AddScoped<IVaccinePackageServices, VaccinePackageServices>();
            services.AddScoped<IVaccinePackageDetailServices, VaccinePackageDetailServices>();
            services.AddScoped<IVaccinePriceServices, VaccinePriceServices>();
            services.AddScoped<IStaffServices, StaffServices>();
            services.AddScoped<IRegulationCustomerServices, RegulationCustomerServices>();
            services.AddScoped<IRegulationInjectionServices, RegulationInjectionServices>();
            services.AddScoped<IPromotionServices, PromotionServices>();
            services.AddScoped<IScreeningExaminationServices, ScreeningExaminationServices>();
            services.AddScoped<ICustomerRankServices, CustomerRankServices>();
            services.AddScoped<IConditionPromotionServices, ConditionPromotionServices>();
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderDetailServices, OrderDetailServices>();
            services.AddScoped<IEntrySlipServices, EntrySlipServices>();
            services.AddScoped<IEntrySlipDetailsServices, EntrySlipDetailsServices>();
            services.AddScoped<ILoginSessionServices, LoginSessionServices>();
            services.AddScoped<IPayServices, PayServices>();
            services.AddScoped<IPayDetailServices, PayDetailServices>();
            services.AddScoped<ICustomerRankDetailsServices, CustomerRankDetailsServices>();
            services.AddScoped<IInjectionIncidentServices, InjectionIncidentServices>();
            services.AddScoped<IInjectionScheduleServices, InjectionScheduleServices>();
            services.AddScoped<IInjectionScheduleDetailServices, InjectionScheduleDetailServices>();
            services.AddScoped<IPermissionDetailsServices, PermissionDetailsServices>();
            services.AddScoped<IJWTServices, JWTServices>();
            services.AddScoped<IStatisticalServices, StatisticalServices>();
            services.AddScoped<IBannerServices, BannerServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IMailServices, MailServices>();
            services.AddScoped<INotificationServices, NotificationServices>();
            services.AddHttpClient<ITwilioRestClient, TwilioClient>();
            services.AddHttpContextAccessor();
        }
    }
}
