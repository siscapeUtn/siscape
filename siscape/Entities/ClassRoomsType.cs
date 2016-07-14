using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class ClassRoomsType
    {
        public Int32 code { set; get; }
        public String description { set; get; }
        public Int16 state { set; get; }

        public ClassRoomsType() { }

        public ClassRoomsType(Int32 pCode, String pDescription, Int16 pState)
        {
            this.code = pCode;
            this.description = pDescription;
            this.state = pState;
        }
    }
}