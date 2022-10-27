using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Setting
    {
        public long SettingId { get; set; }
        public long? SettingTypeId { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
    }
}
