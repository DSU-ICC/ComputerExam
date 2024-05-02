using Microsoft.EntityFrameworkCore;
using DSUContextDBService.Models;

namespace DSUContextDBService.DBService
{
    public partial class DSUContext : DbContext
    {
        public DSUContext()
        {
        }

        public DSUContext(DbContextOptions<DSUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaseCEdukind> CaseCEdukinds { get; set; } = null!;
        public virtual DbSet<CaseCFaculty> CaseCFaculties { get; set; } = null!;
        public virtual DbSet<CaseCFilial> CaseCFilials { get; set; } = null!;
        public virtual DbSet<CaseSDepartment> CaseSDepartments { get; set; } = null!;
        public virtual DbSet<CaseSSpecialization> CaseSSpecializations { get; set; } = null!;
        public virtual DbSet<CaseSStudent> CaseSStudents { get; set; } = null!;
        public virtual DbSet<CaseSTeacher> CaseSTeachers { get; set; } = null!;
        public virtual DbSet<CaseSTplan> CaseSTplans { get; set; } = null!;
        public virtual DbSet<CaseSTplandetail> CaseSTplandetails { get; set; } = null!;
        public virtual DbSet<CaseUkoModule> CaseUkoModules { get; set; } = null!;
        public virtual DbSet<CaseSSubject> CaseSSubjects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaseCEdukind>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_C_EDUKIND");

                entity.Property(e => e.Abr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ABR");

                entity.Property(e => e.Edukind)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EDUKIND");

                entity.Property(e => e.EdukindId).HasColumnName("EDUKIND_ID");

                entity.Property(e => e.Yearedu).HasColumnName("YEAREDU");
            });

            modelBuilder.Entity<CaseCFaculty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_C_FACULTY");

                entity.Property(e => e.Abr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ABR");

                entity.Property(e => e.College).HasColumnName("COLLEGE");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.FacName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FAC_NAME");
            });

            modelBuilder.Entity<CaseCFilial>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_C_FILIAL");

                entity.Property(e => e.Abr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ABR");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.FilId).HasColumnName("FIL_ID");

                entity.Property(e => e.Filial)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("FILIAL");
            });

            modelBuilder.Entity<CaseSDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_DEPARTMENT");

                entity.Property(e => e.Abr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ABR");

                entity.Property(e => e.Code)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DEPT_NAME");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.Qualification)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("QUALIFICATION");
            });

            modelBuilder.Entity<CaseSSpecialization>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_SPECIALIZATION");

                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");

                entity.Property(e => e.FilId).HasColumnName("FIL_ID");

                entity.Property(e => e.SpecId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SPEC_ID");

                entity.Property(e => e.SpecName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("SPEC_NAME");
            });

            modelBuilder.Entity<CaseSStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_STUDENT");

                entity.Property(e => e.AbiturId).HasColumnName("ABITUR_ID");

                entity.Property(e => e.Accelerate).HasColumnName("ACCELERATE");

                entity.Property(e => e.Adrbefore)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ADRBEFORE");

                entity.Property(e => e.Adrnow)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ADRNOW");

                entity.Property(e => e.Alien).HasColumnName("ALIEN");

                entity.Property(e => e.Attestatnum)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ATTESTATNUM");

                entity.Property(e => e.Attestatser)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ATTESTATSER");

                entity.Property(e => e.CountryId).HasColumnName("COUNTRY_ID");

                entity.Property(e => e.Course).HasColumnName("COURSE");

                entity.Property(e => e.Databorn)
                    .HasColumnType("datetime")
                    .HasColumnName("DATABORN");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DocId).HasColumnName("DOC_ID");

                entity.Property(e => e.EduesId).HasColumnName("EDUES_ID");

                entity.Property(e => e.Edugradyear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("EDUGRADYEAR");

                entity.Property(e => e.EdukindId).HasColumnName("EDUKIND_ID");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.FilId).HasColumnName("FIL_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Given)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("GIVEN");

                entity.Property(e => e.GivenDate)
                    .HasColumnType("datetime")
                    .HasColumnName("GIVEN_DATE");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Lastedu)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("LASTEDU");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Ndiplom)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NDIPLOM");

                entity.Property(e => e.Newcourse).HasColumnName("NEWCOURSE");

                entity.Property(e => e.Ngroup)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NGROUP");

                entity.Property(e => e.Npassport)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NPASSPORT");

                entity.Property(e => e.Nzachkn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NZACHKN");

                entity.Property(e => e.Patr)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PATR");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Placeborn)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PLACEBORN");

                entity.Property(e => e.Plat).HasColumnName("PLAT");

                entity.Property(e => e.Ser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SER");

                entity.Property(e => e.Serdiplom)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SERDIPLOM");

                entity.Property(e => e.Sex)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SEX")
                    .IsFixedLength();

                entity.Property(e => e.Snils)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SNILS");

                entity.Property(e => e.SpecId).HasColumnName("SPEC_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TrData)
                    .HasColumnType("datetime")
                    .HasColumnName("TR_DATA");

                entity.Property(e => e.TrNorder)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TR_NORDER");
            });

            modelBuilder.Entity<CaseSTeacher>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_TEACHER");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Patr)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("PATR");

                entity.Property(e => e.TeachId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TEACH_ID");
            });

            modelBuilder.Entity<CaseSTplan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_TPLAN");

                entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");

                entity.Property(e => e.EdukindId).HasColumnName("EDUKIND_ID");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.FilId).HasColumnName("FIL_ID");

                entity.Property(e => e.NumGak).HasColumnName("NUM_GAK");

                entity.Property(e => e.PId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("P_ID");

                entity.Property(e => e.Period).HasColumnName("PERIOD");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PLAN_NAME");

                entity.Property(e => e.QCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Q_CODE");

                entity.Property(e => e.Sem).HasColumnName("SEM");
            });

            modelBuilder.Entity<CaseSTplandetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_TPLANDETAIL");

                entity.Property(e => e.Attest).HasColumnName("ATTEST");

                entity.Property(e => e.CWork).HasColumnName("C_WORK");

                entity.Property(e => e.CathId).HasColumnName("CATH_ID");

                entity.Property(e => e.CompId).HasColumnName("COMP_ID");

                entity.Property(e => e.Contr).HasColumnName("CONTR");

                entity.Property(e => e.CycleId).HasColumnName("CYCLE_ID");

                entity.Property(e => e.Dfk).HasColumnName("DFK");

                entity.Property(e => e.Diffzachet).HasColumnName("DIFFZACHET");

                entity.Property(e => e.Exam).HasColumnName("EXAM");

                entity.Property(e => e.IndHours).HasColumnName("IND_HOURS");

                entity.Property(e => e.Lab).HasColumnName("LAB");

                entity.Property(e => e.Lect).HasColumnName("LECT");

                entity.Property(e => e.Mod).HasColumnName("MOD");

                entity.Property(e => e.NThreads).HasColumnName("N_THREADS");

                entity.Property(e => e.PId).HasColumnName("P_ID");

                entity.Property(e => e.PdId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PD_ID");

                entity.Property(e => e.Pract).HasColumnName("PRACT");

                entity.Property(e => e.Ref).HasColumnName("REF");

                entity.Property(e => e.SId).HasColumnName("S_ID");

                entity.Property(e => e.SamHours).HasColumnName("SAM_HOURS");

                entity.Property(e => e.Sem).HasColumnName("SEM");

                entity.Property(e => e.SessId).HasColumnName("SESS_ID");

                entity.Property(e => e.SpecId).HasColumnName("SPEC_ID");

                entity.Property(e => e.SubjGroup)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("SUBJ_GROUP");

                entity.Property(e => e.Zachet).HasColumnName("ZACHET");
            });

            modelBuilder.Entity<CaseUkoModule>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_UKO_MODULES");

                entity.Property(e => e.Cathedra)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATHEDRA");

                entity.Property(e => e.Closed).HasColumnName("CLOSED");

                entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");

                entity.Property(e => e.EdukindId).HasColumnName("EDUKIND_ID");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.FilId).HasColumnName("FIL_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Ngroup)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NGROUP");

                entity.Property(e => e.Nmod).HasColumnName("NMOD");

                entity.Property(e => e.Patr)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PATR");

                entity.Property(e => e.Predmet)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("PREDMET");

                entity.Property(e => e.Prepod)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PREPOD");

                entity.Property(e => e.Rb).HasColumnName("RB");

                entity.Property(e => e.SId).HasColumnName("S_ID");

                entity.Property(e => e.SessId).HasColumnName("SESS_ID");

                entity.Property(e => e.SpecName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("SPEC_NAME");

                entity.Property(e => e.StudentStatus).HasColumnName("STUDENT_STATUS");

                entity.Property(e => e.Subgroup)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SUBGROUP");

                entity.Property(e => e.TeachId1).HasColumnName("TEACH_ID1");

                entity.Property(e => e.TeachId2).HasColumnName("TEACH_ID2");

                entity.Property(e => e.TeachId3).HasColumnName("TEACH_ID3");

                entity.Property(e => e.Veddate)
                    .HasColumnType("datetime")
                    .HasColumnName("VEDDATE");
            });

            modelBuilder.Entity<CaseSSubject>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CASE_S_SUBJECT");

                entity.Property(e => e.CathId).HasColumnName("CATH_ID");

                entity.Property(e => e.CycleId).HasColumnName("CYCLE_ID");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.SAbr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("S_ABR");

                entity.Property(e => e.SId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("S_ID");

                entity.Property(e => e.SName)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("S_NAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
