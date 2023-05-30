using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Enum;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class AdditionalCustomerInformationServices : IAdditionalCustomerInformationServices
    {
        private readonly IRepository<AdditionalCustomerInformation> repository;
        public AdditionalCustomerInformationServices(IRepository<AdditionalCustomerInformation> repository)
        {
            this.repository = repository;
        }
        public async Task<bool> DeleteAdditionalCustomerInformationAsync(int customerId)
        {

            var additionalCustomer = repository
                                     .Where(x => x.CustomerId == customerId)
                                     .FirstOrDefault();
            if (additionalCustomer != null)
                return await repository.Delete(additionalCustomer);
            return false;
        }

        public IQueryable<AdditionalCustomerInformationModel> GetAdditionalCustomerInformations()
        {
            return repository
                .GetAll(SelectEnum.Select.NONTRASH)
                .Select(x => new AdditionalCustomerInformationModel
                {
                    CustomerId = x.CustomerId,
                    FatherFullName = x.FatherFullName,
                    FatherPhoneNumber = x.FatherPhoneNumber,
                    HeightAtBirth = x.HeightAtBirth,
                    MotherFullName = x.MotherFullName,
                    MotherPhoneNumber = x.MotherPhoneNumber,
                    WeightAtBirth = x.WeightAtBirth,
                    Created = x.Created,
                    Id = x.Id
                });

        }

        public async Task<AdditionalCustomerInformationModel> GetAdditionalCustomerInformationAsync(int id)
        {
            var additionalCustomer = await repository.GetAsync(id);
            var model = new AdditionalCustomerInformationModel
            {
                CustomerId = additionalCustomer.CustomerId,
                FatherFullName = additionalCustomer.FatherFullName,
                FatherPhoneNumber = additionalCustomer.FatherPhoneNumber,
                HeightAtBirth = additionalCustomer.HeightAtBirth,
                MotherFullName = additionalCustomer.MotherFullName,
                MotherPhoneNumber = additionalCustomer.MotherPhoneNumber,
                WeightAtBirth = additionalCustomer.WeightAtBirth,
                Created = additionalCustomer.Created,
                Id = additionalCustomer.Id,
            };
            return model;
        }

        public async Task<bool> InsertAdditionalCustomerInformationAsync(AdditionalCustomerInformationModel model)
        {
            var additionalCustomer = new AdditionalCustomerInformation
            {
                CustomerId = model.CustomerId,
                FatherFullName = model.FatherFullName,
                FatherPhoneNumber = model.FatherPhoneNumber,
                HeightAtBirth = model.HeightAtBirth,
                MotherFullName = model.MotherFullName,
                MotherPhoneNumber = model.MotherPhoneNumber,
                WeightAtBirth = model.WeightAtBirth
            };

            var result = await repository.InsertAsync(additionalCustomer);
            model.Id = additionalCustomer.Id;
            model.Created = additionalCustomer.Created;
            return result;
        }

        public async Task<bool> UpdateAdditionalCustomerInformationAsync(int customerId, AdditionalCustomerInformationModel model)
        {
            var additionalCustomer = repository
                                    .Where(x => x.CustomerId == customerId)
                                    .FirstOrDefault();
            if (additionalCustomer == null)
            {
                var data = await InsertAdditionalCustomerInformationAsync(model);
                return true;
            }
            else
            {
                additionalCustomer.FatherFullName = model.FatherFullName;
                additionalCustomer.FatherPhoneNumber = model.FatherPhoneNumber;
                additionalCustomer.HeightAtBirth = model.HeightAtBirth;
                additionalCustomer.MotherFullName = model.MotherFullName;
                additionalCustomer.MotherPhoneNumber = model.MotherPhoneNumber;
                additionalCustomer.WeightAtBirth = model.WeightAtBirth;

                return await repository.UpdateAsync(additionalCustomer);
            }
        }

        public IQueryable<AdditionalCustomerInformationModel> SearchAdditionalCustomerInformations(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(SelectEnum.Select.NONTRASH)
                .Where(x => x.FatherFullName.ToLower().Contains(q) ||
                            x.FatherPhoneNumber.ToLower().Equals(q) ||
                            x.HeightAtBirth.ToString().Equals(q) ||
                            x.WeightAtBirth.ToString().Equals(q) ||
                            x.MotherFullName.ToLower().Contains(q) ||
                            x.MotherPhoneNumber.Equals(q))
                .Select(model => new AdditionalCustomerInformationModel
                {
                    CustomerId = model.CustomerId,
                    FatherFullName = model.FatherFullName,
                    FatherPhoneNumber = model.FatherPhoneNumber,
                    HeightAtBirth = model.HeightAtBirth,
                    MotherFullName = model.MotherFullName,
                    MotherPhoneNumber = model.MotherPhoneNumber,
                    WeightAtBirth = model.WeightAtBirth,
                    Id = model.Id,
                    Created = model.Created
                });
            return results;
        }

        public IList<AdditionalCustomerInformationModel> GetAdditionalCustomerInformationByIds(int[] ids)
        {
            var additionalCustomerInformationModels = new List<AdditionalCustomerInformationModel>();
            foreach (var id in ids)
            {
                var model = repository.Where(x => x.CustomerId == id).FirstOrDefault();
                if (model != null)
                {
                    additionalCustomerInformationModels.Add(new AdditionalCustomerInformationModel
                    {
                        CustomerId = model.CustomerId,
                        FatherFullName = model.FatherFullName,
                        FatherPhoneNumber = model.FatherPhoneNumber,
                        HeightAtBirth = model.HeightAtBirth,
                        MotherFullName = model.MotherFullName,
                        MotherPhoneNumber = model.MotherPhoneNumber,
                        WeightAtBirth = model.WeightAtBirth,
                        Created = model.Created,
                        Id = model.Id
                    });
                }
            }
            return additionalCustomerInformationModels;
        }

        public async Task<bool> InsertAdditionalCustomerInformationsRange(IList<AdditionalCustomerInformationModel> additionalCustomerInformationModels)
        {
            var additionalCustomerInformations = new List<AdditionalCustomerInformation>();
            foreach (var model in additionalCustomerInformationModels)
            {
                additionalCustomerInformations.Add(new AdditionalCustomerInformation
                {
                    CustomerId = model.CustomerId,
                    FatherFullName = model.FatherFullName,
                    FatherPhoneNumber = model.FatherPhoneNumber,
                    HeightAtBirth = model.HeightAtBirth,
                    MotherFullName = model.MotherFullName,
                    MotherPhoneNumber = model.MotherPhoneNumber,
                    WeightAtBirth = model.WeightAtBirth
                });
            }
            var result = await repository.InsertRangeAsync(additionalCustomerInformations);
            for (int i = 0; i < additionalCustomerInformations.Count; i++)
            {
                additionalCustomerInformationModels[i].Id = additionalCustomerInformations[i].Id;
                additionalCustomerInformationModels[i].Created = additionalCustomerInformations[i].Created;
            }
            return result;
        }

        public async Task<bool> UpdateAdditionalCustomerInformationsRange(IList<AdditionalCustomerInformationModel> additionalCustomerInformationModels)
        {
            var additionalCustomerInformations = new List<AdditionalCustomerInformation>();
            foreach (var model in additionalCustomerInformationModels)
            {
                var additionalCustomer = await repository.GetAsync(model.Id);
                if (additionalCustomer != null)
                {
                    additionalCustomer.FatherFullName = model.FatherFullName;
                    additionalCustomer.FatherPhoneNumber = model.FatherPhoneNumber;
                    additionalCustomer.HeightAtBirth = model.HeightAtBirth;
                    additionalCustomer.MotherFullName = model.MotherFullName;
                    additionalCustomer.MotherPhoneNumber = model.MotherPhoneNumber;
                    additionalCustomer.WeightAtBirth = model.WeightAtBirth;
                    additionalCustomerInformations.Add(additionalCustomer);
                }
            }
            return await repository.UpdateRangeAsync(additionalCustomerInformations);
        }
    }
}
