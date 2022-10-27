using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class FamilyDetail
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public bool? HavingSiblings { get; set; }
        public int? NoOfBrothers { get; set; }
        public int? NoOfSisters { get; set; }
        public int? NoOfKids { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ProfileManagement Profile { get; set; } = null!;
    }
}
