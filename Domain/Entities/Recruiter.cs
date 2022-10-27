using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Recruiter
    {
        public int Id { get; set; }
        public int? RecruitmentCompanyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }

        public virtual RecruitmentCompany? RecruitmentCompany { get; set; }
    }
}
