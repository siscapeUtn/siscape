using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BLL
{
    public class JourneyBLL
    {
        private static JourneyBLL instace = null;

        public static JourneyBLL getInstance()
        {
            if (instace == null)
            {
                instace = new JourneyBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODEJOURNEY";
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
            String oSql = "SP_EXISTJOURMEY";
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
    }
}