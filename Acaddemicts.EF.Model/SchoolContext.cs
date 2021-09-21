using Acaddemicts.EF.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acaddemicts.EF.Model
{
    public class SchoolContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
               .HasDiscriminator<bool>("IsEnrolled")
               .HasValue<Instructor>(false)
               .HasValue<Student>(true);

            modelBuilder.Entity<Course>()
               .HasDiscriminator<bool>("IsOnLine")
               .HasValue<OnlineCourse>(true)
               .HasValue<OnSiteCourse>(false);

            modelBuilder.Entity<CourseGrade>()
                .Property(x => x.Grade)
                .HasColumnType("decimal(18, 6)");

            modelBuilder.Entity<CourseInstructor>()
                .HasKey(nameof(CourseInstructor.CourseId), nameof(CourseInstructor.InstructorId));

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(x => x.Course).WithMany(x => x.CourseInstructors)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(x => x.Instructor).WithMany(x => x.CourseInstructors)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseGrade>()
                .HasOne(x => x.Course).WithMany(x => x.CourseGrades)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseGrade>()
                .HasOne(x => x.Student).WithMany(x => x.CourseGrades)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
