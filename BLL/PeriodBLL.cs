using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAL;
using Entities;

namespace BLL
{
    public class PeriodBLL
    {
        private static PeriodBLL instace = null;

        public static PeriodBLL getInstance()
        {
            if (instace == null)
            {
                instace = new PeriodBLL();
            }
            return instace;
        }

        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTSPERIOD";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
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
        }

        public Int32 getNextCode()
        {

            String next;
            String oSql = "SP_GETNEXTCODEPERIOD";
           
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

        public Int32 insert(Period oPeriod)
        {

            String oSql = "SP_INSERTPERIOD";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", oPeriod.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oPeriod.name);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STARTDATE", oPeriod.startDate);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@FINALDATE", oPeriod.finalDate);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MODALITY", oPeriod.oPeriodType.code);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oPeriod.state);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(Period oPeriod)
        {

            String oSql = "SP_MODIFYPERIOD";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", oPeriod.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oPeriod.name);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STARTDATE", oPeriod.startDate);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@FINALDATE", oPeriod.finalDate);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MODALITY", oPeriod.oPeriodType.code);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oPeriod.state);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 delete(Int32 pCode)
        {

            String oSql = "SP_DELETEPERIOD";

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
        }

        public List<Period> getAll()
        {
            String sql = "SP_GETALLPERIOD";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Period> listProgram = new List<Period>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Period oPeriod = new Period();
                    // PeriodType oPeridType = new PeriodType();
                    oPeriod.code = Convert.ToInt32(oDataRow[0].ToString());
                    oPeriod.name = oDataRow[1].ToString();
                    oPeriod.startDate = Convert.ToDateTime(oDataRow[2].ToString());
                    oPeriod.finalDate = Convert.ToDateTime(oDataRow[3].ToString());
                    oPeriod.oPeriodType = new PeriodType();
                    oPeriod.oPeriodType.description = oDataRow[4].ToString();
                    oPeriod.state = Convert.ToInt16(oDataRow[5].ToString());
                    listProgram.Add(oPeriod);
                }
                return listProgram;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        //obtiene periodos activos
        public List<Period> getAllActive()
        {
            String sql = "SP_GETALLPERIODACTIVE";
           
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Period> listProgram = new List<Period>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Period oPeriod = new Period();
                    // PeriodType oPeridType = new PeriodType();
                    oPeriod.code = Convert.ToInt32(oDataRow[0].ToString());
                    oPeriod.name = oDataRow[1].ToString();
                    oPeriod.startDate = Convert.ToDateTime(oDataRow[2].ToString());
                    oPeriod.finalDate = Convert.ToDateTime(oDataRow[3].ToString());
                    oPeriod.oPeriodType = new PeriodType();
                    oPeriod.oPeriodType.description = oDataRow[4].ToString();
                    oPeriod.state = Convert.ToInt16(oDataRow[5].ToString());
                    listProgram.Add(oPeriod);
                }
                return listProgram;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Period getPeriod(Int32 pCode)
        {
            String sql = "SP_GETPERIOD";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Period oPeriod = new Period();
                //PeriodType oPeridType = new PeriodType();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oPeriod.code = Convert.ToInt32(oDataRow[0].ToString());
                    oPeriod.name = oDataRow[1].ToString();
                    oPeriod.startDate = Convert.ToDateTime(oDataRow[2].ToString());
                    oPeriod.finalDate = Convert.ToDateTime(oDataRow[3].ToString());
                    oPeriod.oPeriodType = new PeriodType();
                    oPeriod.oPeriodType.code = Convert.ToInt32(oDataRow[4].ToString());
                    oPeriod.state = Convert.ToInt16(oDataRow[5].ToString());
                }
                return oPeriod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
        //brinsg last period code
        public Int32 getLasPeriod()
        {

            String next;
            String oSql = "SP_GET_LAST_PERIOD";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                next = DAO.getInstance().executeQueryScalar(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End
    }
}