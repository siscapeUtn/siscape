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
            Encrypt en = new Encrypt();
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
                oCommand.Parameters.AddWithValue("@PASSWORD", en.Encriptar(oUser.Password));
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

        public Int32 delete(Int32 pCode)
        {

            String oSql = "SP_DELETEUSER";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@USERSYSTEM_ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//delete end

        //get all the Users System
        public List<UserSystem> getAll()
        {
            String sql = "SP_GETALLUSERSYTEM";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<UserSystem> listUserSystem = new List<UserSystem>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    UserSystem oUserSystem = new UserSystem();
                    Program oProgram = new Program();
                    Role oRole = new Role();
                    oUserSystem.code = Convert.ToInt32(oDataRow[0].ToString());
                    oUserSystem.id = oDataRow[1].ToString();
                    oUserSystem.name = oDataRow[2].ToString();
                    oUserSystem.lastName = oDataRow[3].ToString();
                    oProgram.code = Convert.ToInt32(oDataRow[4].ToString());
                    oProgram.name = oDataRow[5].ToString();
                    oUserSystem.homePhone = oDataRow[6].ToString();
                    oUserSystem.cellPhone = oDataRow[7].ToString();
                    oUserSystem.email = oDataRow[8].ToString();
                    oRole.Role_Id = Convert.ToInt32(oDataRow[9].ToString());
                    oRole.Description = oDataRow[10].ToString();
                    oUserSystem.state = Convert.ToInt16(oDataRow[11].ToString());
                    oUserSystem.oProgram = oProgram;
                    oUserSystem.oRole = oRole;
                    listUserSystem.Add(oUserSystem);
                }
                return listUserSystem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAll


        //get one especific UserSytem
        public UserSystem getUserSystem(Int32 pCode)
        {
            String sql = "SP_GETUSERSYSTEM";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                UserSystem oUserSystem = new UserSystem();
                Program oProgram = new Program();
                Role oRole = new Role();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oUserSystem.code = Convert.ToInt32(oDataRow[0].ToString());
                    oUserSystem.id = oDataRow[1].ToString();
                    oUserSystem.name = oDataRow[2].ToString();
                    oUserSystem.lastName = oDataRow[3].ToString();
                    oProgram.code = Convert.ToInt32(oDataRow[4].ToString());
                    oProgram.name = oDataRow[5].ToString();
                    oUserSystem.homePhone = oDataRow[6].ToString();
                    oUserSystem.cellPhone = oDataRow[7].ToString();
                    oUserSystem.email = oDataRow[8].ToString();
                    oRole.Role_Id = Convert.ToInt32(oDataRow[9].ToString());
                    oRole.Description = oDataRow[10].ToString();
                    oUserSystem.state = Convert.ToInt16(oDataRow[11].ToString());
                    oUserSystem.oProgram = oProgram;
                    oUserSystem.oRole = oRole;
                }
                return oUserSystem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }



        public UserSystem verify_User(string pMail, string pPassword)
        {
                Encrypt en = new Encrypt();
                String oSql = "SP_VERIFYUSERSYSTEM";
                    try
                    {
                string encrypt = en.Encriptar(pPassword);
                        SqlCommand oCommand = new SqlCommand(oSql);
                        oCommand.CommandType = CommandType.StoredProcedure;
                        oCommand.Parameters.AddWithValue("@MAIL", pMail);
                        oCommand.Parameters[0].Direction = ParameterDirection.Input;
                        oCommand.Parameters.AddWithValue("@PASSWORD", encrypt);
                        oCommand.Parameters[1].Direction = ParameterDirection.Input;
                        DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                        UserSystem oUserSystem = new UserSystem();
                        Program oProgram = new Program();
                        Role oRole = new Role();

                        foreach (DataRow oDataRow in oDataTable.Rows)
                        {
                            oUserSystem.code = Convert.ToInt32(oDataRow[0].ToString());
                            oUserSystem.id = oDataRow[1].ToString();
                            oUserSystem.name = oDataRow[2].ToString();
                            oUserSystem.lastName = oDataRow[3].ToString();
                            oProgram.code = Convert.ToInt32(oDataRow[4].ToString());
                            oProgram.name = oDataRow[5].ToString();
                            oUserSystem.homePhone = oDataRow[6].ToString();
                            oUserSystem.cellPhone = oDataRow[7].ToString();
                            oUserSystem.email = oDataRow[8].ToString();
                            oRole.Role_Id = Convert.ToInt32(oDataRow[9].ToString());
                            oRole.Description = oDataRow[10].ToString();
                            oUserSystem.state = Convert.ToInt16(oDataRow[11].ToString());
                            oUserSystem.oProgram = oProgram;
                            oUserSystem.oRole = oRole;
                        }
                        return oUserSystem;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally { }
         }

        public UserSystem getUserSystemByMail(String pMail)
        {
            Encrypt en = new Encrypt();
            String sql = "SP_GETUSERSYSTEMBYMAIL";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@MAIL", pMail);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                UserSystem oUserSystem = new UserSystem();
                
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oUserSystem.email = oDataRow[0].ToString();
                    oUserSystem.Password = oDataRow[1].ToString();
                }
                return oUserSystem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}