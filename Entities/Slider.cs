using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Slider
    {
        public Int32 code { get; set; }
        public String description { get; set; }
        public String image { get; set; }
        
        public Slider() { }

        public Slider(Int32 pCode, String pDescription, String pImage)
        {
            this.code = pCode;
            this.description = pDescription;
            this.image = pImage;
        }
    }
}