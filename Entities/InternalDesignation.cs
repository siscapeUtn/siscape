using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class InternalDesignation
    {
        public Int32 code { set; get; }
        public String description { set; get; }
        public Double annuity { set; get; }
        public Double baseSalary { set; get; }
        public Int16 state { set; get; }

        public InternalDesignation() { }

        public InternalDesignation(Int32 pCode, String pDescription, Double pAnnuity, Double pBaseSalary, Int16 pState)
        {
            this.code = pCode;
            this.description = pDescription;
            this.annuity = pAnnuity;
            this.baseSalary = pBaseSalary;
            this.state = pState;
        }
    }
}