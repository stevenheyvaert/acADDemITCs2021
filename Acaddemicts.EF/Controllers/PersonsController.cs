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
                    var isEnrolled = NextBool(r);
                    var person = new Person
                    {
                        LastName = personGenerator.GenerateRandomLastName(),
                        FirstName = personGenerator.GenerateRandomFirstName(),
                        IsEnrolled = isEnrolled,
                        EnrollmentDate = isEnrolled ? DateTime.Now : default(DateTime?),
                        HireDate = isEnrolled ? default(DateTime?) : DateTime.Now,
                    };
                    ctx.Persons.Add(person);
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
    }
}