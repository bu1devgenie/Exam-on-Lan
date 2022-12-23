using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Class
    {
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public override string ToString()
        {
            return ClassCode + " - " + ClassName;
        }
    }
}
