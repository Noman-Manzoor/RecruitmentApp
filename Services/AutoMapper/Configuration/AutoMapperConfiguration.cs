using AutoMapper;
using Services.BusinessModels.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.BusinessModels.Response;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;

namespace Services.AutoMapper.Configuration
{
    /// <summary>
    /// Auto Mapper Configuration
    /// </summary>
    public class AutoMapperConfiguration : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConfiguration"/> class.
        /// </summary>
        public AutoMapperConfiguration()
        {
            var entityAssemply = typeof(ProfileManagement).Assembly;
            var assemplyModelNamespace = "Domain.BusinessModels.{0}Model";
            entityAssemply.ExportedTypes.ToList().ForEach(s =>
            {
                var ModelName = Type.GetType(string.Format(assemplyModelNamespace, s.Name));
                if (ModelName != null)
                {
                    this.CreateMap(s, ModelName).ReverseMap();
                }
            });

            //// custom mapping will be here
            //// like you want to map userclass to pasword class etc
            this.CreateMap<ProfileManagementRequestModel, ProfileManagement>().ReverseMap();
            this.CreateMap<ProfileManagementResponseModel, ProfileManagement>().ReverseMap();
            this.CreateMap<RecruiterResponseModel, Recruiter>().ReverseMap();
            this.CreateMap<RecruiterRequestModel, Recruiter>().ReverseMap();
            this.CreateMap<CompanyRequestModel, Company>().ReverseMap();
            this.CreateMap<CompanyResponseModel, Company>().ReverseMap();
            this.CreateMap<RecruitmentCompanyRequestModel, RecruitmentCompany>().ReverseMap();
            this.CreateMap<RecruitmentCompanyResponseModel, RecruitmentCompany>().ReverseMap();
            this.CreateMap<LeadManagmentRequestModel, RecruitmentCompany>().ReverseMap();
            this.CreateMap<LeadManagmentResponseModel, RecruitmentCompany>().ReverseMap();

        }
    }
}
