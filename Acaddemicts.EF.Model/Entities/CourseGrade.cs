using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acaddemicts.EF.Model.Entities
{
    public class CourseGrade
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public decimal? Grade { get; set; }
    }
}
