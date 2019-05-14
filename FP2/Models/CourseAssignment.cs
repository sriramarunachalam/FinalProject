using System;
using System.Collections.Generic;

namespace FP2.Models
{
    public partial class CourseAssignment
    {
        public string CourseId { get; set; }
        public long ProfessorId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Professor Professor { get; set; }
    }
}
