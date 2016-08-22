using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class WaitingList
    {
        public Int32 code { set; get; }
        public String id { set; get; }
        public String name { set; get; }
        public String lastName { get; set; }
        public String homePhone { get; set; }
        public String cellPhone { get; set; }
        public String email { get; set; }
        public Int32 period { get; set; }
        public Int32 course { get; set; }
        public String course_name { get; set; }
        public String day { get; set; }
        public Int32 contacted { get; set; }
    }
}