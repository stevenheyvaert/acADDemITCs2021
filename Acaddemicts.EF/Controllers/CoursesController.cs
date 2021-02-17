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
    public class CoursesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddCourse(string title, int credits, int departmentId)
        {
            using (var ctx = new SchoolContext())
            {
                ctx.Courses.Add(new Course
                {
                    Credits = credits,
                    Title = title,
                    DepartmentId = departmentId
                });
                await ctx.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            using (var ctx = new SchoolContext())
            {
                var courses = await ctx.Courses.Include(x => x.Department)
                    .Select(x => new { CourseName = x.Title, Department = x.Department.Name }).ToListAsync();

                return Ok(courses);
            }
        }
    }
}