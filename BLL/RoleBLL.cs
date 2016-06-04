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
    public class RoleBLL
    {
        private static RoleBLL instace = null;

        public static RoleBLL getInstance()
        {
            if (instace == null)
            {
                instace = new RoleBLL();
            }
            return instace;
        }//End getInstance()


        public Int32 getNextCode()
        {
            String next; //To contain the next code
            String oSql = "SP_GETNEXTCODEROLE"; //Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                next = DAO.getInstance().executeQueryScalar(oCommand);//To execute query
                if (next.Equals(""))
                {
                    next = "1";
                }
                else
                {
                    next = Convert.ToString(Convert.ToInt32(next) + 1);
                }
                return Convert.ToInt32(next);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } // getNextCode End


        public Boolean exists(Int32 pCode)
        {
            Boolean exist;
            String oSql = "SP_EXISTROLE";//Stored procedure name
            try
            {

                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 toExist = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand)); //To execute query
                if (toExist == 0)
                {
                    exist = false;
                }
                else
                {
                    exist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return exist;
        }//End exists

        public Boolean existsName(String pDescription)
        {
            Boolean exist;
            String oSql = "SP_EXISTROLENAME";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", pDescription);
                Int32 toExist = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand)); //To execute query
                if (toExist == 0)
                {
                    exist = false;
                }
                else
                {
                    exist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return exist;
        }//End exists

        //Method to insert a new program in the database
        public Int32 insert(Role oRole)
        {
            Int32 num=0;
            String oSql = "SP_INSERTROLE"; //Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oRole.Role_Id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oRole.Description);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oRole.State);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                num= DAO.getInstance().executeSQL(oCommand); //To execute insert
                if (num > 0)
                {
                    insertRoleModule(oRole);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return num;
        }//End insert()

        //Method to update a program in the database
        public Int32 modify(Role oRole)
        {
            Int32 num = 0;
            String oSql = "SP_MODIFYROLE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oRole.Role_Id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oRole.Description);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oRole.State);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                num= DAO.getInstance().executeSQL(oCommand);////To execute update
                if (num > 0)
                {
                    insertRoleModule(oRole);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return num;
        }//End modify()

        private void insertRoleModule(Role oRole)
        {
            if (existsRoleModule(oRole.Role_Id))
            {
                DeleteRoleModule(oRole.Role_Id);
            }
            for (int i = 0; i < oRole.oListSystemModule.Count; i++)
            {
                Entities.SystemModule oModule = oRole.oListSystemModule[i];
                insertSqlRoleModule(oRole.Role_Id, oModule.SystemModule_Id);
            }
        }



        public Boolean existsRoleModule(Int32 pCode)
        {
            Boolean exist;
            String oSql = "SP_EXISTROLEMODULE";//Stored procedure name
            try
            {

                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 toExist = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand)); //To execute query
                if (toExist == 0)
                {
                    exist = false;
                }
                else
                {
                    exist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return exist;
        }//End exists

        private Int32 DeleteRoleModule(int code)
        {
            Int32 num = 0;
            String oSql = "SP_DELETEROLEMODULE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                num = DAO.getInstance().executeSQL(oCommand);////To execute update
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return num;
        }

        public Int32 insertSqlRoleModule(int role_id, int modulo_id)
        {
            Int32 num = 0;
            String oSql = "SP_INSERTROLEMODULE"; //Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID_ROLE", role_id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID_MODULE", modulo_id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                num = DAO.getInstance().executeSQL(oCommand); //To execute insert

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return num;
        }//End insert()


        public List<Role> getAll()
        {
            String sql = "SP_GETALLROLE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                List<Role> listRole = new List<Role>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Role oRole = new Role();
                    oRole.Role_Id = Convert.ToInt32(oDataRow[0].ToString());
                    oRole.Description = oDataRow[1].ToString();
                    oRole.State = Convert.ToInt16(oDataRow[2].ToString());
                    oRole.oListSystemModule= getRoleModuleByRole(Convert.ToInt32(oDataRow[0].ToString()));
                    oRole.modulesName();
                    listRole.Add(oRole);
                }
                return listRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<SystemModule> getRoleModuleByRole(int role_id)
        {
            String sql = "SP_GETALLROLEMODULEBYROLE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", role_id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                List<SystemModule> listSystemModule = new List<SystemModule>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    SystemModule oSystemModule = new SystemModule();
                    oSystemModule.SystemModule_Id = Convert.ToInt32(oDataRow[1].ToString());
                    oSystemModule.Description = oDataRow[2].ToString();
                    listSystemModule.Add(oSystemModule);
                }
                return listSystemModule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Role getRole(int role_id)
        {
            String sql = "SP_GETROLE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", role_id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                Role oRole = new Role();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    
                    oRole.Role_Id = Convert.ToInt32(oDataRow[0].ToString());
                    oRole.Description = oDataRow[1].ToString();
                    oRole.State = Convert.ToInt16(oDataRow[2].ToString());
                    oRole.oListSystemModule = getRoleModuleByRole(role_id);
                    oRole.modulesName();
                   
                }
                return oRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 delete(Int32 pCode)
        {
            String oSql = "SP_DELETEROLE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                return DAO.getInstance().executeSQL(oCommand);////To execute update
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End delete() 
    }
}