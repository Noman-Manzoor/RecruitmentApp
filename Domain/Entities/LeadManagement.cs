using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class LeadManagement
    {
        public int Id { get; set; }
        public string? RecruiterName { get; set; }
        public int? RecruiterCompanyId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProfileId { get; set; }
        public string? Interviewee { get; set; }
        public string? Interviewer { get; set; }
        public string? InterviewLevel { get; set; }
        public string? InterviewStatus { get; set; }
        public string? TechnologyStack { get; set; }
        public string? JobDescription { get; set; }
        public decimal? HourlyRate { get; set; }
        public double? AnnaulSalary { get; set; }
        public string? JobStatus { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual RecruitmentCompany? RecruitmentCompany { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ProfileManagement? ProfileManagement { get; set; }

    }
}
