using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessModels.Request
{
    public class ProfileManagementRequestModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Visa status is required.")]
        public string? VisaStatus { get; set; }
        [Required(ErrorMessage = "Driving Liscense No is required.")]
        public string? DrivingLiscenceNo { get; set; }
        [Required(ErrorMessage = "Move in USA field is required.")]
        public string? MoveInUSA { get; set; }
        [Required(ErrorMessage = "Born in USA field is required.")]
        public bool? BornInUSA { get; set; }

        [Required(ErrorMessage = "Status in USA field is required.")]
        public string? StatusInUSA { get; set; }
        [Required(ErrorMessage = "Marital status is required.")]
        public string? MaritalStauts { get; set; }
        [Required(ErrorMessage = "Documents field is required.")]
        public string? Documents { get; set; }
        [Required(ErrorMessage = "Skill set is required.")]
        public string? SkillSet { get; set; }
        [Required(ErrorMessage = "Job Experience is required.")]
        public string? JobExperience { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public bool? IsVaccinated { get; set; }

        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public bool? HavingSiblings { get; set; }
        public int? NoOfBrothers { get; set; }
        public int? NoOfSisters { get; set; }
        public int? NoOfKids { get; set; }

    }
}
