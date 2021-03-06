﻿using System;
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
    public class DepartmentsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddDepartment(string name)
        {
            using (var ctx = new SchoolContext())
            {
                ctx.Departments.Add(new Department { Name = name, StartDate = DateTime.Now });
                await ctx.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            using (var ctx = new SchoolContext())
            {
                var departments = await ctx.Departments.ToListAsync();
                return Ok(departments);
            }
        }
    }
}