using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acaddemicts.EF.Model.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? HireDate { get; set; }
        public bool IsEnrolled { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    }
}
