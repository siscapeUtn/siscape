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
    public class ExternalDesignationBLL
    {
        private static ExternalDesignationBLL instace = null;

        public static ExternalDesignationBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ExternalDesignationBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODEEXTERNAL_DESIGNATION";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                next = DAO.getInstance().executeQueryScalar(oCommand);
                if (next.Equals(""))
                {
                    next = "1";
                }
                else
                {
                    next = Convert.ToString(Convert.ToInt32(next) + 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End

        public Boolean exists(Int32 pCode)
        {
            Boolean existe;
            String oSql = "SP_EXISTEXTERNAL_DESIGNATION";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", pCode);
            try
            {
                Int32 existencia = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (existencia == 0)
                {
                    existe = false;
                }
                else
                {
                    existe = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return existe;
        }

        public Int32 insert(ExternalDesignation oExternal)
        {

            String oSql = "SP_INSERTEXTERNAL_DESIGNATION";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", oExternal.code);
            oCommand.Parameters.AddWithValue("@TEACHER_ID", oExternal.oTeacher.code);
            oCommand.Parameters.AddWithValue("@LOCATION", oExternal.location);
            oCommand.Parameters.AddWithValue("@POSITION", oExternal.position);
            oCommand.Parameters.AddWithValue("@HOURS", oExternal.hours);
            oCommand.Parameters.AddWithValue("@INITIAL_DATE", oExternal.initial_day);
            oCommand.Parameters.AddWithValue("@FINAL_DATE", oExternal.final_day);
            try
            {
                DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception)
            {
                throw;
            }



            for (int i = 0; i < oExternal.journeys.Count; i++)
            {
                int Codejourney = JourneyBLL.getInstance().getNextCode();
                String oSql2 = "SP_INSERTJOURNEY";
                SqlCommand oCommand2 = new SqlCommand(oSql2);
                oCommand2.CommandType = CommandType.StoredProcedure;
                oCommand2.Parameters.AddWithValue("@ID", Codejourney);
                oCommand2.Parameters.AddWithValue("@DAY_ID", oExternal.journeys[i].day.code);
                oCommand2.Parameters.AddWithValue("@START", oExternal.journeys[i].start);
                oCommand2.Parameters.AddWithValue("@FINISH", oExternal.journeys[i].finish);
                try
                {
                    DAO.getInstance().executeSQL(oCommand2);
                }
                catch (Exception)
                {
                    throw;
                }

                String oSql3 = "SP_INSERTEXTERNALDESIGNATION_JOURNEY";
                SqlCommand oCommand3 = new SqlCommand(oSql3);
                oCommand3.CommandType = CommandType.StoredProcedure;
                oCommand3.Parameters.AddWithValue("@IDEXTERNAL", oExternal.code);
                oCommand3.Parameters.AddWithValue("@IDJOURNEY", Codejourney);
                try
                {
                    DAO.getInstance().executeSQL(oCommand3);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return 0;
        }

        public Int32 delete(Int32 pCode) 
        {
            String oSql = "SP_DELETE_EXT_DESIG_JOURNEY";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", pCode);
            Int32 result = 0;
            try
            {
                result = DAO.getInstance().executeSQL(oCommand);
                deleteExternalDesignation(pCode);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return result;
        }

        public Int32 deleteExternalDesignation(Int32 pCode)
        {
            String oSql = "SP_DELETE_EXTERNAL_DESIGNATION";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", pCode);
            try
            {
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<ExternalDesignation> getAll()
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
    
        public ExternalDesignation getExternalDesignation(Int32 pCode)
        {
            String oSql = "SP_GETABYCODE_EXTERNAL_DESIGNATION";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", pCode);
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                ExternalDesignation oExternalDesignation = new ExternalDesignation();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oExternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oExternalDesignation.location = oDataRow[1].ToString();
                    oExternalDesignation.position = oDataRow[2].ToString();
                    oExternalDesignation.hours = Convert.ToInt32(oDataRow[3].ToString());
                    oExternalDesignation.initial_day = Convert.ToDateTime(oDataRow[4].ToString());
                    oExternalDesignation.final_day = Convert.ToDateTime(oDataRow[5].ToString());
                }
                return oExternalDesignation;

            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<Teacher> getAllActived()
        {
            String sql = "sp_teacherActive";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Teacher> listTeacher = new List<Teacher>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Teacher oTeacher = new Teacher();
                    oTeacher.id = oDataRow[0].ToString();
                    oTeacher.name = oDataRow[1].ToString();

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

        public List<ExternalDesignation> getExternalDesignationByTeacher(Int32 id)
        {
            String sql = "SP_EXTERNAL_DESIGNATION_BY_TEACHER";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", id);
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ExternalDesignation> listExternalDesignation = new List<ExternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ExternalDesignation oExternalDesignation = new ExternalDesignation();
                    Teacher oTeacher = new Teacher();
                    oExternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oTeacher.name = oDataRow[1].ToString();
                    oTeacher.id = id.ToString();
                    oExternalDesignation.location = oDataRow[2].ToString();
                    oExternalDesignation.position = oDataRow[3].ToString();
                    oExternalDesignation.hours = Convert.ToInt32(oDataRow[4].ToString());
                    oExternalDesignation.initial_day = Convert.ToDateTime(oDataRow[5].ToString());
                    oExternalDesignation.final_day = Convert.ToDateTime(oDataRow[6].ToString());
                    oExternalDesignation.oTeacher = oTeacher;
                    listExternalDesignation.Add(oExternalDesignation);
                }
                return listExternalDesignation;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }


        public ExternalDesignation getExternal(int code)
        {
            String sql = "SP_EXTERNAL_DESIGNATION_BY_CODE";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", code);

            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                ExternalDesignation oExternalDesignation = new ExternalDesignation();
                Teacher oTeacher = new Teacher();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {

                    oExternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oTeacher.id = oDataRow[1].ToString();
                    oExternalDesignation.location = oDataRow[2].ToString();
                    oExternalDesignation.position = oDataRow[3].ToString();
                    oExternalDesignation.hours = Convert.ToInt32(oDataRow[4].ToString());
                    oExternalDesignation.initial_day = Convert.ToDateTime(oDataRow[5].ToString());
                    oExternalDesignation.final_day = Convert.ToDateTime(oDataRow[6].ToString());
                    oExternalDesignation.oTeacher = oTeacher;

                }
                String sql2 = "SP_EXTERNAL_DESIGNATION_BY_CODE_JOURNEY";
                SqlCommand oCommand2 = new SqlCommand(sql2);
                oCommand2.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand2.Parameters.AddWithValue("@ID", code);

                try
                {
                    DataTable oDataTable1 = DAO.getInstance().executeQuery(oCommand2);
                    List<Journey> listJourney = new List<Journey>();
                    foreach (DataRow oDataRow in oDataTable1.Rows)
                    {
                        Journey oJourney = new Journey();
                        Day oday = new Day();
                        oJourney.code = Convert.ToInt32(oDataRow[0].ToString());
                        oday.code = Convert.ToInt32(oDataRow[1].ToString());
                        oday.description = oDataRow[2].ToString();
                        oJourney.start = oDataRow[3].ToString();
                        oJourney.finish = oDataRow[4].ToString();
                        oJourney.day = oday;
                        listJourney.Add(oJourney);
                    }
                    oExternalDesignation.journeys = listJourney;
                }
                catch (Exception)
                {
                    throw;
                }



                return oExternalDesignation;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        /*Este metodo trae las horas restantes del profesor*/
        public Int32 getHours(Int32 id)
        {
            String hours;
            String oSql = "SP_HOURS_REMAINING";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@TEACHER", id);
            try
            {
                hours = DAO.getInstance().executeQueryScalar(oCommand);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return Convert.ToInt32(hours);
        } 
    }
}