using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Model;

namespace SchoolManager.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ScheduleEntry> ScheduleEntries { get; set; }
        public DbSet<TeacherSalary> TeacherSalaries { get; set; }
        public DbSet<MotivationalAllowance> MotivationalAllowances { get; set; }


        public Context(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Employee → Position (many-to-one)
            builder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee ↔ Education (many-to-many)
            builder.Entity<Employee>()
                .HasMany(e => e.Educations)
                .WithMany(ed => ed.Employees)
                .UsingEntity(j => j.ToTable("EmployeeEducation"));
            
            // ScheduleEntry relationships
            builder.Entity<ScheduleEntry>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(se => se.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ScheduleEntry>()
                .HasOne(e => e.Position)
                .WithMany()
                .HasForeignKey(se => se.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeacherSalary>()
                .HasMany(ts => ts.AllowancesHistory)
                .WithOne(ma => ma.TeacherSalary)
                .HasForeignKey(ma => ma.TeacherSalaryId);

            builder.Entity<MotivationalAllowance>()
                .HasOne(ma => ma.Teacher)
                .WithMany()
                .HasForeignKey(ma => ma.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
       