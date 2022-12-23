using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public Level Level { get; set; }
        public Subject Subject { get; set; }
        public Type Type { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
