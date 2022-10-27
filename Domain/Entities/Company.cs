using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Description { get; set; }
        public string? CompanyType { get; set; }
        public string? BusinessType { get; set; }
        public string? NatureOfWork { get; set; }
        public string? Crediblity { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<LeadManagement>? LeadManagements { get; set; }

        public virtual ICollection<PersonalInformation>? PersonalInformation { get; internal set; }

    }
}
