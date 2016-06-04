    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Period
    {
        public Int32 code { get; set; }
        public String name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finalDate { get; set; }
        public PeriodType oPeriodType { get; set; }
        public Int16 state { get; set; }

        public Period(String name, DateTime startDate, DateTime finalDate, PeriodType periodType, Int16 state)
        {
            this.name = name;
            this.startDate = startDate;
            this.finalDate = finalDate;
            this.oPeriodType = periodType;
            this.state = state;
        }

        public Period(Int32 code, String name, DateTime startDate, DateTime finalDate, PeriodType periodType, Int16 state)
        {
            this.code = code;
            this.name = name;
            this.startDate = startDate;
            this.finalDate = finalDate;
            this.oPeriodType = periodType;
            this.state = state;
        }

        public Period(){ }
    }
}