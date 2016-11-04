using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Actives
    {
        public Int32 code { set; get; }
        public string codeAlphaNumeric { get; set; }
        public string description { get; set; }
        public string state { get; set; }
        public ClassRoom oClassRoom { get; set; }

        public Actives() { }
    }
}