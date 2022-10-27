using AutoMapper;
using Domain.BusinessModels.Request;
using Domain.BusinessModels.Response;
using Domain.BusinessModels.Update;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Interface;


namespace Services.Implementation
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RecruiterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddRecruiter(RecruiterRequestModel request)
        {
            var recruiter = new Recruiter
            {
                RecruitmentCompanyId = request.RecruitmentCompanyId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Type = request.Type,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                Status = request.Status,
                IsDeleted = false,
                CreatedBy = string.Empty,
                CreatedDate = DateTime.Now,
                UpdatedBy = string.Empty,
                UpdatedDate = null
            };

            this.unitOfWork.Repository<Recruiter>().Add(recruiter);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRecruiter(RecruiterUpdateRequestModel request)
        {
            var existingRecruiter = await this.unitOfWork.Repository<Recruiter>().FindAsync(x => x.Id == request.Id && x.IsDeleted != true);
            if (existingRecruiter != null)
            {
                existingRecruiter.RecruitmentCompanyId = request.RecruitmentCompanyId;
                existingRecruiter.FirstName = request.FirstName;
                existingRecruiter.LastName = request.LastName;
                existingRecruiter.Type = request.Type;
                existingRecruiter.ContactNumber = request.ContactNumber;
                existingRecruiter.Email = request.Email;
                existingRecruiter.Status = request.Status;

                this.unitOfWork.Repository<Recruiter>().Update(existingRecruiter);
                await this.unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<RecruiterResponseModel>> GetAllRecruiters()
        {
            var recruiterResponse = new List<RecruiterResponseModel>();
            var recruiter = await this.unitOfWork.Repository<Recruiter>().FindAllAsync(x => x.IsDeleted != true);
            if (recruiter.Any())
                recruiterResponse = this.mapper.Map<List<RecruiterResponseModel>>(recruiter);
            return recruiterResponse;
        }

        public async Task<RecruiterResponseModel> GetRecruiterById(int? id)
        {
            var recruiterResponse = new RecruiterResponseModel();
            var recruiter = await this.unitOfWork.Repository<Recruiter>().FindAsync(x => x.Id == id && x.IsDeleted != true);
            if (recruiter != null)
                recruiterResponse = this.mapper.Map<RecruiterResponseModel>(recruiter);
            return recruiterResponse;
        }
        public async Task<bool> DeleteRecruiter(int? id)
        {
            var recruiter = await this.unitOfWork.Repository<Recruiter>().FindAsync(x => x.Id == id && x.IsDeleted != true);
            if (recruiter != null)
            {
                recruiter.IsDeleted = true;
                this.unitOfWork.Repository<Recruiter>().Update(recruiter);
                await this.unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
