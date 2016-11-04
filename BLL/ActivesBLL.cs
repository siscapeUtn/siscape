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
    public class ActivesBLL
    {
        private static ActivesBLL instace = null;

        public static ActivesBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ActivesBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODEACTIVES";
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
            catch (Exception)
            {
                throw;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End

        public Boolean exists(Int32 pCode)
        {
            Boolean existe;
            String oSql = "SP_EXISTACTIVES";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 existencia = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (existencia == 0)
                {
                    existe = false;
                }
                else
                {
                    existe = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return existe;
        }// end get next code

        public Int32 insert(Actives oActives)
        {
            String oSql = "SP_INSERTACTIVES";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oActives.code);
                oCommand.Parameters.AddWithValue("@CODEALPHANUMERIC", oActives.codeAlphaNumeric);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oActives.description);
                oCommand.Parameters.AddWithValue("@STATEACTIVE", oActives.state);
                oCommand.Parameters.AddWithValue("@CLASSROOM_ID", oActives.oClassRoom.code);
                
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public Int32 modify(Actives oActives)
        {
            String oSql = "SP_MODIFYACTIVES";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oActives.code);
                oCommand.Parameters.AddWithValue("@CODEALPHANUMERIC", oActives.codeAlphaNumeric);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oActives.description);
                oCommand.Parameters.AddWithValue("@STATEACTIVE", oActives.state);
                oCommand.Parameters.AddWithValue("@CLASSROOM_ID", oActives.oClassRoom.code);

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
            String oSql = "SP_DELETEACTIVES";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }


        public List<Actives> getAll()
        {
            String sql = "SP_GETALLACTIVES";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Actives> listActives = new List<Actives>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Actives oActivites = new Actives();
                    ClassRoom oClassRoom = new ClassRoom();

                    oActivites.code = Convert.ToInt32(oDataRow[0].ToString());
                    oActivites.codeAlphaNumeric = oDataRow[1].ToString();
                    oActivites.description = oDataRow[2].ToString();
                    oActivites.state = oDataRow[3].ToString();
                    oClassRoom.code = Convert.ToInt32(oDataRow[4].ToString());
                    oClassRoom.num_room = oDataRow[5].ToString();
                    oActivites.oClassRoom = oClassRoom;

                    listActives.Add(oActivites);
                }
                return listActives;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Actives getActive(Int32 pCode)
        {
            String sql = "SP_GETACTIVE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Actives oActivites = new Actives();
                ClassRoom oClassRoom = new ClassRoom();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oActivites.code = Convert.ToInt32(oDataRow[0].ToString());
                    oActivites.codeAlphaNumeric = oDataRow[1].ToString();
                    oActivites.description = oDataRow[2].ToString();
                    oActivites.state = oDataRow[3].ToString();
                    oClassRoom.code = Convert.ToInt32(oDataRow[4].ToString());
                    oClassRoom.num_room = oDataRow[5].ToString();
                    oActivites.oClassRoom = oClassRoom;
                }
                return oActivites;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }


        public List<Actives> getAllByClassroom(Int32 pCode)
        {
            String sql = "SP_GETALLACTIVESBYCLASSROOM";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Actives> listActives = new List<Actives>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Actives oActivites = new Actives();
                    ClassRoom oClassRoom = new ClassRoom();

                    oActivites.code = Convert.ToInt32(oDataRow[0].ToString());
                    oActivites.codeAlphaNumeric = oDataRow[1].ToString();
                    oActivites.description = oDataRow[2].ToString();
                    oActivites.state = oDataRow[3].ToString();
                    oClassRoom.code = Convert.ToInt32(oDataRow[4].ToString());
                    oClassRoom.num_room = oDataRow[5].ToString();
                    oActivites.oClassRoom = oClassRoom;

                    listActives.Add(oActivites);
                }
                return listActives;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

    }
}