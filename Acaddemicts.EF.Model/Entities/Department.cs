using System;
using System.Collections.Generic;
using System.Text;

namespace Acaddemicts.EF.Model.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int AdministratorId { get; set; }
        public virtual Person Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
