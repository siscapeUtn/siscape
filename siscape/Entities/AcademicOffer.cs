using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class AcademicOffer
    {

        public Int32 code { set; get; }
        public Period oPeriod { get; set; }
        public Program oProgram { get; set; }
        public Course oCourse { get; set; }
        public Decimal price { get; set; }
        public ClassRoom oClassRoom { get; set; }
        public Schedule oSchedule { get; set; }
        public Teacher oteacher { get; set; }
        public int hours { get; set; }

        public AcademicOffer()
        {
        }

        public AcademicOffer(Int32 code, Period oPeriod, Program oProgram, Course oCourse, Decimal price,
                            ClassRoom oClassRoom, Schedule oSchedule, Teacher oteacher, int hours)
        {
            this.code = code;
            this.oPeriod = oPeriod;
            this.oProgram = oProgram;
            this.oCourse = oCourse;
            this.price = price;
            this.oClassRoom = oClassRoom;
            this.oSchedule = oSchedule;
            this.oteacher = oteacher;
            this.hours = hours;
        }

    }
}