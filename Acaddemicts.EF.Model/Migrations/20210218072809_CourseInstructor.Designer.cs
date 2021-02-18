﻿// <auto-generated />
using System;
using Acaddemicts.EF.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Acaddemicts.EF.Model.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20210218072809_CourseInstructor")]
    partial class CourseInstructor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOnLine")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Courses");

                    b.HasDiscriminator<bool>("IsOnLine");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.CourseGrade", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Grade")
                        .HasColumnType("decimal(18,6)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseGrade");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.CourseInstructor", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "InstructorId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CourseInstructor");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdministratorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("InstructorPersonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DepartmentId");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("InstructorPersonId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnrolled")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasDiscriminator<bool>("IsEnrolled");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.OnSiteCourse", b =>
                {
                    b.HasBaseType("Acaddemicts.EF.Model.Entities.Course");

                    b.Property<string>("Days")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue(false);
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.OnlineCourse", b =>
                {
                    b.HasBaseType("Acaddemicts.EF.Model.Entities.Course");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(true);
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Instructor", b =>
                {
                    b.HasBaseType("Acaddemicts.EF.Model.Entities.Person");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue(false);
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Student", b =>
                {
                    b.HasBaseType("Acaddemicts.EF.Model.Entities.Person");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue(true);
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Course", b =>
                {
                    b.HasOne("Acaddemicts.EF.Model.Entities.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.CourseGrade", b =>
                {
                    b.HasOne("Acaddemicts.EF.Model.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("Acaddemicts.EF.Model.Entities.Student", "Student")
                        .WithMany("CourseGrades")
                        .HasForeignKey("StudentId");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.CourseInstructor", b =>
                {
                    b.HasOne("Acaddemicts.EF.Model.Entities.Course", "Course")
                        .WithMany("CourseInstructors")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Acaddemicts.EF.Model.Entities.Instructor", "Instructor")
                        .WithMany("CourseInstructors")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Department", b =>
                {
                    b.HasOne("Acaddemicts.EF.Model.Entities.Person", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Acaddemicts.EF.Model.Entities.Instructor", null)
                        .WithMany("Departments")
                        .HasForeignKey("InstructorPersonId");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Course", b =>
                {
                    b.Navigation("CourseInstructors");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Instructor", b =>
                {
                    b.Navigation("CourseInstructors");

                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Acaddemicts.EF.Model.Entities.Student", b =>
                {
                    b.Navigation("CourseGrades");
                });
#pragma warning restore 612, 618
        }
    }
}
