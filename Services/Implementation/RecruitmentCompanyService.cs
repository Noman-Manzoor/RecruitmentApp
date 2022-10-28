using AutoMapper;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;
using Services.BusinessModels.UpdateModel;
using Services.Interface;

namespace Services.Implementation
{
    public class RecruitmentCompanyService : IRecruitmentCompanyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RecruitmentCompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddRecruitmentCompany(RecruitmentCompanyRequestModel request)
        {
            var newRecruitmentCompany = new RecruitmentCompany()
            {
                Name = request.Name,
                CompanyType = request.CompanyType,
                ContactNumber = request.ContactNumber,
                Description = request.Description,
                Email = request.Email,
                Address = request.Address,
                Status = request.Status,
                IsDeleted = false,
                CreatedBy = String.Empty,
                CreatedDate = DateTime.Now,
                UpdatedBy = String.Empty
            };

            this.unitOfWork.Repository<RecruitmentCompany>().Add(newRecruitmentCompany);
            await this.unitOfWork.SaveChangesAsync();

            var personalInformation = new PersonalInformation()
            {
                RecruitmentCompanyId = newRecruitmentCompany.RecruitmentCompanyId,
                Name = request.Name,
                ContactNumbers = request.ContactNumber,
                Address = request.Address,
                Email = request.Email,
                IsDeleted = newRecruitmentCompany.IsDeleted
            };
            this.unitOfWork.Repository<PersonalInformation>().Add(personalInformation);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRecruitmentCompany(RecruitmentCompanyUpdateRequestModel request)
        {
            var existingCompany = await this.unitOfWork.Repository<RecruitmentCompany>().FindAsync(x => x.RecruitmentCompanyId == request.RecruitmentCompanyId && x.IsDeleted != true);
            if (existingCompany != null)
            {
                existingCompany.Name = request.Name;
                existingCompany.Status = request.Status;
                existingCompany.Address = request.Address;
                existingCompany.CompanyType = request.CompanyType;
                existingCompany.ContactNumber = request.ContactNumber;
                existingCompany.Description = request.Description;
                existingCompany.Email = request.Email;

                this.unitOfWork.Repository<RecruitmentCompany>().Update(existingCompany);
                await this.unitOfWork.SaveChangesAsync();
                var personalInfo = await this.unitOfWork.Repository<PersonalInformation>().FindAllAsync(x => x.RecruitmentCompanyId == request.RecruitmentCompanyId && x.IsDeleted != true);
                if (personalInfo != null)
                {
                    foreach (var info in personalInfo)
                    {
                        info.Name = request.Name;
                        info.ContactNumbers = request.ContactNumber;
                        info.Address = request.Address;
                        info.Email = request.Email;

                        this.unitOfWork.Repository<PersonalInformation>().Update(info);
                        await this.unitOfWork.SaveChangesAsync();
                    }
                    return true;
                }
            };
            return false;
        }
        public async Task<List<RecruitmentCompanyResponseModel>> GetAllRecruitmentCompanies()
        {
            var companyResponseList = new List<RecruitmentCompanyResponseModel>();
            var companies = await this.unitOfWork.Repository<RecruitmentCompany>().FindAllAsync(x => x.IsDeleted != true);
            if (companies.Any())
            {
                var recruitmentCompanyResponse = new RecruitmentCompanyResponseModel();
                foreach (var item in companies)
                {
                    recruitmentCompanyResponse = this.mapper.Map<RecruitmentCompanyResponseModel>(item);
                    companyResponseList.Add(recruitmentCompanyResponse);
                }
            }
            return companyResponseList;
        }

        public async Task<RecruitmentCompanyResponseModel> GetRecruitmentCompanyById(int? id)
        {
            var companyResponse = new RecruitmentCompanyResponseModel();
            var company = await this.unitOfWork.Repository<RecruitmentCompany>().FindAsync(x => x.RecruitmentCompanyId == id && x.IsDeleted != true);
            if (company != null)
            {
                companyResponse = this.mapper.Map<RecruitmentCompanyResponseModel>(company);
            }
            return companyResponse;
        }
        public async Task<bool> DeleteRecruitmentCompany(int? id)
        {
            var companyExist = await this.unitOfWork.Repository<RecruitmentCompany>().FindAsync(x => x.RecruitmentCompanyId == id && x.IsDeleted != true);
            if (companyExist != null)
            {
                companyExist.IsDeleted = true;
                this.unitOfWork.Repository<RecruitmentCompany>().Update(companyExist);
                await this.unitOfWork.SaveChangesAsync();

                var personalInfo = await this.unitOfWork.Repository<PersonalInformation>().FindAllAsync(x => x.RecruitmentCompanyId == id && x.IsDeleted != true);
                if (personalInfo.Any())
                {
                    foreach (var item in personalInfo)
                    {
                        item.IsDeleted = true;

                        this.unitOfWork.Repository<PersonalInformation>().Update(item);
                        await this.unitOfWork.SaveChangesAsync();
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
