using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class SubmissionQA
    {
        public Submission Submission { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
