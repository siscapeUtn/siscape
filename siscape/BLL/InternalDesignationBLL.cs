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
    public class InternalDesignationBLL
    {
        private static InternalDesignationBLL instace = null;

        public static InternalDesignationBLL getInstance()
        {
            if (instace == null)
            {
                instace = new InternalDesignationBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            try
            {
                String oSql = "SP_GETNEXTCODEINTERNAL_DESIGNATION";
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
            try
            {
                String oSql = "SP_EXISTINTERNAL_DESIGNATION";
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 recordsExists = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (recordsExists == 0)
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

        public Int32 insert(InternalDesignation pInternalDesignation)
        {
            String oSql = "SP_INSERTINTERNAL_DESIGNATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pInternalDesignation.code);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", pInternalDesignation.description);
                oCommand.Parameters.AddWithValue("@SALARY", pInternalDesignation.baseSalary);
                oCommand.Parameters.AddWithValue("@ANNUITY", pInternalDesignation.annuity);
                oCommand.Parameters.AddWithValue("@STATE", pInternalDesignation.state);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(InternalDesignation pInternalDesignation)
        {
            String oSql = "SP_MODIFYINTERNAL_DESIGNATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pInternalDesignation.code);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", pInternalDesignation.description);
                oCommand.Parameters.AddWithValue("@SALARY", pInternalDesignation.baseSalary);
                oCommand.Parameters.AddWithValue("@ANNUITY", pInternalDesignation.annuity);
                oCommand.Parameters.AddWithValue("@STATE", pInternalDesignation.state);
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
            String oSql = "SP_DELETEINTERNALDESIGNATION";

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

        public List<InternalDesignation> getAll()
        {
            String sql = "SP_GETALLINTERNAL_DESIGNATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<InternalDesignation> listInternalDesignation = new List<InternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    InternalDesignation oInternalDesignation = new InternalDesignation();
                    oInternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oInternalDesignation.description = oDataRow[1].ToString();
                    oInternalDesignation.baseSalary = Convert.ToDouble(oDataRow[2].ToString());
                    oInternalDesignation.annuity = Convert.ToDouble(oDataRow[3].ToString());
                    oInternalDesignation.state = Convert.ToInt16(oDataRow[4].ToString());
                    listInternalDesignation.Add(oInternalDesignation);
                }
                return listInternalDesignation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<InternalDesignation> getAllActive()
        {
            String sql = "SP_GETALLINTERNAL_DESIGNATIONACTIVE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<InternalDesignation> listInternalDesignation = new List<InternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    InternalDesignation oInternalDesignation = new InternalDesignation();
                    oInternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oInternalDesignation.description = oDataRow[1].ToString();
                    oInternalDesignation.baseSalary = Convert.ToDouble(oDataRow[2].ToString());
                    oInternalDesignation.annuity = Convert.ToDouble(oDataRow[3].ToString());
                    oInternalDesignation.state = Convert.ToInt16(oDataRow[4].ToString());
                    listInternalDesignation.Add(oInternalDesignation);
                }
                return listInternalDesignation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<InternalDesignation> getAllDesactive()
        {
            String sql = "SP_GETALLINTERNAL_DESIGNATIONDESACTIVE";
           
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<InternalDesignation> listInternalDesignation = new List<InternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    InternalDesignation oInternalDesignation = new InternalDesignation();
                    oInternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oInternalDesignation.description = oDataRow[1].ToString();
                    oInternalDesignation.baseSalary = Convert.ToDouble(oDataRow[2].ToString());
                    oInternalDesignation.annuity = Convert.ToDouble(oDataRow[3].ToString());
                    oInternalDesignation.state = Convert.ToInt16(oDataRow[4].ToString());
                    listInternalDesignation.Add(oInternalDesignation);
                }
                return listInternalDesignation;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public InternalDesignation getInternalDesignation(Int32 pCode)
        {
            String oSql = "SP_GETINTERNAL_DESIGNATION";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                InternalDesignation oInternalDesignation = new InternalDesignation();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oInternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oInternalDesignation.description = oDataRow[1].ToString();
                    oInternalDesignation.baseSalary = Convert.ToDouble(oDataRow[2].ToString());
                    oInternalDesignation.annuity = Convert.ToDouble(oDataRow[3].ToString());
                    oInternalDesignation.state = Convert.ToInt16(oDataRow[4].ToString());
                }
                return oInternalDesignation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}