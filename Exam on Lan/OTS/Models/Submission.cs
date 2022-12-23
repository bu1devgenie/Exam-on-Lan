using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        public Student Student { get; set; }
        public DateTime SubmitDate { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
