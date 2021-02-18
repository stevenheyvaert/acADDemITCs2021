using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acaddemicts.EF.Model.Entities
{
    public abstract class Person
    {
        public int PersonId { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        public bool IsEnrolled { get; set; }
    }

    public class Student : Person
    {
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<CourseGrade> CourseGrades { get; set; } = new HashSet<CourseGrade>();
    }

    public class Instructor : Person
    {
        public DateTime HireDate { get; set; }
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; } = new HashSet<CourseInstructor>();
    }
}
