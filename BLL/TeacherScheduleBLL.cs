using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class TeacherScheduleBLL
    {

        private static TeacherScheduleBLL instace = null;

        public static TeacherScheduleBLL getInstance()
        {
            if (instace == null)
            {
                instace = new TeacherScheduleBLL();
            }
            return instace;
        }

            public Int32 getNextCode()
              {
                  String next;
                  String oSql = "SP_GETNEXTCODETEACHER_SCHEDULE";
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
                  String oSql = "SP_INSERT_TEACHER_SCHEDULE";
                  SqlCommand oCommand = new SqlCommand(oSql);
                  oCommand.CommandType = CommandType.StoredProcedure;
                  oCommand.Parameters.AddWithValue("@TEACHER_SCHEDULE_ID", cod);
                  oCommand.Parameters.AddWithValue("@TEACHER_ID", oAcademicOffer.oteacher.code);
                  oCommand.Parameters.AddWithValue("@PERIOD_ID", oAcademicOffer.oPeriod.code);
                  oCommand.Parameters.AddWithValue("@DIA_ID", day);
                  oCommand.Parameters.AddWithValue("@COURSE_ID", oAcademicOffer.oCourse.id);
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


        public List<Teacher> getAllActiveTeacherBySchedule(int periodId, Entities.Schedule oSchedule, int[] days)
        {
            String sql = "SP_TEACHER_BY_SCHEDULE";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@period", periodId);
            oCommand.Parameters.AddWithValue("@inicialHour", oSchedule.startTime);
            oCommand.Parameters.AddWithValue("@finalhour", oSchedule.endTime);
            oCommand.Parameters.AddWithValue("@day1", Convert.ToInt32(days[0]));
            oCommand.Parameters.AddWithValue("@day2", Convert.ToInt32(days[1]));
            oCommand.Parameters.AddWithValue("@day3", Convert.ToInt32(days[2]));
            oCommand.Parameters.AddWithValue("@day4", Convert.ToInt32(days[3]));
            oCommand.Parameters.AddWithValue("@day5", Convert.ToInt32(days[4]));
            oCommand.Parameters.AddWithValue("@day6", Convert.ToInt32(days[5]));
            oCommand.Parameters.AddWithValue("@day7", Convert.ToInt32(days[6]));
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Teacher> listTeacher = new List<Teacher>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Teacher oTeacher = new Teacher();
                    InternalDesignation oInternal = new InternalDesignation();
                    oTeacher.code = Convert.ToInt32(oDataRow[0].ToString());
                    oTeacher.name = oDataRow[1].ToString();
                    oTeacher.lastName = oDataRow[2].ToString();
                    oTeacher.homePhone = oDataRow[3].ToString();
                    oTeacher.cellPhone = oDataRow[4].ToString();
                    oTeacher.email = oDataRow[5].ToString();
                    oInternal.code = Convert.ToInt32(oDataRow[6].ToString());
                    oInternal.description = oDataRow[7].ToString();
                    oTeacher.Position = oInternal;
                    listTeacher.Add(oTeacher);
                }
                return listTeacher;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

    }
}