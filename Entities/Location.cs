using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Location
    {
        public Int32 code { set; get; }
        public String building { set; get; } //Num or name of the class room 
        public String module { set; get; }
        public Headquarters oHeadquarters { set; get; }
        public Int32 State { set; get; }

        public Location() { }
    }
}