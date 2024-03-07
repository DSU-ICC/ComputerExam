using System;
using System.Collections.Generic;

namespace DSUContextDBService.Models
{
    public partial class CaseSTplandetail
    {
        public int PdId { get; set; }
        public int PId { get; set; }
        public int SId { get; set; }
        public string? SubjGroup { get; set; }
        public int SpecId { get; set; }
        public int CathId { get; set; }
        public int? CompId { get; set; }
        public byte? CycleId { get; set; }
        public byte SessId { get; set; }
        public byte Exam { get; set; }
        public byte Zachet { get; set; }
        public byte Diffzachet { get; set; }
        public byte Attest { get; set; }
        public byte CWork { get; set; }
        public byte? Dfk { get; set; }
        public int Lect { get; set; }
        public int Pract { get; set; }
        public int Lab { get; set; }
        public int IndHours { get; set; }
        public int SamHours { get; set; }
        public int Sem { get; set; }
        public int Contr { get; set; }
        public int Ref { get; set; }
        public int Mod { get; set; }
        public int NThreads { get; set; }
        public int P { get; set; }
    }
}
