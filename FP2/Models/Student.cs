using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FP2.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public long StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }

        [Display(Name ="Department")]
        public long DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
