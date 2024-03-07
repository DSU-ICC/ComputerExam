using System;
using System.Collections.Generic;

namespace DSUContextDBService.Models
{
    public partial class CaseCFilial
    {
        public int FilId { get; set; }
        public string? Filial { get; set; }
        public string? Abr { get; set; }
        public bool Deleted { get; set; }
    }
}
