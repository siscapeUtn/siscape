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
    public class UserSystemBLL
    {

        private static UserSystemBLL instace = null;

        public static UserSystemBLL getInstance()
        {
            if (instace == null)
            {
                instace = new UserSystemBLL();
            }
            return instace;
        }

        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTUSERSYSTEM";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
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
            String oSql = "SP_GETNEXTCODEUSERSYSTEM";

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
        public Int32 insert(Entities.UserSystem oUser)
        {

            String oSql = "SP_INSERTUSERSYSTEM";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@USERSYSTEM_ID", oUser.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", oUser.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oUser.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", oUser.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", oUser.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELLPHONE", oUser.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", oUser.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PASSWORD", oUser.Password);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ROLE_ID", oUser.oRole.Role_Id);
                oCommand.Parameters[8].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oUser.oProgram.code);
                oCommand.Parameters[9].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oUser.state);
                oCommand.Parameters[10].Direction = ParameterDirection.Input;

                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //Insert end

        //Modify
        public Int32 modify(UserSystem oUser)
        {

            String oSql = "SP_MODIFYFUNCTIONARY";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@USERSYSTEM_ID", oUser.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", oUser.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oUser.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", oUser.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", oUser.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELLPHONE", oUser.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", oUser.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PASSWORD", oUser.Password);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ROLE_ID", oUser.oRole.Role_Id);
                oCommand.Parameters[8].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oUser.oProgram.code);
                oCommand.Parameters[9].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oUser.state);
                oCommand.Parameters[10].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end Modify

    }
}