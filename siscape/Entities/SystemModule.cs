using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class SystemModule
    {
        public int SystemModule_Id { get; set; }
        public String Description { get; set; }
        public int  State { get; set; }
        public int Deleted { get; set; }

        public SystemModule() { }
    }
}