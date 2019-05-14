using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP2.Models
{
    public class Announcement
    {
        public long Id { get; set; }
        public long ProfessorId { get; set; }
        public string CourseId { get; set; }
        public string Announcement1 { get; set; }
        public DateTime Date { get; set; }

        public virtual Course Course { get; set; }
        public virtual Professor Professor { get; set; }
    }
}
