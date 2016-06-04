using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Functionary
    {
        public Int32 code { set; get; }
        public String id { set; get; }
        public String name { set; get; }
        public String lastName { get; set; }
        public String homePhone { get; set; }
        public String cellPhone { get; set; }
        public String email { get; set; }
        public Int16 state { set; get; }
    }
}