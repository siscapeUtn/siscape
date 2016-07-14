using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Headquarters
    {
        public Int32 code { set; get; }
        public String description { set; get; }
        public Int32 state { set; get; }

        public Headquarters() { }
    }
}