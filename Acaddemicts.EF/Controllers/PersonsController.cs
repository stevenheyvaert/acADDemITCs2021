using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acaddemicts.EF.Model;
using Acaddemicts.EF.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;

namespace Acaddemicts.EF.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private bool NextBool(Random r, int truePercentage = 50)
        {
            return r.NextDouble() < truePercentage / 100.0;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersons(int howMany)
        {
            var personGenerator = new PersonNameGenerator();
            Random r = new Random();

            using (var ctx = new SchoolContext())
            {
                for (int i = 0; i < howMany; i++)
                {
                    var isStudent = NextBool(r);
                    if (isStudent)
                    {
                        var person = new Student
                        {
                            LastName = personGenerator.GenerateRandomLastName(),
                            FirstName = personGenerator.GenerateRandomFirstName(),
                            EnrollmentDate = DateTime.Now,
                        };
                        ctx.Persons.Add(person);
                    }
                    else
                    {
                        var person = new Instructor
                        {
                            LastName = personGenerator.GenerateRandomLastName(),
                            FirstName = personGenerator.GenerateRandomFirstName(),
                            HireDate = DateTime.Now,
                        };
                        ctx.Persons.Add(person);
                    }
                }

                await ctx.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            using (var ctx = new SchoolContext())
            {
                var departments = await ctx.Persons.ToListAsync();
                return Ok(departments);
            }
        }

        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            using (var ctx = new SchoolContext())
            {
                var departments = await ctx.Persons.OfType<Student>().ToListAsync();
                return Ok(departments);
            }
        }

        [HttpGet]
        [Route("instructors")]
        public async Task<IActionResult> GetAllInstructors()
        {
            using (var ctx = new SchoolContext())
            {
                var departments = await ctx.Persons.OfType<Instructor>().ToListAsync();
                return Ok(departments);
            }
        }
    }
}