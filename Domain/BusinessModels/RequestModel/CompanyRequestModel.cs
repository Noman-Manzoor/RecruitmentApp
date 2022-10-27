using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessModels.RequestModel
{
    public class CompanyRequestModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Company type is required.")]
        public string? CompanyType { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        public string? ContactNumber { get; set; }
        [Required(ErrorMessage = "Business type is required.")]
        public string? BusinessType { get; set; }
        [Required(ErrorMessage = "Nature of work is required.")]
        public string? NatureOfWork { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        public string? Crediblity { get; set; }
    }
}
