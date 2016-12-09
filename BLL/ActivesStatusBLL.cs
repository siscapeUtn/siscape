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

    }
}