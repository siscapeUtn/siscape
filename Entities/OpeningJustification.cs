using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class OpeningJustification
    {
        public AcademicOffer oAcademic { get; set; }
        public InternalDesignation  oInternal { get; set; }
        public double Salary { get; set; }
        public double CauntAnualities { get; set; }
        public double Anuality { get; set; }
        public double CCSS { get; set;}
        public double Others { get; set;}
        public double publicity { get; set; }
        public double TotalMensual { get; set; }
        public double TotalBimensual { get; set; }
        public int Students {get; set;}
        public double TotalIncome { get; set; }
        public double Diference { get; set; }

        public OpeningJustification() { }

        public void getSalary()
        {
            double hours = TypesCategories(this.oAcademic.hours);
            this.Salary = oInternal.baseSalary * hours;
        }

        public void getCCSS()
        {
            double cant= 0.37;
            this.CCSS = cant*this.Salary;
            this.publicity = 17000;
        }

        public void getCalc(int anuality,double others, int students)
        {
            this.Others = others;
            this.CauntAnualities = anuality;
            this.Students = students;
            calcAnuality();
            calcIncomes();
            calcIncomesMonth();
            totalDiference();
        }

        private void totalDiference()
        {
            this.Diference = (this.TotalIncome - this.TotalBimensual);
        }

        private void calcIncomesMonth()
        {
            double total = 0;
            total = this.Salary + this.Anuality + this.CCSS + this.publicity + this.Others;
            this.TotalMensual = total;
            this.TotalBimensual = total * 2;
        }

        private void calcAnuality()
        {
            double cant = this.CauntAnualities * oInternal.annuity;
            double hours= TypesCategories(this.oAcademic.hours);
            this.Anuality = cant * hours;
        }

        private void calcIncomes()
        {
            this.TotalIncome =Convert.ToDouble(oAcademic.price * this.Students);
        }

        public double TypesCategories(int pHours)
        {
            double total = 0;
            switch (pHours)
            {
                case 5: total =0.133333333333333;
                        break;
                case 10:
                        total = 0.266666666666667;
                        break;
                case 15:
                        total =0.4;
                        break;
                case 20:
                        total = 0.533333333333333;
                        break;
                case 25:
                        total = 0.666666666666667;
                        break;
                case 30:
                        total = 0.8;
                        break;
                case 35:
                    total = 0.933333333333333;
                    break;
                case 40:
                    total = 1.066666666666667;
                    break;
            }
            return total;
        }


    }
}