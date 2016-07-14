using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Course
    {
        public Int32 id { set; get; }
        public String description { set; get; }
        public Program oProgram { set; get; }
        public Int16 state { set; get; }
        public String schedule { get; set; }
        public String days { get; set; }
        public Course() {
            oProgram = new Program();
        }
    }
}