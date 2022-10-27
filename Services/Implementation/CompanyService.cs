﻿using AutoMapper;
using Domain.BusinessModels.Request;
using Domain.BusinessModels.RequestModel;
using Domain.BusinessModels.Response;
using Domain.BusinessModels.ResponseModel;
using Domain.BusinessModels.Update;
using Domain.BusinessModels.UpdateModel;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly DMRecruitmentContext _db;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, DMRecruitmentContext db)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _db = db;
        }

        public async Task<bool> AddCompanyAsync(CompanyRequestModel request)
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

        public async Task<bool> UpdateCompanyAsync(CompanyUpdateRequestModel request)
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
        public async Task<List<CompanyResponseModel>> GetAllCompaniesAsync()
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

        public async Task<CompanyResponseModel> GetCompanyByIdAsync(int? id)
        {
            var companyResponse = new CompanyResponseModel();
            var company = await this.unitOfWork.Repository<Company>().FindAsync(x => x.CompanyId == id && x.IsDeleted != true);
            if (company != null)
            {
                companyResponse = this.mapper.Map<CompanyResponseModel>(company);
            }
            return companyResponse;
        }
        public async Task<bool> DeleteCompanyAsync(int? id)
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
