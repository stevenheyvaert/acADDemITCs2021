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
                        // Voeg een query toe om de OnSiteCourse gegevens af te beelden
                        var onsiteCourses = await ctx.Courses.OfType<OnSiteCourse>().ToListAsync();
                        return Ok(onsiteCourses);
                    case 2:
                        // Voeg een query toe om de OnLineCourse gegevens af te beelden
                        var onLineCourses = await ctx.Courses.OfType<OnlineCourse>().ToListAsync();
                        return Ok(onLineCourses);
                    case 3:
                        // Voeg een query toe om de Student gegevens af te beelden
                        var students = await ctx.Persons.OfType<Student>().ToListAsync();
                        return Ok(students);
                    case 4:
                        // Voeg een query toe om de Instructor gegevens af te beelden
                        var instructors = await ctx.Persons.OfType<Instructor>().ToListAsync();
                        return Ok(instructors);
                    case 5:
                        // Geef een lijst van studenten ingeschreven voor 1/9/2004.
                        var studentsBeforeSept2004 = await ctx.Persons.OfType<Student>()
                            .Where(x => x.EnrollmentDate < new DateTime(2004, 9, 1)).ToListAsync();
                        return Ok(studentsBeforeSept2004);
                    case 6:
                        // Geef een lijst van departementen waar er een cursus gegeven wordt waar credits kleiner zijn dan 3.
                        var depCredits = await ctx.Departments.Where(x => x.Courses.Any(y => y.Credits < 3)).ToListAsync();
                        return Ok(depCredits);
                    case 7:
                        // Geef de lijst van punten van alle studenten gesorteerd per student en naam van de cursus. 
                        // Geef volgende gegevens terug: naam en voornaam student, naam van de cursus, resultaat(grade).
                        var listStudents = await ctx.CourseGrade
                                .OrderBy(x => x.Student.LastName)
                                .ThenBy(x => x.Course.Title)
                                .Where(x => x.Grade.HasValue)
                                .Select(x => $"{x.Student.LastName} {x.Student.FirstName} {x.Course.Title} {x.Grade}").ToListAsync();
                        return Ok(listStudents);
                    case 8:
                        // Geef een lijst van de verschillende voornamen van de lesgevers.
                        var distinctInstructor = await ctx.Persons.OfType<Instructor>().Select(x => x.FirstName).Distinct().ToListAsync();
                        return Ok(distinctInstructor);
                    case 9:
                        // Geef het totaal aantal studenten die resultaten hebben behaald voor cursussen in een departement waar het budget groter is dan 200000.
                        var totalStudents = await ctx.CourseGrade
                                .Where(x => x.Student.EnrollmentDate > DateTime.MinValue && x.Grade.HasValue && x.Course.Department.Budget > 200_000)
                                .Select(x => x.StudentId)
                                .Distinct()
                                .CountAsync();
                        return Ok(totalStudents);
                    case 10:
                        // Geef de gemiddelde score (grade) van iedere student, gesorteerd op naam, voornaam van de student.
                        var averagePerStudent = await ctx.Persons.OfType<Student>()
                                .OrderBy(x => x.LastName)
                                .ThenBy(x => x.FirstName)
                                .Select(x => $"{x.LastName} {x.FirstName} {x.CourseGrades.Average(y => y.Grade)}").ToListAsync();
                        return Ok(averagePerStudent);
                    case 11:
                        // Geef een lijst met de laagste punten per cursus.
                        var lowestPerCourse = await ctx.Courses
                                .Where(x => x.CourseGrades.Any(y => y.Grade.HasValue))
                                .Select(x => $"{x.Title} {x.CourseGrades.Min(y => y.Grade)}").ToListAsync();
                        return Ok(lowestPerCourse);
                    case 12:
                        // Geef een lijst met de hoogste puntenscore per departement.
                        var highestPerDept = await ctx.Departments
                                .Where(x => x.Courses.SelectMany(y => y.CourseGrades).Any(z => z.Grade.HasValue))
                                .Select(x => $"{x.Name} {x.Courses.SelectMany(y => y.CourseGrades).Max(z => z.Grade)}").ToListAsync();
                        return Ok(highestPerDept);
                    case 13:
                        // Geef een lijst van de studenten ingeschreven in 2004 gesorteerd op naam en voornaam
                        var stud2004 = await ctx.Persons.OfType<Student>()
                                .Where(x => x.EnrollmentDate.Year == 2004)
                                .OrderBy(x => x.FirstName)
                                .ThenBy(x => x.LastName)
                                .Select(x => $"{x.FirstName} {x.LastName} {x.EnrollmentDate:dd/MM/yyyy}").ToListAsync();
                        return Ok(stud2004);
                    case 14:
                        // Geef een lijst van alle cursussen die voldoen aan een patroon %et%
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