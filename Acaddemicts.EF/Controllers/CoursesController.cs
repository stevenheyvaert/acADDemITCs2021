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
        [Route("onsite")]
        public async Task<IActionResult> AddOnSiteCourse(string title, int credits, int departmentId, string location, DateTime time, string days)
        {
            using (var ctx = new SchoolContext())
            {
                ctx.Courses.Add(new OnSiteCourse
                {
                    Credits = credits,
                    Title = title,
                    DepartmentId = departmentId,
                    Location = location,
                    Time = time,
                    Days = days
                });
                await ctx.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        [Route("online")]
        public async Task<IActionResult> AddOnlineCourse(string title, int credits, int departmentId, string url)
        {
            using (var ctx = new SchoolContext())
            {
                ctx.Courses.Add(new OnlineCourse
                {
                    Credits = credits,
                    Title = title,
                    DepartmentId = departmentId,
                    Url = url
                });
                await ctx.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCourses()
        {
            using (var ctx = new SchoolContext())
            {
                var courses = await ctx.Courses.Include(x => x.Department)
                    .Select(x => new { CourseName = x.Title, Department = x.Department.Name }).ToListAsync();

                return Ok(courses);
            }
        }

        [HttpGet]
        [Route("online")]
        public async Task<IActionResult> GetAllOnLineCourses()
        {
            using (var ctx = new SchoolContext())
            {
                var courses = await ctx.Courses.OfType<OnlineCourse>().Include(x => x.Department)
                    .Select(x => new { CourseName = x.Title, Department = x.Department.Name, x.Url }).ToListAsync();

                return Ok(courses);
            }
        }

        [HttpGet]
        [Route("onsite")]
        public async Task<IActionResult> GetAllOnSiteCourses()
        {
            using (var ctx = new SchoolContext())
            {
                var courses = await ctx.Courses.OfType<OnSiteCourse>().Include(x => x.Department)
                    .Select(x => new { CourseName = x.Title, Department = x.Department.Name, x.Location }).ToListAsync();

                return Ok(courses);
            }
        }
    }
}