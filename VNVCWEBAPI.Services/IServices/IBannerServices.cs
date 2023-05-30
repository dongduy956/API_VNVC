using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IBannerServices
    {
        IQueryable<BannerModel> GetBanners();
        Task<BannerModel> GetBannerAsync(int id);
        Task<bool> InsertBanner(BannerModel bannerModel);
        Task<bool> UpdateBannerAsync(int id,BannerModel bannerModel);
        Task<bool> DeleteBannerAsync(int id);
        Task<bool> DeleteBannerRangeAsync(int[] ids);
    }
}
