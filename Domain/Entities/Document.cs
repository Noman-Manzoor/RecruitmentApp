using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Document
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public string? Name { get; set; }
    }
}
