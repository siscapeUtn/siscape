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
    public class ClassRoomsTypeBLL
    {
        private static ClassRoomsTypeBLL instace = null;

        public static ClassRoomsTypeBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ClassRoomsTypeBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODECLASSROOMSTYPE";
           
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
            String oSql = "SP_EXISTCLASSROOMSTYPE";
           
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

        public Int32 insert(ClassRoomsType oClassRoomsType)
        {
            String oSql = "SP_INSERTCLASSROOMSTYPE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oClassRoomsType.code);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oClassRoomsType.description);
                oCommand.Parameters.AddWithValue("@STATE", oClassRoomsType.state);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(ClassRoomsType oClassRoomsType)
        {
            String oSql = "SP_MODIFYCLASSROOMSTYPE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oClassRoomsType.code);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oClassRoomsType.description);
                oCommand.Parameters.AddWithValue("@STATE", oClassRoomsType.state);
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
            String oSql = "SP_DELETECLASSROOMTYPE";

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

        public List<ClassRoomsType> getAll()
        {
            String sql = "SP_GETALLCLASSROOMSTYPE";
           
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoomsType> listClassRoomsType = new List<ClassRoomsType>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoomsType.description = oDataRow[1].ToString();
                    oClassRoomsType.state = Convert.ToInt16(oDataRow[2].ToString());
                    listClassRoomsType.Add(oClassRoomsType);
                }
                return listClassRoomsType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<ClassRoomsType> getAllActived()
        {
            String sql = "SP_GETALLCLASSROOMACTIVESTYPE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoomsType> listClassRoomsType = new List<ClassRoomsType>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoomsType.description = oDataRow[1].ToString();
                    oClassRoomsType.state = Convert.ToInt16(oDataRow[2].ToString());
                    listClassRoomsType.Add(oClassRoomsType);
                }
                return listClassRoomsType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<ClassRoomsType> getAllDesactived()
        {
            String sql = "SP_GETALLCLASSROOMDESACTIVESTYPE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoomsType> listClassRoomsType = new List<ClassRoomsType>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoomsType.description = oDataRow[1].ToString();
                    oClassRoomsType.state = Convert.ToInt16(oDataRow[2].ToString());
                    listClassRoomsType.Add(oClassRoomsType);
                }
                return listClassRoomsType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public ClassRoomsType getClassRoomsType(Int32 pCode)
        {
            String sql = "SP_GETCLASSROOMSTYPE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                ClassRoomsType oClassRoomsType = new ClassRoomsType();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoomsType.description = oDataRow[1].ToString();
                    oClassRoomsType.state = Convert.ToInt16(oDataRow[2].ToString());
                }
                return oClassRoomsType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}