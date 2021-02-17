using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acaddemicts.EF.Model.Entities
{
    public abstract class Person
    {
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsEnrolled { get; set; }
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    }

    public class Student : Person
    {
        public DateTime EnrollmentDate { get; set; }
    }

    public class Instructor : Person
    {
        public DateTime HireDate { get; set; }
    }
}
