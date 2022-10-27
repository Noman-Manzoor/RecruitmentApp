using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PersonalInformation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNumbers { get; set; }
        public string? Email { get; set; }
        public int? ProfileId { get; set; }
        public int? CompanyId { get; set; }
        public int? RecruitmentCompanyId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual RecruitmentCompany? RecruitmentCompany { get; internal set; }
        public virtual ProfileManagement? ProfileManagement { get; internal set; }
        public virtual Company? Company { get; internal set; }
    }
}
