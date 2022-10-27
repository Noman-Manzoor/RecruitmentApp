using AutoMapper;
using Domain.BusinessModels.Request;
using Domain.BusinessModels.Response;
using Domain.BusinessModels.Update;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System.Data;


namespace Services.Implementation
{
    public class ProfileManagementService : IProfileManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProfileManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddProfileAsync(ProfileManagementRequestModel request)
        {
            var newProfile = new ProfileManagement
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                VisaStatus = request.VisaStatus,
                Documents = request.Documents,
                DrivingLiscenceNo = request.DrivingLiscenceNo,
                JobExperience = request.JobExperience,
                MaritalStauts = request.MaritalStauts,
                BornInUSA = request.BornInUSA,
                MoveToUSA = request.MoveInUSA,
                IsDeleted = false,
                SkillSet = request.SkillSet,
                StatusInUSA = request.StatusInUSA,
                IsVaccinated = request.IsVaccinated,
                CreatedBy = string.Empty,  // implement through identity
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = string.Empty,
                UpdatedDate = null
            };
            this.unitOfWork.Repository<ProfileManagement>().Add(newProfile);
            await this.unitOfWork.SaveChangesAsync();

            var newFamilyDetail = new FamilyDetail()
            {
                ProfileId = newProfile.Id,
                MotherName = request.MotherName,
                FatherName = request.FatherName,
                HavingSiblings = request.HavingSiblings,
                NoOfBrothers = request.NoOfBrothers,
                NoOfSisters = request.NoOfSisters,
                NoOfKids = request.NoOfKids,
                IsDeleted = false
            };

            this.unitOfWork.Repository<FamilyDetail>().Add(newFamilyDetail);
            await this.unitOfWork.SaveChangesAsync();

            var personalInformation = new PersonalInformation()
            {
                ProfileId = newProfile.Id,
                Name = request.FirstName + " " + request.LastName,
                ContactNumbers = request.ContactNumber,
                Address = request.Address,
                Email = request.Email,
                IsDeleted = newProfile.IsDeleted
            };
            this.unitOfWork.Repository<PersonalInformation>().Add(personalInformation);
            await this.unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateProfileAsync(ProfileManagementUpdateRequestModel request)
        {
            var existingProfile = await this.unitOfWork.Repository<ProfileManagement>().FindAsync(x => x.Id == request.Id && x.IsDeleted != true);
            if (existingProfile != null)
            {
                var existingFamilyDetail = await this.unitOfWork.Repository<FamilyDetail>().FindAsync(x => x.Id == existingProfile.Id && x.IsDeleted != true);
                if (existingFamilyDetail != null)
                {
                    existingFamilyDetail.MotherName = request.MotherName;
                    existingFamilyDetail.FatherName = request.FatherName;
                    existingFamilyDetail.HavingSiblings = request.HavingSiblings;
                    existingFamilyDetail.NoOfBrothers = request.NoOfBrothers;
                    existingFamilyDetail.NoOfSisters = request.NoOfSisters;
                    existingFamilyDetail.NoOfKids = request.NoOfKids;

                    this.unitOfWork.Repository<FamilyDetail>().Update(existingFamilyDetail);
                    await this.unitOfWork.SaveChangesAsync();
                }

                existingProfile.FirstName = request.FirstName;
                existingProfile.MiddleName = request.MiddleName;
                existingProfile.LastName = request.LastName;
                existingProfile.ContactNumber = request.ContactNumber;
                existingProfile.Email = request.Email;
                existingProfile.Address = request.Address;
                existingProfile.DateOfBirth = request.DateOfBirth;
                existingProfile.VisaStatus = request.VisaStatus;
                existingProfile.Documents = request.Documents;
                existingProfile.DrivingLiscenceNo = request.DrivingLiscenceNo;
                existingProfile.JobExperience = request.JobExperience;
                existingProfile.MaritalStauts = request.MaritalStauts;
                existingProfile.BornInUSA = request.BornInUSA;
                existingProfile.MoveToUSA = request.MoveToUSA;
                existingProfile.SkillSet = request.SkillSet;
                existingProfile.StatusInUSA = request.StatusInUSA;
                existingProfile.IsVaccinated = request.IsVaccinated;

                this.unitOfWork.Repository<ProfileManagement>().Update(existingProfile);
                await this.unitOfWork.SaveChangesAsync();

                var personalInfo = await this.unitOfWork.Repository<PersonalInformation>().FindAllAsync(x => x.ProfileId == request.Id && x.IsDeleted != true);
                if (personalInfo != null)
                {
                    foreach (var info in personalInfo)
                    {
                        info.Name = request.FirstName + " " + request.LastName;
                        info.ContactNumbers = request.ContactNumber;
                        info.Address = request.Address;
                        info.Email = request.Email;

                        this.unitOfWork.Repository<PersonalInformation>().Update(info);
                        await this.unitOfWork.SaveChangesAsync();
                    }
                    return true;
                }
            }
            return false;
        }
        public async Task<List<ProfileManagementResponseModel>> GetAllProfileAsync()
        {
            var profileResponseList = new List<ProfileManagementResponseModel>();
            var profile = await this.unitOfWork.Repository<ProfileManagement>().FindAllAsync(x => x.IsDeleted != true);
            if (profile.Any())
            {
                foreach (var item in profile)
                {
                    var familyDetail = await this.unitOfWork.Repository<FamilyDetail>().FindAsync(x => x.ProfileId == item.Id && x.IsDeleted == false);
                    if (familyDetail != null)
                    {
                        var profileResponse = new ProfileManagementResponseModel()
                        {
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            Email = item.Email,
                            ContactNumber = item.ContactNumber,
                            DateOfBirth = item.DateOfBirth,
                            VisaStatus = item.VisaStatus,
                            DrivingLiscenceNo = item.DrivingLiscenceNo,
                            BornInUSA = item.BornInUSA,
                            MoveToUSA = item.MoveToUSA,
                            StatusInUSA = item.StatusInUSA,
                            MaritalStauts = item.MaritalStauts,
                            Documents = item.Documents,
                            SkillSet = item.SkillSet,
                            JobExperience = item.JobExperience,
                            IsVaccinated = item.IsVaccinated,
                            FatherName = familyDetail.FatherName,
                            MotherName = familyDetail.MotherName,
                            HavingSiblings = familyDetail.HavingSiblings,
                            NoOfBrothers = familyDetail.NoOfBrothers,
                            NoOfSisters = familyDetail.NoOfSisters,
                            NoOfKids = familyDetail.NoOfKids
                        };
                        profileResponseList.Add(profileResponse);
                    }
                }
            }
            return profileResponseList;
        }

        public async Task<ProfileManagementResponseModel?> GetProfileByIdAsync(int? id)
        {
            var profile = await this.unitOfWork.Repository<ProfileManagement>().FindAsync(x => x.Id == id);
            if (profile != null && profile.IsDeleted == false)
            {
                var familyDetail = await this.unitOfWork.Repository<FamilyDetail>().FindAsync(x => x.ProfileId == id && x.IsDeleted == false);
                if (familyDetail != null)
                {
                    var profileResponse = new ProfileManagementResponseModel()
                    {
                        FirstName = profile.FirstName,
                        MiddleName = profile.MiddleName,
                        LastName = profile.LastName,
                        Email = profile.Email,
                        ContactNumber = profile.ContactNumber,
                        DateOfBirth = profile.DateOfBirth,
                        VisaStatus = profile.VisaStatus,
                        DrivingLiscenceNo = profile.DrivingLiscenceNo,
                        BornInUSA = profile.BornInUSA,
                        MoveToUSA = profile.MoveToUSA,
                        StatusInUSA = profile.StatusInUSA,
                        MaritalStauts = profile.MaritalStauts,
                        Documents = profile.Documents,
                        SkillSet = profile.SkillSet,
                        JobExperience = profile.JobExperience,
                        IsVaccinated = profile.IsVaccinated,
                        FatherName = familyDetail.FatherName,
                        MotherName = familyDetail.MotherName,
                        HavingSiblings = familyDetail.HavingSiblings,
                        NoOfBrothers = familyDetail.NoOfBrothers,
                        NoOfSisters = familyDetail.NoOfSisters,
                        NoOfKids = familyDetail.NoOfKids
                    };
                    return profileResponse;
                }
            }
            return null;
        }
        public async Task<ProfileManagementResponseModel?> GetProfileByNameAsync(string name)
        {

            var profile = await this.unitOfWork.Repository<ProfileManagement>().FindAsync(x => x.FirstName == name);
            if (profile != null && profile.IsDeleted == false)
            {
                var familyDetail = await this.unitOfWork.Repository<FamilyDetail>().FindAsync(x => x.ProfileId == profile.Id && x.IsDeleted == false);
                if (familyDetail != null)
                {
                    var profileResponse = new ProfileManagementResponseModel()
                    {
                        FirstName = profile.FirstName,
                        MiddleName = profile.MiddleName,
                        LastName = profile.LastName,
                        Email = profile.Email,
                        ContactNumber = profile.ContactNumber,
                        DateOfBirth = profile.DateOfBirth,
                        VisaStatus = profile.VisaStatus,
                        DrivingLiscenceNo = profile.DrivingLiscenceNo,
                        BornInUSA = profile.BornInUSA,
                        MoveToUSA = profile.MoveToUSA,
                        StatusInUSA = profile.StatusInUSA,
                        MaritalStauts = profile.MaritalStauts,
                        Documents = profile.Documents,
                        SkillSet = profile.SkillSet,
                        JobExperience = profile.JobExperience,
                        IsVaccinated = profile.IsVaccinated,
                        FatherName = familyDetail.FatherName,
                        MotherName = familyDetail.MotherName,
                        HavingSiblings = familyDetail.HavingSiblings,
                        NoOfBrothers = familyDetail.NoOfBrothers,
                        NoOfSisters = familyDetail.NoOfSisters,
                        NoOfKids = familyDetail.NoOfKids
                    };
                    return profileResponse;
                }
            }
            return null;
        }
        public async Task<bool> DeleteProfileAsync(int? id)
        {
            var profile = await this.unitOfWork.Repository<ProfileManagement>().FindAsync(x => x.Id == id);
            if (profile != null)
            {
                var familyDetail = await this.unitOfWork.Repository<FamilyDetail>().FindAsync(x => x.ProfileId == id);
                if (familyDetail != null)
                {
                    familyDetail.IsDeleted = true;
                    this.unitOfWork.Repository<FamilyDetail>().Update(familyDetail);

                    await this.unitOfWork.SaveChangesAsync();
                }

                profile.IsDeleted = true;
                this.unitOfWork.Repository<ProfileManagement>().Update(profile);
                await this.unitOfWork.SaveChangesAsync();

                var personalInfo = await this.unitOfWork.Repository<PersonalInformation>().FindAllAsync(x => x.ProfileId == id && x.IsDeleted != true);
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
