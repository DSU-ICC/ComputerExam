using System;
using System.Collections.Generic;

namespace DSUContextDBService.Models
{
    public partial class CaseSStudent
    {
        public int Id { get; set; }
        public int FilId { get; set; }
        public int FacId { get; set; }
        public int DepartmentId { get; set; }
        public int EdukindId { get; set; }
        public int Course { get; set; }
        public int Newcourse { get; set; }
        public string? Ngroup { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Patr { get; set; }
        public int Status { get; set; }
        public string? Nzachkn { get; set; }
        public string? Snils { get; set; }
        public string? Ndiplom { get; set; }
        public string? Serdiplom { get; set; }
        public int AbiturId { get; set; }
        public int Plat { get; set; }
        public string Sex { get; set; } = null!;
        public DateTime? Databorn { get; set; }
        public string? Placeborn { get; set; }
        public string? Lastedu { get; set; }
        public string? Phone { get; set; }
        public string? Adrbefore { get; set; }
        public string? Adrnow { get; set; }
        public string? Npassport { get; set; }
        public string? Ser { get; set; }
        public string? Given { get; set; }
        public DateTime? GivenDate { get; set; }
        public int EduesId { get; set; }
        public string? Edugradyear { get; set; }
        public int SpecId { get; set; }
        public int DocId { get; set; }
        public string? Attestatnum { get; set; }
        public string? Attestatser { get; set; }
        public bool Accelerate { get; set; }
        public bool Alien { get; set; }
        public int CountryId { get; set; }
        public DateTime? TrData { get; set; }
        public string? TrNorder { get; set; }
    }
}
