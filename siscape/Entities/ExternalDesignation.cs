using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class ExternalDesignation
    {
        public Int32 code { set; get; }
        public String position { set; get; }
        public String location { set; get; }
        public Int32 hours { set; get; }
        //public Int16 state { set; get; }
        public DateTime initial_day { set; get; }
        public DateTime final_day { set; get; }
        public Teacher oTeacher { set; get; }
        public List<Journey> journeys { set; get; }

        public ExternalDesignation(Int32 pCode, String position, String location, Int32 hours, DateTime INITIAL_DATE,
            DateTime FINAL_DATE, Teacher oTeacher, List<Entities.Journey> listDay)
        {
            this.code = pCode;
            this.position = position;
            this.location = location;
            this.hours = hours;
            this.initial_day = INITIAL_DATE;
            this.final_day = FINAL_DATE;
            this.oTeacher = oTeacher;
            this.journeys = listDay;
        }

        public ExternalDesignation() { }
    }
}