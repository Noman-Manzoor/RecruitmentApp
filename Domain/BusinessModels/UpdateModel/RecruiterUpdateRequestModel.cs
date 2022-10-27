using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessModels.Update
{
    public class RecruiterUpdateRequestModel
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        public int? RecruitmentCompanyId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
       
        public string? Email { get; set; }
        [Required(ErrorMessage = "Contact Number is required.")]
        public string? ContactNumber { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }

    }
}
