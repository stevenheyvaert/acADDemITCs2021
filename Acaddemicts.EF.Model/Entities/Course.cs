using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acaddemicts.EF.Model.Entities
{
    public abstract class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }

    public class OnSiteCourse : Course
    {
        public string Location { get; set; }
        public string Days { get; set; }
        public DateTime Time { get; set; }
    }

    public class OnlineCourse : Course
    {
        public string Url { get; set; }
    }
}
