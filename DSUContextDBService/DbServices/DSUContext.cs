using Microsoft.EntityFrameworkCore;

namespace DSUContextDBService.DbServices
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

        public virtual DbSet<CaseSDepartment> CaseSDepartments { get; set; } = null!;
        public virtual DbSet<CaseSSpecialization> CaseSSpecializations { get; set; } = null!;
        public virtual DbSet<CaseSStudent> CaseSStudents { get; set; } = null!;
        public virtual DbSet<CaseSTeacher> CaseSTeachers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.Godequalif)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("GODEQUALIF");

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

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.FilId).HasColumnName("FIL_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Graddate)
                    .HasColumnType("datetime")
                    .HasColumnName("GRADDATE");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Nzachkn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NZACHKN");

                entity.Property(e => e.Patr)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PATR");

                entity.Property(e => e.Snils)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SNILS");

                entity.Property(e => e.Status).HasColumnName("STATUS");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
