using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BLL
{
    public class OpeningJustificationBLL
    {

        private static OpeningJustificationBLL instace = null;

        public static OpeningJustificationBLL getInstance()
        {
            if (instace == null)
            {
                instace = new OpeningJustificationBLL();
            }
            return instace;
        }

        public OpeningJustification getJustification(Int32 pCode)
        {
            String sql = "SP_OPENINGJUSTIFCATION";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                OpeningJustification oJustification = new OpeningJustification();
                AcademicOffer oAcademic = new AcademicOffer();
                Teacher oTeacher = new Teacher();
                Course oCourse = new Course();
                InternalDesignation oInternal = new InternalDesignation();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oAcademic.code = Convert.ToInt32(oDataRow[0].ToString());
                    oTeacher.name = oDataRow[1].ToString();
                    oTeacher.lastName = oDataRow[2].ToString();
                    oInternal.description= oDataRow[3].ToString();
                    oInternal.baseSalary = Convert.ToDouble(oDataRow[4].ToString());
                    oInternal.annuity = Convert.ToDouble(oDataRow[5].ToString());
                    oAcademic.price = Convert.ToDecimal(oDataRow[6].ToString());
                    oAcademic.hours = Convert.ToInt32(oDataRow[7].ToString());
                    oCourse.description= oDataRow[8].ToString();
                    oInternal.code= Convert.ToInt32(oDataRow[9].ToString());
                    oAcademic.oteacher = oTeacher;
                    oAcademic.oCourse = oCourse;
                    oJustification.Anuality = 0;
                    oJustification.CauntAnualities = 0;
                    oJustification.CCSS = 0;
                    oJustification.Diference = 0;
                    oJustification.Others = 0;
                    oJustification.publicity = 0;
                    oJustification.Salary = 0;
                    oJustification.Students = 0;
                    oJustification.TotalBimensual = 0;
                    oJustification.TotalIncome = 0;
                    oJustification.TotalMensual = 0;
                    oJustification.oInternal = oInternal;
                    oJustification.oAcademic = oAcademic;

                }
                return oJustification;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}