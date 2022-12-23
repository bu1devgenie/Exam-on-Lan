using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Subject
    {
        public string SubjectCode { get; set; } 
        public string SubjectName { get; set; }
        public override string ToString()
        {
            return SubjectCode + " - " + SubjectName;
        }
    }
}
