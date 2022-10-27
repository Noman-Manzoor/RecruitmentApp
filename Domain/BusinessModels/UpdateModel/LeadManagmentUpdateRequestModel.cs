using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessModels.UpdateModel
{
    public class LeadManagmentUpdateRequestModel
    {
        public int Id { get; set; }
        public int? RecruiterCompanyId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProfileId { get; set; }
        public string? RecruiterName { get; set; }
        public string? Interviewee { get; set; }
        public string? Interviewer { get; set; }
        public string? InterviewLevel { get; set; }
        public string? InterviewStatus { get; set; }
        public string? TechnologyStack { get; set; }
        public string? JobDescription { get; set; }
        public decimal? HourlyRate { get; set; }
        public double? AnnaulSalary { get; set; }
        public string? JobStatus { get; set; }
    }
}
