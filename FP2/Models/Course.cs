using System;
using System.Collections.Generic;

namespace FP2.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseAssignment = new HashSet<CourseAssignment>();
            Enrollment = new HashSet<Enrollment>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public long DepartmentId { get; set; }
        public string Description { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseAssignment> CourseAssignment { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
        public virtual ICollection<Announcement> Announcement { get; set; }
    }
}
