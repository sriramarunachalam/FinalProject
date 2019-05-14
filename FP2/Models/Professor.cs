using System;
using System.Collections.Generic;

namespace FP2.Models
{
    public partial class Professor
    {
        public Professor()
        {
            CourseAssignment = new HashSet<CourseAssignment>();
        }

        public long ProfessorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public long DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<CourseAssignment> CourseAssignment { get; set; }
        public virtual ICollection<Announcement> Announcement { get; set; }
        //public virtual Course Course { get; set; }
    }
}
