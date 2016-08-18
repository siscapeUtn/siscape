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
    public class ReportBLL
    {

        private static ReportBLL instace = null;

        public static ReportBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ReportBLL();
            }
            return instace;
        }

        public List<Teacher> reportTeacher()
        {
            String sql = "SP_REPORT_TEACHER";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Teacher> listTeacher = new List<Teacher>();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Teacher oTeacher = new Teacher();
                    InternalDesignation oInternalDesignation = new InternalDesignation();
                    oTeacher.code = Convert.ToInt32(oDataRow[0].ToString());
                    oTeacher.id = oDataRow[1].ToString();
                    oTeacher.name = oDataRow[2].ToString();
                    oTeacher.lastName = oDataRow[3].ToString();
                    oTeacher.homePhone = oDataRow[4].ToString();
                    oTeacher.cellPhone = oDataRow[5].ToString();
                    oTeacher.email = oDataRow[6].ToString();
                    oInternalDesignation.code = Convert.ToInt32(oDataRow[7].ToString());
                    oInternalDesignation.description = oDataRow[8].ToString();
                    oTeacher.state = Convert.ToInt16(oDataRow[9].ToString());
                    oTeacher.Position = oInternalDesignation;
                    listTeacher.Add(oTeacher);
                }

                return listTeacher;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<ExternalDesignation> reportExternalDesignation()
        {
            String sql = "SP_GETALLEXTERNAL_DESIGNATION";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ExternalDesignation> listInternalDesignation = new List<ExternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ExternalDesignation oExternalDesignation = new ExternalDesignation();
                    oExternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oExternalDesignation.oTeacher = new Teacher();
                    oExternalDesignation.oTeacher.name = oDataRow[1].ToString();
                    oExternalDesignation.oTeacher.id = oDataRow[2].ToString();
                    oExternalDesignation.position = oDataRow[3].ToString();
                    oExternalDesignation.location = oDataRow[4].ToString();
                    oExternalDesignation.hours = Convert.ToInt32(oDataRow[5].ToString());
                    oExternalDesignation.initial_day = Convert.ToDateTime(oDataRow[6].ToString());
                    oExternalDesignation.final_day = Convert.ToDateTime(oDataRow[7].ToString());

                    listInternalDesignation.Add(oExternalDesignation);
                }
                return listInternalDesignation;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public int oDataRow { get; set; }
    }
}