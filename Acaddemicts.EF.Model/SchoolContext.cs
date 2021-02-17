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
