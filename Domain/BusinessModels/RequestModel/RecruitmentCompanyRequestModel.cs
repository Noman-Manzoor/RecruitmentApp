using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessModels.RequestModel
{
    public class RecruitmentCompanyRequestModel
    {
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CompanyType { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

    }
}
