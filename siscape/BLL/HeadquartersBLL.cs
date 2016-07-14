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
    public class HeadquartersBLL
    {
        private static HeadquartersBLL instace = null;

        public static HeadquartersBLL getInstance()
        {
            if (instace == null)
            {
                instace = new HeadquartersBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {

            String next;
            String oSql = "SP_GETNEXTCODEHEADQUARTERS";
           
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
            String oSql = "SP_EXISTPHEADQUARTERS";
    
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 recordExits = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (recordExits == 0)
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

        public Int32 insert(Headquarters pHeadquarters)
        {
            String oSql = "SP_INSERTHEADQUARTERS";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pHeadquarters.code);
                oCommand.Parameters.AddWithValue("@NAME", pHeadquarters.description);
                oCommand.Parameters.AddWithValue("@STATE", pHeadquarters.state);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(Headquarters pHeadquarters)
        {
            String oSql = "SP_MODIFYHEADQUARTERS";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pHeadquarters.code);
                oCommand.Parameters.AddWithValue("@NAME", pHeadquarters.description);
                oCommand.Parameters.AddWithValue("@STATE", pHeadquarters.state);
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
            String oSql = "SP_DELETEHEADQUATERS";

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

        public List<Headquarters> getAll()
        {
            String sql = "SP_GETALLHEADQUARTERS";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Headquarters> listHeadquarters = new List<Headquarters>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Headquarters oHeadquarters = new Headquarters();
                    oHeadquarters.code = Convert.ToInt32(oDataRow[0].ToString());
                    oHeadquarters.description = oDataRow[1].ToString();
                    oHeadquarters.state = Convert.ToInt32(oDataRow[2].ToString());
                    listHeadquarters.Add(oHeadquarters);
                }
                return listHeadquarters;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<Headquarters> getAllActive()
        {
            String sql = "SP_GETALLHEADQUARTERSACTIVE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Headquarters> listHeadquarters = new List<Headquarters>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Headquarters oHeadquarters = new Headquarters();
                    oHeadquarters.code = Convert.ToInt32(oDataRow[0].ToString());
                    oHeadquarters.description = oDataRow[1].ToString();
                    oHeadquarters.state = Convert.ToInt32(oDataRow[2].ToString());
                    listHeadquarters.Add(oHeadquarters);
                }
                return listHeadquarters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Headquarters getHeadquarters(Int32 pCode)
        {
            String sql = "SP_GETPHEADQUARTERS";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Headquarters oHeadquarters = new Headquarters();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oHeadquarters.code = Convert.ToInt32(oDataRow[0].ToString());
                    oHeadquarters.description = oDataRow[1].ToString();
                    oHeadquarters.state = Convert.ToInt32(oDataRow[2].ToString());
                }
                return oHeadquarters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}