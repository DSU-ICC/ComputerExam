using System;
using System.Collections.Generic;

namespace DSUContextDBService.Models
{
    public partial class CaseSTplan
    {
        public int PId { get; set; }
        public int FilId { get; set; }
        public int? FacId { get; set; }
        public int DeptId { get; set; }
        public int EdukindId { get; set; }
        public string? PlanName { get; set; }
        public int Y { get; set; }
        public byte Period { get; set; }
        public byte Sem { get; set; }
        public byte NumGak { get; set; }
        public string? QCode { get; set; }
    }
}
