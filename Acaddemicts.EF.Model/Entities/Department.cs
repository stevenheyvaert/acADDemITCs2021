using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acaddemicts.EF.Model.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int AdministratorId { get; set; }
        public virtual Person Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }
    }
}
