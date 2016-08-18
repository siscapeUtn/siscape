using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class UserSystem
    {
        public int code { get; set; }
        public String id { get; set; }
        public String name { get; set; }
        public String lastName { get; set; }
        public String homePhone { get; set; }
        public String cellPhone { get; set; }
        public String email { get; set; }
        public Program oProgram { get; set; }
        public String Password { get; set; }
        public String setPassword { get; set; }
        public Role oRole { get; set; }
        public Int16 state { set; get; }

        public UserSystem() {}
    }
}