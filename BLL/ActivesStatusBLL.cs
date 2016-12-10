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
    public class ActivesStatusBLL
    {
        private static ActivesStatusBLL instace = null;

        public static ActivesStatusBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ActivesStatusBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODEACTIVESSTATUS";

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
        }

        public Boolean exists(ActivesStatus ostatus)
        {
            Boolean exists;
            String oSql = "SP_EXISTACTIVESSTATUS";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", ostatus.activesSatus_ID);
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

        public Boolean existsName(string description)
        {
            Boolean exists;
            String oSql = "SP_EXISTNAMEACTIVESSTATUS";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", description);
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


        public Int32 insert(ActivesStatus ostatus)
        {
            String oSql = "SP_INSERTSTATUSACTIVE";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", ostatus.activesSatus_ID);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", ostatus.description);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(ActivesStatus ostatus)
        {
            String oSql = "SP_MODIFYSTATUSACTIVE";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", ostatus.activesSatus_ID);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", ostatus.description);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<ActivesStatus> getAll()
        {
            String sql = "SP_GETALLACTIVESSTATUS";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ActivesStatus> listActivesStatus = new List<ActivesStatus>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ActivesStatus oActivites = new ActivesStatus();

                    oActivites.activesSatus_ID = Convert.ToInt32(oDataRow[0].ToString());
                    oActivites.description = oDataRow[1].ToString();
            
                    listActivesStatus.Add(oActivites);
                }
                return listActivesStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public ActivesStatus getActivesStatus(int code)
        {
            String sql = "SP_GETACTIVESTATUSBYCODE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", code);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                ActivesStatus oActivites = new ActivesStatus();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oActivites.activesSatus_ID = Convert.ToInt32(oDataRow[0].ToString());
                    oActivites.description = oDataRow[1].ToString();

                }
                return oActivites;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}