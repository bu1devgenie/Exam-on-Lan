using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Question Question { get; set; }
        public bool IsCorrect { get; set; }
    }
}
