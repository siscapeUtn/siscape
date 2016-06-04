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
    public class LocationBLL
    {
        private static LocationBLL instace = null;

        public static LocationBLL getInstance()
        {
            if (instace == null)
            {
                instace = new LocationBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODELOCATION";
            
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

        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTPLOCATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
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

        public Int32 insert(Location oLocation)
        {
            String oSql = "SP_INSERTLOCATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oLocation.code);
                oCommand.Parameters.AddWithValue("@BUILDING", oLocation.building);
                oCommand.Parameters.AddWithValue("@MODULE", oLocation.module);
                oCommand.Parameters.AddWithValue("@IDHEADQUARTERS", oLocation.oHeadquarters.code);
                oCommand.Parameters.AddWithValue("@STATE", oLocation.State);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(Location oLocation)
        {
            String oSql = "SP_MODIFYLOCATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oLocation.code);
                oCommand.Parameters.AddWithValue("@BUILDING", oLocation.building);
                oCommand.Parameters.AddWithValue("@MODULE", oLocation.module);
                oCommand.Parameters.AddWithValue("@IDHEADQUARTERS", oLocation.oHeadquarters.code);
                oCommand.Parameters.AddWithValue("@STATE", oLocation.State);
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
            String oSql = "SP_DELETELOCATION";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<Location> getAll()
        {
            String sql = "SP_GETALLLOCATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Location> listLocation = new List<Location>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    oLocation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oLocation.building = oDataRow[1].ToString();
                    oLocation.module = oDataRow[2].ToString();
                    oHeadquarters.code = Convert.ToInt32(oDataRow[3].ToString());
                    oHeadquarters.description = oDataRow[4].ToString();
                    oLocation.State = Convert.ToInt32(oDataRow[5].ToString());
                    oLocation.oHeadquarters = oHeadquarters;
                    listLocation.Add(oLocation);
                }
                return listLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<Location> getAllActive()
        {
            String sql = "SP_GETALLLOCATIONACTIVE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Location> listLocation = new List<Location>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    oLocation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oLocation.building = oDataRow[1].ToString();
                    oLocation.module = oDataRow[2].ToString();
                    oHeadquarters.code = Convert.ToInt32(oDataRow[3].ToString());
                    oHeadquarters.description = oDataRow[4].ToString();
                    oLocation.State = Convert.ToInt32(oDataRow[5].ToString());
                    oLocation.oHeadquarters = oHeadquarters;
                    listLocation.Add(oLocation);
                }
                return listLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Location getLocation(Int32 pCode)
        {
            String sql = "SP_GETPLOCATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Location oLocation = new Location();
                Headquarters oHeadquarters = new Headquarters();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oLocation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oLocation.building = oDataRow[1].ToString();
                    oLocation.module = oDataRow[2].ToString();
                    oHeadquarters.code = Convert.ToInt32(oDataRow[3].ToString());
                    oHeadquarters.description = oDataRow[4].ToString();
                    oLocation.State = Convert.ToInt32(oDataRow[5].ToString());
                    oLocation.oHeadquarters = oHeadquarters;
                }
                return oLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}