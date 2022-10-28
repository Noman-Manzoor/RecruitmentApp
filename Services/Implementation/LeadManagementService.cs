using AutoMapper;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;
using Services.BusinessModels.UpdateModel;
using Services.Interface;

namespace Services.Implementation
{
    public class LeadManagementService : ILeadManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LeadManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddLead(LeadManagmentRequestModel request)
        {
            var newLead = new LeadManagement()
            {
                ProfileId = request.ProfileId,
                RecruiterCompanyId = request.RecruiterCompanyId,
                CompanyId = request.CompanyId,
                RecruiterName = request.RecruiterName,
                Interviewee = request.Interviewee,
                Interviewer = request.Interviewer,
                InterviewLevel = request.InterviewLevel,
                InterviewStatus = request.InterviewStatus,
                JobDescription = request.JobDescription,
                AnnaulSalary = request.AnnaulSalary,
                HourlyRate = request.HourlyRate,
                TechnologyStack = request.TechnologyStack,
                JobStatus = request.JobStatus,
                IsDeleted = false,
                CreatedBy = string.Empty,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = string.Empty,
            };

            this.unitOfWork.Repository<LeadManagement>().Add(newLead);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLead(LeadManagmentUpdateRequestModel request)
        {
            var existingLead = await this.unitOfWork.Repository<LeadManagement>().FindAsync(x => x.Id == request.Id && x.IsDeleted != true);
            if (existingLead != null)
            {
                existingLead.ProfileId = request.ProfileId;
                existingLead.CompanyId = request.CompanyId;
                existingLead.RecruiterCompanyId = request.RecruiterCompanyId;
                existingLead.AnnaulSalary = request.AnnaulSalary;
                existingLead.HourlyRate = request.HourlyRate;
                existingLead.Interviewee = request.Interviewee;
                existingLead.Interviewer = request.Interviewer;
                existingLead.InterviewLevel = request.InterviewLevel;
                existingLead.InterviewStatus = request.InterviewStatus;
                existingLead.JobDescription = request.JobDescription;
                existingLead.RecruiterName = request.RecruiterName;
                existingLead.JobStatus = request.JobStatus;
                existingLead.TechnologyStack = request.TechnologyStack;

                this.unitOfWork.Repository<LeadManagement>().Update(existingLead);
                await this.unitOfWork.SaveChangesAsync();

                return true;
            };
            return false;
        }

        public async Task<List<LeadManagmentResponseModel>> GetAllLeads()
        {
            var leadsResponseList = new List<LeadManagmentResponseModel>();
            var leads = await this.unitOfWork.Repository<LeadManagement>().FindAllAsync(x => x.IsDeleted != true);
            if (leads.Any())
            {
                foreach (var item in leads)
                {
                    var profile = await this.unitOfWork.Repository<ProfileManagement>().FindAsync(x => x.Id == item.ProfileId);
                    var recruitmentCompany = await this.unitOfWork.Repository<RecruitmentCompany>().FindAsync(x => x.RecruitmentCompanyId == item.RecruiterCompanyId);
                    var company = await this.unitOfWork.Repository<Company>().FindAsync(x => x.CompanyId == item.CompanyId);

                    if (profile != null && company != null && recruitmentCompany != null)
                    {
                        var leadResponse = new LeadManagmentResponseModel()
                        {
                            Id = item.Id,
                            ProfileName = profile.FirstName + " " + profile.LastName,
                            RecruiterCompanyName = recruitmentCompany.Name,
                            RecruiterName = item.RecruiterName,
                            CompanyName = company.Name,
                            AnnaulSalary = item.AnnaulSalary,
                            HourlyRate = item.HourlyRate,
                            Interviewee = item.Interviewee,
                            Interviewer = item.Interviewer,
                            InterviewLevel = item.InterviewLevel,
                            InterviewStatus = item.InterviewStatus,
                            JobDescription = item.JobDescription,
                            JobStatus = item.JobStatus,
                            TechnologyStack = item.TechnologyStack
                        };

                        leadsResponseList.Add(leadResponse);
                    }
                }
            }
            return leadsResponseList;
        }

        public async Task<LeadManagmentResponseModel?> GetLeadById(int? id)
        {
            var lead = await this.unitOfWork.Repository<LeadManagement>().FindAsync(x => x.Id == id && x.IsDeleted != true);
            if (lead != null)
            {
                var profile = await this.unitOfWork.Repository<ProfileManagement>().FindAsync(x => x.Id == lead.ProfileId);
                var recruitmentCompany = await this.unitOfWork.Repository<RecruitmentCompany>().FindAsync(x => x.RecruitmentCompanyId == lead.RecruiterCompanyId);
                var company = await this.unitOfWork.Repository<Company>().FindAsync(x => x.CompanyId == lead.CompanyId);


                if (profile != null && company != null && recruitmentCompany != null)
                {
                    var leadResponse = new LeadManagmentResponseModel()
                    {
                        Id = lead.Id,
                        ProfileName = profile.FirstName + " " + profile.LastName,
                        RecruiterCompanyName = recruitmentCompany.Name,
                        RecruiterName = lead.RecruiterName,
                        CompanyName = company.Name,
                        AnnaulSalary = lead.AnnaulSalary,
                        HourlyRate = lead.HourlyRate,
                        Interviewee = lead.Interviewee,
                        Interviewer = lead.Interviewer,
                        InterviewLevel = lead.InterviewLevel,
                        InterviewStatus = lead.InterviewStatus,
                        JobDescription = lead.JobDescription,
                        JobStatus = lead.JobStatus,
                        TechnologyStack = lead.TechnologyStack
                    };
                    return leadResponse;
                }
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteLead(int? id)
        {
            var LeadExist = await this.unitOfWork.Repository<LeadManagement>().FindAsync(x => x.Id == id && x.IsDeleted != true);
            if (LeadExist != null)
            {
                LeadExist.IsDeleted = true;
                this.unitOfWork.Repository<LeadManagement>().Update(LeadExist);
                await this.unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
