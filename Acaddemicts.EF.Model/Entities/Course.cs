using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acaddemicts.EF.Model.Entities
{
    public abstract class Course
    {
        public int CourseId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; } = new HashSet<CourseInstructor>();
        public virtual ICollection<CourseGrade> CourseGrades { get; set; } = new HashSet<CourseGrade>();
    }

    public class OnSiteCourse : Course
    {
        [MaxLength(50)]
        public string Location { get; set; }
        [MaxLength(50)]
        public string Days { get; set; }
        public DateTime Time { get; set; }
    }

    public class OnlineCourse : Course
    {
        [MaxLength(100)]
        public string Url { get; set; }
    }
}
