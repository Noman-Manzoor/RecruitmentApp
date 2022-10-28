using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessModels.Update
{
    public class ProfileManagementUpdateRequestModel
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? VisaStatus { get; set; }
        public string? DrivingLiscenceNo { get; set; }
        public bool? BornInUSA { get; set; }
        public string? MoveToUSA { get; set; }
        public string? StatusInUSA { get; set; }
        public string? MaritalStauts { get; set; }
        public string? Documents { get; set; }
        public string? SkillSet { get; set; }
        public string? JobExperience { get; set; }
        public bool? IsVaccinated { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public bool? HavingSiblings { get; set; }
        public int? NoOfBrothers { get; set; }
        public int? NoOfSisters { get; set; }
        public int? NoOfKids { get; set; }
    }
}
