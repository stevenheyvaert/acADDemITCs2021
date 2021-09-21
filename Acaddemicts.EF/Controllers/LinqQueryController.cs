using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acaddemicts.EF.Model;
using Acaddemicts.EF.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acaddemicts.EF.Controllers
{
    [Route("api/[controller]")]
    public class LinqQueryController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetLinqQuery(int number)
        {
            using (var ctx = new SchoolContext())
            {
                switch (number)
                {
                    case 1:
                        // Add a query to only display the OnSiteCourse records, sorted on Title
                        var onsiteCourses = await ctx.Courses.OfType<OnSiteCourse>().OrderBy(x => x.Title).ToListAsync();
                        return Ok(onsiteCourses);
                    case 2:
                        // Add a query to only display the OnLineCourse records, sorted on Title, then by Url
                        var onLineCourses = await ctx.Courses.OfType<OnlineCourse>()
                            .OrderBy(x => x.Title)
                            .ThenBy(x => x.Url)
                            .ToListAsync();
                        return Ok(onLineCourses);
                    case 3:
                        // Add a query to only display the Student records. Sorted on Lastname, include the year of the enrollment.
                        var students = await ctx.Persons.OfType<Student>()
                            .OrderBy(x => x.LastName)
                            .Select(x => new { x.LastName, x.FirstName, x.EnrollmentDate.Year })
                            .ToListAsync();
                        return Ok(students);
                    case 4:
                        // Add a query to only display the Instructor records. Include and sort on seniority (=years in service).
                        var instructors = await ctx.Persons.OfType<Instructor>()
                            .Select(x => new { x.LastName, x.FirstName, Seniority= (DateTime.Now.Year - x.HireDate.Year)})
                            .OrderByDescending(x => x.Seniority)
                            .ToListAsync();
                        return Ok(instructors);
                    case 5:
                        // Add a query to show the students, subscribed before 1/9/2004.
                        var studentsBeforeSept2004 = await ctx.Persons.OfType<Student>()
                            .Where(x => x.EnrollmentDate < new DateTime(2004, 9, 1)).ToListAsync();
                        return Ok(studentsBeforeSept2004);
                    case 6:
                        // Add a query to list the departments with courses, having credits less than 3.
                        var depCredits = await ctx.Departments.Where(x => x.Courses.Any(y => y.Credits < 3)).ToListAsync();
                        return Ok(depCredits);
                    case 7:
                        // Add a list of results from all students, sorted by student name and course name.  
                        // Return: name and surname of the student, course name, and result (=grade)
                        var listStudents = await ctx.Set<CourseGrade>()
                                .OrderBy(x => x.Student.LastName)
                                .ThenBy(x => x.Course.Title)
                                .Where(x => x.Grade.HasValue)
                                .Select(x => $"{x.Student.LastName} {x.Student.FirstName} {x.Course.Title} {x.Grade}").ToListAsync();
                        return Ok(listStudents);
                    case 8:
                        // Return a list of all distinct surnames of instructors.
                        var distinctInstructor = await ctx.Persons.OfType<Instructor>().Select(x => x.FirstName).Distinct().ToListAsync();
                        return Ok(distinctInstructor);
                    case 9:
                        // Return the total of students, having results from courses in a department where the budget is larger than 200000.
                        var totalStudents = await ctx.Set<CourseGrade>()
                                .Where(x => x.Student.EnrollmentDate > DateTime.MinValue && x.Grade.HasValue && x.Course.Department.Budget > 200_000)
                                .Select(x => x.StudentId)
                                .Distinct()
                                .CountAsync();
                        return Ok(totalStudents);
                    case 10:
                        // Return the average course (grade) from all students, sorted by name, surname
                        var averagePerStudent = await ctx.Persons.OfType<Student>()
                                .OrderBy(x => x.LastName)
                                .ThenBy(x => x.FirstName)
                                .Select(x => $"{x.LastName} {x.FirstName} {x.CourseGrades.Average(y => y.Grade)}").ToListAsync();
                        return Ok(averagePerStudent);
                    case 11:
                        // Return the list of lowest grades per course.
                        var lowestPerCourse = await ctx.Courses
                                .Where(x => x.CourseGrades.Any(y => y.Grade.HasValue))
                                .Select(x => $"{x.Title} {x.CourseGrades.Min(y => y.Grade)}").ToListAsync();
                        return Ok(lowestPerCourse);
                    case 12:
                        // Return the list of highest grade per department.
                        var highestPerDept = await ctx.Departments
                                .Where(x => x.Courses.SelectMany(y => y.CourseGrades).Any(z => z.Grade.HasValue))
                                .Select(x => $"{x.Name} {x.Courses.SelectMany(y => y.CourseGrades).Max(z => z.Grade)}").ToListAsync();
                        return Ok(highestPerDept);
                    case 13:
                        // Return a list of students, subscribed in 2004, sorted by name, surname
                        var stud2004 = await ctx.Persons.OfType<Student>()
                                .Where(x => x.EnrollmentDate.Year == 2004)
                                .OrderBy(x => x.FirstName)
                                .ThenBy(x => x.LastName)
                                .Select(x => $"{x.FirstName} {x.LastName} {x.EnrollmentDate:dd/MM/yyyy}").ToListAsync();
                        return Ok(stud2004);
                    case 14:
                        // Return a list of all courses, where the name contains ‘et’
                        var cont = await ctx.Courses
                                .Where(x => x.Title.Contains("et"))
                                .Select(x => x.Title).ToListAsync();
                        return Ok(cont);
                }
            }
            return NotFound();
        }
    }
}