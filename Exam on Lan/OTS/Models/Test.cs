using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime TestDate { get; set; }
        public TimeSpan Duration { get; set; }
        public Subject Subject { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsReview { get; set; }
        public List<QuestionTest> QuestionTests = new List<QuestionTest>();
    }
}
