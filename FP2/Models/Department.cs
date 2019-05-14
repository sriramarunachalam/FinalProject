using System;
using System.Collections.Generic;

namespace FP2.Models
{
    public partial class Department
    {
        public Department()
        {
            Course = new HashSet<Course>();
            Professor = new HashSet<Professor>();
        }

        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Professor> Professor { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
