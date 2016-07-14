using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class PeriodType
    {
        public Int32 code { get; set; }
        public string description { get; set; }
        public Int16 state { get; set; }

        public PeriodType() { }

        public PeriodType(Int32 code, String description, Int16 state)
        {
            this.code = code;
            this.description = description;
            this.state = state;
        }

        public PeriodType(String description, Int16 state)
        {
            this.description = description;
            this.state = state;
        }
    }
}