using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class ClassRoom
    {
        public Int32 code { set; get; }
        public String num_room { set; get; } //Num or name of the class room 
        public Int32 size { set; get; }
        public ClassRoomsType oClassRoomsType { set; get; }
        public Program oProgram { set; get; }
        public Location oLocation { set; get; }
        public Int16 state { set; get; }

        public ClassRoom() { }
    }
}