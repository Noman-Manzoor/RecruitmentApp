using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessModels.ResponseModel
{
    public class CompanyResponseModel
    {
        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CompanyType { get; set; }
        public string? ContactNumber { get; set; }
        public string? BusinessType { get; set; }
        public string? NatureOfWork { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Crediblity { get; set; }
    }
}
