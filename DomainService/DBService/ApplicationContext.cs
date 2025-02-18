﻿using DomainService.Entity;
using Microsoft.EntityFrameworkCore;

namespace DomainService.DBService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Examen> Examens { get; set; } = null!;
        public DbSet<ExamTicket> ExamTickets { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<AnswerBlank> AnswerBlanks { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();   // удаляем базу данных при первом обращении
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamTicket>()
                .HasOne(p => p.Examen)
                .WithMany(t => t.Tickets)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>()
                .HasOne(p => p.Ticket)
                .WithMany(t => t.Questions)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
