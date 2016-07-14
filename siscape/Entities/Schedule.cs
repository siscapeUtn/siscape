using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Schedule
    {
        public Int32 code { set; get; }
        public String name { set; get; }
        public String codday { set; get; }
        public Int32 state { set; get; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public String typeSchedule { get; set; }
        public Program oProgram { get; set; }
        public Schedule()
        {
            oProgram = new Program();
        }
    }
}