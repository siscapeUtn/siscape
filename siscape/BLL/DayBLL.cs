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
    public class DayBLL
    {
        private static DayBLL instace = null;

        public static DayBLL getInstance()
        {
            if (instace == null)
            {
                instace = new DayBLL();
            }
            return instace;
        }

        public List<Day> getAll()
        {
            String sql = "SP_GETALLDAY";
          
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery( oCommand );
                List<Day> listDay = new List<Day>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Day oDay = new Day();
                    oDay.code = Convert.ToInt32(oDataRow[0].ToString());
                    oDay.description = oDataRow[1].ToString();
                    listDay.Add(oDay);
                }
                return listDay;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }
    }
}