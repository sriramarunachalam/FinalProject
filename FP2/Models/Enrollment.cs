using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FP2.Models
{
    public partial class Enrollment
    {
        public long EnrollmentId { get; set; }
        public string CourseId { get; set; }
        public long StudentId { get; set; }

        [RegularExpression(@"A|B|C|D|F")]
        public string Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }    
}
