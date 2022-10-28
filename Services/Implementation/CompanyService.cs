using AutoMapper;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;
using Services.BusinessModels.UpdateModel;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Services.Interface;


namespace Services.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddCompany(CompanyRequestModel request)
        {
            var newCompany = new Company()
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                ContactNumber = request.ContactNumber,
                BusinessType = request.BusinessType,
                CompanyType = request.CompanyType,
                NatureOfWork = request.NatureOfWork,
                Crediblity = request.Crediblity,
                Description = request.Description,
                IsDeleted = false,
                CreatedBy = string.Empty,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = string.Empty,
                UpdatedDate = null,
            };

            this.unitOfWork.Repository<Company>().Add(newCompany);
            await this.unitOfWork.SaveChangesAsync();


            var personalInformation = new PersonalInformation()
            {
                CompanyId = newCompany.CompanyId,
                Name = request.Name,
                ContactNumbers = request.ContactNumber,
                Address = request.Address,
                Email = request.Email,
                IsDeleted = newCompany.IsDeleted
            };
            this.unitOfWork.Repository<PersonalInformation>().Add(personalInformation);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCompany(CompanyUpdateRequestModel request)
        {
            var existingCompany = await this.unitOfWork.Repository<Company>().FindAsync(x => x.CompanyId == request.Id && x.IsDeleted != true);
            if (existingCompany != null)
            {
                existingCompany.Name = request.Name;
                existingCompany.Address = request.Address;
                existingCompany.BusinessType = request.BusinessType;
                existingCompany.CompanyType = request.CompanyType;
                existingCompany.ContactNumber = request.ContactNumber;
                existingCompany.Description = request.Description;
                existingCompany.Email = request.Email;
                existingCompany.Crediblity = request.Crediblity;
                existingCompany.NatureOfWork = request.NatureOfWork;

                this.unitOfWork.Repository<Company>().Update(existingCompany);
                await this.unitOfWork.SaveChangesAsync();

                var personalInfo = await this.unitOfWork.Repository<PersonalInformation>().FindAllAsync(x => x.CompanyId == request.Id && x.IsDeleted != true);
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
        public async Task<List<CompanyResponseModel>> GetAllCompanies()
        {
            var companyResponseList = new List<CompanyResponseModel>();
            var companies = await this.unitOfWork.Repository<Company>().FindAllAsync(x => x.IsDeleted == false);
            if (companies.Any())
            {
                var companyResponse = new CompanyResponseModel();
                foreach (var item in companies)
                {
                    companyResponse = this.mapper.Map<CompanyResponseModel>(item);
                    companyResponseList.Add(companyResponse);
                }
            }
            return companyResponseList;
        }

        public async Task<CompanyResponseModel> GetCompanyById(int? id)
        {
            var companyResponse = new CompanyResponseModel();
            var company = await this.unitOfWork.Repository<Company>().FindAsync(x => x.CompanyId == id && x.IsDeleted != true);
            if (company != null)
            {
                companyResponse = this.mapper.Map<CompanyResponseModel>(company);
            }
            return companyResponse;
        }
        public async Task<bool> DeleteCompany(int? id)
        {
            var companyExist = await this.unitOfWork.Repository<Company>().FindAsync(x => x.CompanyId == id && x.IsDeleted != true);
            if (companyExist != null)
            {
                companyExist.IsDeleted = true;
                this.unitOfWork.Repository<Company>().Update(companyExist);
                await this.unitOfWork.SaveChangesAsync();

                var personalInfo = await this.unitOfWork.Repository<PersonalInformation>().FindAllAsync(x => x.CompanyId == id && x.IsDeleted != true);
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
