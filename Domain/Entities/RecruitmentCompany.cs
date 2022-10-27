using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class RecruitmentCompany
    {
        public RecruitmentCompany()
        {
            Recruiters = new HashSet<Recruiter>();
        }

        public int RecruitmentCompanyId { get; set; }
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CompanyType { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }

        public virtual ICollection<Recruiter>? Recruiters { get; set; }
        public virtual ICollection<PersonalInformation>? PersonalInformation { get; internal set; }
        public virtual ICollection<LeadManagement>? LeadManagements { get; set; }

    }
}
