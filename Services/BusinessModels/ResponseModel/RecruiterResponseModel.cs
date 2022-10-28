
namespace Services.BusinessModels.Response
{
    public class RecruiterResponseModel
    {
        public int Id { get; set; }
        public int? RecruitmentCompanyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
    }
}
