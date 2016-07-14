using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Journey
    {
        public Int32 code { get; set; }
        public String start { get; set; }
        public String finish { get; set; }
        public Day day { get; set; }
        public Journey() { }

        public Journey(Int32 code, String dayDescriptiion, String start, String finish)
        {
            Day days = new Day();
            days.code = code;
            days.description = dayDescriptiion;
            this.day = days;
            this.start = start;
            this.finish = finish;

        }
    }
}