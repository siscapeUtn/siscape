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
    public class FunctionaryBLL
    {

        private static FunctionaryBLL instace = null;

        public static FunctionaryBLL getInstance()
        {
            if (instace == null)
            {
                instace = new FunctionaryBLL();
            }
            return instace;
        }

        //
        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTSFUNCTIONARY";
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
        }//  exists End

        public Int32 getNextCode()
        {

            String next;
            String oSql = "SP_GETNEXTCODEFUNCTIONARY";

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
        public Int32 insert(Functionary oFunctionary)
        {

            String oSql = "SP_INSERTFUNCTIONARY";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@FUNCTIONARY_ID", oFunctionary.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", oFunctionary.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oFunctionary.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", oFunctionary.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", oFunctionary.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELLPHONE", oFunctionary.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", oFunctionary.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oFunctionary.state);
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
        public Int32 modify(Functionary oFunctionary)
        {

            String oSql = "SP_MODIFYFUNCTIONARY";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@FUNCTIONARY_ID", oFunctionary.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", oFunctionary.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oFunctionary.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", oFunctionary.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", oFunctionary.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELLPHONE", oFunctionary.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", oFunctionary.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oFunctionary.state);
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

            String oSql = "SP_DELETEFUNCTIONARY";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@FUNCTIONARY_ID", pCode);
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
        public List<Functionary> getAll()
        {
            String sql = "SP_GETALLFUNCTIONARY";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Functionary> listFunctionary = new List<Functionary>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Functionary oFunctionary = new Functionary();
                    oFunctionary.code = Convert.ToInt32(oDataRow[0].ToString());
                    oFunctionary.id = oDataRow[1].ToString();
                    oFunctionary.name = oDataRow[2].ToString();
                    oFunctionary.lastName = oDataRow[3].ToString();
                    oFunctionary.homePhone = oDataRow[4].ToString();
                    oFunctionary.cellPhone = oDataRow[5].ToString();
                    oFunctionary.email = oDataRow[6].ToString();
                    oFunctionary.state = Convert.ToInt16(oDataRow[7].ToString());
                    listFunctionary.Add(oFunctionary);
                }
                return listFunctionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAll

        //obtiene periodos activos
        public List<Functionary> getAllActive()
        {
            String sql = "SP_GETALLFUNCTIONARYACTIVED";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Functionary> listFunctionary = new List<Functionary>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Functionary oFunctionary = new Functionary();
                    oFunctionary.code = Convert.ToInt32(oDataRow[0].ToString());
                    oFunctionary.id = oDataRow[1].ToString();
                    oFunctionary.name = oDataRow[2].ToString();
                    oFunctionary.lastName = oDataRow[3].ToString();
                    oFunctionary.homePhone = oDataRow[4].ToString();
                    oFunctionary.cellPhone = oDataRow[5].ToString();
                    oFunctionary.email = oDataRow[6].ToString();
                    oFunctionary.state = Convert.ToInt16(oDataRow[7].ToString());
                    listFunctionary.Add(oFunctionary);
                }
                return listFunctionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//getActive

        //get one especific teacher
        public Functionary getFunctionary(Int32 pCode)
        {
            String sql = "SP_GETFUNCTIONARY";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Functionary oFunctionary = new Functionary();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oFunctionary.code = Convert.ToInt32(oDataRow[0].ToString());
                    oFunctionary.id = oDataRow[1].ToString();
                    oFunctionary.name = oDataRow[2].ToString();
                    oFunctionary.lastName = oDataRow[3].ToString();
                    oFunctionary.homePhone = oDataRow[4].ToString();
                    oFunctionary.cellPhone = oDataRow[5].ToString();
                    oFunctionary.email = oDataRow[6].ToString();
                    oFunctionary.state = Convert.ToInt16(oDataRow[7].ToString());
                }
                return oFunctionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }



    }
}