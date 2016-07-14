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
    public class ClassRoomScheduleBLL
    {

        private static ClassRoomScheduleBLL instace = null;

        public static ClassRoomScheduleBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ClassRoomScheduleBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODECLASSROOM_SCHEDULE";
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
            catch (Exception)
            {
                throw;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End


        public Int32 insert(Entities.AcademicOffer oAcademicOffer, Entities.Schedule oSchedule, int cod, int day)
        {
            String oSql = "SP_INSERTCLASSROOM_SCHEDULE";
            SqlCommand oCommand = new SqlCommand(oSql);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@CLASSROOM_ID", oAcademicOffer.oClassRoom.code);
            oCommand.Parameters.AddWithValue("@CLASSROOM_SCHEDULE_ID", cod);
            oCommand.Parameters.AddWithValue("@PERIOD_ID", oAcademicOffer.oPeriod.code);
            oCommand.Parameters.AddWithValue("@Course_ID", oAcademicOffer.oCourse.id);
            oCommand.Parameters.AddWithValue("@DIA_ID", day);
            oCommand.Parameters.AddWithValue("@INITIAL_HOUR", oSchedule.startTime);
            oCommand.Parameters.AddWithValue("@FINAL_HOUR", oSchedule.endTime);
            oCommand.Parameters.AddWithValue("@ACADEMICOFFER_ID", oAcademicOffer.code);
            try
            {
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }


        public List<ClassRoom> getAllActiveBySchedule(int periodId, int programid, Entities.Schedule oSchedule, int[] days)
        {
            String sql = "SP_CLASSROOMBYSCHEDULE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@period", periodId);
                oCommand.Parameters.AddWithValue("@program", programid);
                oCommand.Parameters.AddWithValue("@inicialHour", oSchedule.startTime);
                oCommand.Parameters.AddWithValue("@finalhour", oSchedule.endTime);
                oCommand.Parameters.AddWithValue("@day1", Convert.ToInt32(days[0]));
                oCommand.Parameters.AddWithValue("@day2", Convert.ToInt32(days[1]));
                oCommand.Parameters.AddWithValue("@day3", Convert.ToInt32(days[2]));
                oCommand.Parameters.AddWithValue("@day4", Convert.ToInt32(days[3]));
                oCommand.Parameters.AddWithValue("@day5", Convert.ToInt32(days[4]));
                oCommand.Parameters.AddWithValue("@day6", Convert.ToInt32(days[5]));
                oCommand.Parameters.AddWithValue("@day7", Convert.ToInt32(days[6]));
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoom> listClassRoom = new List<ClassRoom>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoom oClassRoom = new ClassRoom();
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoom.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoom.num_room = oDataRow[1].ToString();
                    oClassRoomsType.description = oDataRow[2].ToString();
                    oLocation.code = Convert.ToInt32(oDataRow[3].ToString());
                    oLocation.building = oDataRow[4].ToString();
                    oLocation.module = oDataRow[5].ToString();
                    oHeadquarters.description = oDataRow[6].ToString();
                    oLocation.oHeadquarters = oHeadquarters;
                    oClassRoom.oClassRoomsType = oClassRoomsType;
                    oClassRoom.oLocation = oLocation;
                    listClassRoom.Add(oClassRoom);
                }
                return listClassRoom;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }


    }
}