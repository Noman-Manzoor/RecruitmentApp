using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProfileManagement
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? VisaStatus { get; set; }
        public string? DrivingLiscenceNo { get; set; }
        public bool? BornInUSA { get; set; }
        public string? MoveToUSA { get; set; }
        public string? StatusInUSA { get; set; }
        public string? MaritalStauts { get; set; }
        public string? Documents { get; set; }
        public string? SkillSet { get; set; }
        public string? JobExperience { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsVaccinated { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }

        public virtual FamilyDetail? FamilyDetail { get; set; }
        public virtual ICollection<PersonalInformation>? PersonalInformation { get; internal set; }
        public virtual ICollection<LeadManagement>? LeadManagements { get; set; }


    }
}
