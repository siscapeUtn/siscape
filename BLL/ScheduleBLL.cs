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
    public class ScheduleBLL
    {

        private static ScheduleBLL instace = null;

        public static ScheduleBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ScheduleBLL();
            }
            return instace;
        }

        //
        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTSCHEDULE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                Int32 recordExists = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (recordExists == 0)
                {
                    exists = false;
                }
                else
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return exists;
        }//  exists End

        public Int32 getNextCode()
        {

            String next;
            String oSql = "SP_GETNEXTCODESCHEDULE";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End

        //Insert
        public Int32 insert(Schedule oSchedule)
        {

            String oSql = "SP_INSERTSCHEDULE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oSchedule.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", /*oSchedule.oProgram.code*/1);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oSchedule.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CODDESCRIPTION", oSchedule.codday);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@TYPESHEDULE", oSchedule.typeSchedule);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STARTTIME", oSchedule.startTime);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ENDTIME", oSchedule.endTime);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oSchedule.state);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //Insert end

        //Modify
        public Int32 modify(Schedule oSchedule)
        {

            String oSql = "SP_MODIFYSCHEDULE";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oSchedule.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", /*oSchedule.oProgram.code*/1);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oSchedule.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CODDESCRIPTION", oSchedule.codday);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@TYPESHEDULE", oSchedule.typeSchedule);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STARTTIME", oSchedule.startTime);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ENDTIME", oSchedule.endTime);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oSchedule.state);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end Modify

        //delete
        public Int32 delete(Int32 pCode)
        {

            String oSql = "SP_DELETESCHEDULE";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//delete end

        //get all the teachers
        public List<Schedule> getAll()
        {
            String sql = "SP_GETALLSCHEDULE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Schedule> listSchedule = new List<Schedule>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Schedule oSchedule = new Schedule();
                    Program oProgram = new Program();
                    oSchedule.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.code =  Convert.ToInt32(oDataRow[1].ToString());
                    oSchedule.name = oDataRow[2].ToString();
                    oSchedule.codday = oDataRow[3].ToString();
                    oSchedule.typeSchedule = oDataRow[4].ToString();
                    oSchedule.startTime = Convert.ToDateTime(oDataRow[5].ToString());
                    oSchedule.endTime = Convert.ToDateTime(oDataRow[6].ToString());
                    oSchedule.state = Convert.ToInt32(oDataRow[7].ToString());
                    oProgram.name = oDataRow[8].ToString();
                    oSchedule.oProgram = oProgram;
                    listSchedule.Add(oSchedule);
                }
                return listSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAll

        //obtiene schedule activos
        public List<Schedule> getAllActive()
        {
            String sql = "SP_GETALLSCHEDULEACTIVE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Schedule> listSchedule = new List<Schedule>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Schedule oSchedule = new Schedule();
                    Program oProgram = new Program();
                    oSchedule.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[1].ToString());
                    oSchedule.name = oDataRow[2].ToString();
                    oSchedule.codday = oDataRow[3].ToString();
                    oSchedule.typeSchedule = oDataRow[4].ToString();
                    oSchedule.startTime = Convert.ToDateTime(oDataRow[5].ToString());
                    oSchedule.endTime = Convert.ToDateTime(oDataRow[6].ToString());
                    oSchedule.state = Convert.ToInt32(oDataRow[7].ToString());
                    oSchedule.oProgram = oProgram;
                    listSchedule.Add(oSchedule);
                }
                return listSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//getActive

        //TO the offer academic, cmbteacher by program
        public List<Schedule> getAllActiveByPrgram(Int32 id)
        {
            String sql = "SP_GETALLSCHEDULEACTIVEBYPROGRAM";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", id);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Schedule> listSchedule = new List<Schedule>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Schedule oSchedule = new Schedule();
                    Program oProgram = new Program();
                    oSchedule.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[1].ToString());
                    oSchedule.name = oDataRow[2].ToString();
                    oSchedule.codday = oDataRow[3].ToString();
                    oSchedule.typeSchedule = oDataRow[4].ToString();
                    oSchedule.startTime = Convert.ToDateTime(oDataRow[5].ToString());
                    oSchedule.endTime = Convert.ToDateTime(oDataRow[6].ToString());
                    oSchedule.state = Convert.ToInt32(oDataRow[7].ToString());
                    oProgram.name = oDataRow[8].ToString();
                    oSchedule.oProgram = oProgram;
                    listSchedule.Add(oSchedule);
                }
                return listSchedule;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<Schedule> getAllByPrgrams(Int32 id)
        {
            String sql = "SP_GETALLSCHEDULEBYPROGRAM";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", id);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Schedule> listSchedule = new List<Schedule>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Schedule oSchedule = new Schedule();
                    Program oProgram = new Program();
                    oSchedule.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[1].ToString());
                    oSchedule.name = oDataRow[2].ToString();
                    oSchedule.codday = oDataRow[3].ToString();
                    oSchedule.typeSchedule = oDataRow[4].ToString();
                    oSchedule.startTime = Convert.ToDateTime(oDataRow[5].ToString());
                    oSchedule.endTime = Convert.ToDateTime(oDataRow[6].ToString());
                    oSchedule.state = Convert.ToInt32(oDataRow[7].ToString());
                    oSchedule.oProgram = oProgram;
                    listSchedule.Add(oSchedule);
                }
                return listSchedule;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        //get one especific Schedule
        public Schedule getSchedule(Int32 pCode)
        {
            String sql = "SP_GETSCHEDULE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Schedule oSchedule = new Schedule();
                Program oProgram = new Program();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oSchedule.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[1].ToString());
                    oSchedule.name = oDataRow[2].ToString();
                    oSchedule.codday = oDataRow[3].ToString();
                    oSchedule.typeSchedule = oDataRow[4].ToString();
                    oSchedule.startTime = Convert.ToDateTime(oDataRow[5].ToString());
                    oSchedule.endTime = Convert.ToDateTime(oDataRow[6].ToString());
                    oSchedule.state = Convert.ToInt32(oDataRow[7].ToString());
                    oSchedule.oProgram = oProgram;
                }
                return oSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        //get one especific Schedule Active by code
        public Schedule getSchedulebyCodeActive(Int32 pCode)
        {
            String sql = "SP_GETALLSCHEDULEACTIVEBYPID";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@SCHEDULE_ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Schedule oSchedule = new Schedule();
                Program oProgram = new Program();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oSchedule.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[1].ToString());
                    oSchedule.name = oDataRow[2].ToString();
                    oSchedule.codday = oDataRow[3].ToString();
                    oSchedule.typeSchedule = oDataRow[4].ToString();
                    oSchedule.startTime = Convert.ToDateTime(oDataRow[5].ToString());
                    oSchedule.endTime = Convert.ToDateTime(oDataRow[6].ToString());
                    oSchedule.state = Convert.ToInt32(oDataRow[7].ToString());
                    oSchedule.oProgram = oProgram;
                }
                return oSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        /*Reports*/
        public DataTable getReportSchelude()
        {
            String sql = "SP_REPORTSCHEDULE ";
            SqlCommand oCommand = new SqlCommand(sql);
            // oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            // oCommand.Parameters.AddWithValue("@ID", pCode);
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

    }
}