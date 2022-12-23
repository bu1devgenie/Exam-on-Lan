using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Mark
    {
        public Test Test { get; set; }
        public Student Student { get; set; }
        public float Grade { get; set; }
        public string Note { get; set; }
    }
}
