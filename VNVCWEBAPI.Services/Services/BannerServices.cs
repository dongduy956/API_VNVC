using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class BannerServices : IBannerServices
    {
        private readonly IRepository<Banner> repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BannerServices(IRepository<Banner> repository, IHttpContextAccessor _httpContextAccessor)
        {
            this.repository = repository;
            this._httpContextAccessor= _httpContextAccessor;
        }

        public async Task<bool> DeleteBannerAsync(int id)
        {
            var banner = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(banner);
        }

        public async Task<bool> DeleteBannerRangeAsync(int[] ids)
        {
            var items = new List<Banner>();
            foreach (var id in ids)
            {
                var banner = await repository.GetAsync(id);
                items.Add(banner);
            }
            return await repository.DeleteFromTrashRange(items);
        }

        public async Task<BannerModel> GetBannerAsync(int id)
        {
            var banner = await repository.GetAsync(id);
            if (banner == null)
                return null;
            return new BannerModel
            {
                Created = banner.Created,
                Id = banner.Id,
                Href = banner.href,
                Image = banner.Image,
                Title = banner.Title
            };
        }

        public IQueryable<BannerModel> GetBanners()
        {
            string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            
            return repository.GetAll(Common.Enum.SelectEnum.Select.ALL)
                             .Select(x => new BannerModel
                             {
                                 Id = x.Id,
                                 Image = string.IsNullOrEmpty(x.Image)?"":$"{myHostUrl}{x.Image}",
                                 Href = x.href,
                                 Title = x.Title,
                                 Created = x.Created
                             });
        }

        public async Task<bool> InsertBanner(BannerModel bannerModel)
        {
            var banner = new Banner
            {
                href = bannerModel.Href,
                Title = bannerModel.Title,
                Image = bannerModel.Image,
            };
            return await repository.InsertAsync(banner);
        }

        public async Task<bool> UpdateBannerAsync(int id, BannerModel bannerModel)
        {
            var banner = await repository.GetAsync(id);
            banner.href = bannerModel.Href;
            banner.Title = banner.Title;
            banner.Image = banner.Image;
            return await repository.UpdateAsync(banner);
        }
    }
}
