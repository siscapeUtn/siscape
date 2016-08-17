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
    public class ClassRoomBLL
    {
        private static ClassRoomBLL instace = null;

        public static ClassRoomBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ClassRoomBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODECLASSROOM";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            try
            {
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
            String oSql = "SP_EXISTCLASSROOM";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@ID", pCode);
            try
            {
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

        public Int32 insert(ClassRoom oClassRoom)
        {
            String oSql = "SP_INSERTCLASSROOM";
           
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oClassRoom.code);
                oCommand.Parameters.AddWithValue("@NUM_ROOM", oClassRoom.num_room);
                oCommand.Parameters.AddWithValue("@SIZE", oClassRoom.size);
                oCommand.Parameters.AddWithValue("@CLASSROOMSTYPE", oClassRoom.oClassRoomsType.code);
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oClassRoom.oProgram.code);
                oCommand.Parameters.AddWithValue("@LOCATION_ID", oClassRoom.oLocation.code);
                oCommand.Parameters.AddWithValue("@STATE", oClassRoom.state);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(ClassRoom oClassRoom)
        {
            String oSql = "SP_MODIFYCLASSROOM";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oClassRoom.code);
                oCommand.Parameters.AddWithValue("@NUM_ROOM", oClassRoom.num_room);
                oCommand.Parameters.AddWithValue("@SIZE", oClassRoom.size);
                oCommand.Parameters.AddWithValue("@CLASSROOMSTYPE", oClassRoom.oClassRoomsType.code);
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oClassRoom.oProgram.code);
                oCommand.Parameters.AddWithValue("@LOCATION_ID", oClassRoom.oLocation.code);
                oCommand.Parameters.AddWithValue("@STATE", oClassRoom.state);
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
            String oSql = "SP_DELETECLASSROOM";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE",pCode);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<ClassRoom> getAll()
        {
            String sql = "SP_GETALLCLASSROOM";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoom> listClassRoom = new List<ClassRoom>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoom oClassRoom = new ClassRoom();
                    Program oProgram = new Program();
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoom.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoom.num_room = oDataRow[1].ToString();
                    oClassRoom.size = Convert.ToInt32(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oProgram.name = oDataRow[4].ToString();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[5].ToString());
                    oClassRoomsType.description = oDataRow[6].ToString();
                    oLocation.code = Convert.ToInt16(oDataRow[7].ToString());
                    oHeadquarters.code = Convert.ToInt16(oDataRow[8].ToString());
                    oHeadquarters.description = oDataRow[9].ToString();
                    oLocation.building = oDataRow[10].ToString();
                    oLocation.module = oDataRow[11].ToString();
                    oLocation.oHeadquarters = oHeadquarters;
                    oClassRoom.state = Convert.ToInt16(oDataRow[12].ToString());
                    oClassRoom.oProgram = oProgram;
                    oClassRoom.oClassRoomsType = oClassRoomsType;
                    oClassRoom.oLocation = oLocation;
                    listClassRoom.Add(oClassRoom);
                }
                return listClassRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<ClassRoom> getAllActive()
        {
            String sql = "SP_GETALLCLASSROOMACTIVE";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoom> listClassRoom = new List<ClassRoom>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoom oClassRoom = new ClassRoom();
                    Program oProgram = new Program();
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoom.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoom.num_room = oDataRow[1].ToString();
                    oClassRoom.size = Convert.ToInt32(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oProgram.name = oDataRow[4].ToString();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[5].ToString());
                    oClassRoomsType.description = oDataRow[6].ToString();
                    oLocation.code = Convert.ToInt16(oDataRow[7].ToString());
                    oHeadquarters.code = Convert.ToInt16(oDataRow[8].ToString());
                    oHeadquarters.description = oDataRow[9].ToString();
                    oLocation.building = oDataRow[10].ToString();
                    oLocation.module = oDataRow[11].ToString();
                    oLocation.oHeadquarters = oHeadquarters;
                    oClassRoom.state = Convert.ToInt16(oDataRow[12].ToString());
                    oClassRoom.oProgram = oProgram;
                    oClassRoom.oClassRoomsType = oClassRoomsType;
                    oClassRoom.oLocation = oLocation;
                    listClassRoom.Add(oClassRoom);
                }
                return listClassRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public ClassRoom getClassRoom(Int32 pCode)
        {
            String sql = "SP_GETCLASSROOM";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                ClassRoom oClassRoom = new ClassRoom();
                Program oProgram = new Program();
                ClassRoomsType oClassRoomsType = new ClassRoomsType();
                Location oLocation = new Location();
                Headquarters oHeadquarters = new Headquarters();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oClassRoom.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoom.num_room = oDataRow[1].ToString();
                    oClassRoom.size = Convert.ToInt32(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oProgram.name = oDataRow[4].ToString();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[5].ToString());
                    oClassRoomsType.description = oDataRow[6].ToString();
                    oLocation.code = Convert.ToInt16(oDataRow[7].ToString());
                    oHeadquarters.code = Convert.ToInt16(oDataRow[8].ToString());
                    oHeadquarters.description = oDataRow[9].ToString();
                    oLocation.building = oDataRow[10].ToString();
                    oLocation.module = oDataRow[11].ToString();
                    oLocation.oHeadquarters = oHeadquarters;
                    oClassRoom.state = Convert.ToInt16(oDataRow[12].ToString());
                    oClassRoom.oProgram = oProgram;
                    oClassRoom.oClassRoomsType = oClassRoomsType;
                    oClassRoom.oLocation = oLocation;
                }
                return oClassRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }


        public List<ClassRoom> getAllByProgram(Int32 pCode)
        {
            String sql = "SP_GETALLCLASSROOMBYPROGRAM";
            
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoom> listClassRoom = new List<ClassRoom>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoom oClassRoom = new ClassRoom();
                    Program oProgram = new Program();
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoom.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoom.num_room = oDataRow[1].ToString();
                    oClassRoom.size = Convert.ToInt32(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oProgram.name = oDataRow[4].ToString();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[5].ToString());
                    oClassRoomsType.description = oDataRow[6].ToString();
                    oLocation.code = Convert.ToInt16(oDataRow[7].ToString());
                    oHeadquarters.code = Convert.ToInt16(oDataRow[8].ToString());
                    oHeadquarters.description = oDataRow[9].ToString();
                    oLocation.building = oDataRow[10].ToString();
                    oLocation.module = oDataRow[11].ToString();
                    oLocation.oHeadquarters = oHeadquarters;
                    oClassRoom.state = Convert.ToInt16(oDataRow[12].ToString());
                    oClassRoom.oProgram = oProgram;
                    oClassRoom.oClassRoomsType = oClassRoomsType;
                    oClassRoom.oLocation = oLocation;
                    listClassRoom.Add(oClassRoom);
                }
                return listClassRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}