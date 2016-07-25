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
    public class SliderBLL
    {

        private static SliderBLL instace = null;

        public static SliderBLL getInstance()
        {
            if (instace == null)
            {
                instace = new SliderBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            try
            {
                String oSql = "SP_GETNEXTCODE_SLIDER";
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

        //Method to check if the program exists in the database
        public Boolean exists(Int32 pCode)
        {
            Boolean exist;
            String oSql = "SP_EXISTSLIDER";//Stored procedure name
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

        public Int32 insert(Slider pSlider)
        {
            String oSql = "SP_INSERT_SLIDER";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pSlider.code);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", pSlider.description);
                oCommand.Parameters.AddWithValue("@IMAGE", pSlider.image);
                oCommand.Parameters.AddWithValue("@STATE", pSlider.state);
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public Int32 modify(Slider pSlider)
        {
            String oSql = "SP_MODIFYSLIDER";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pSlider.code);
                oCommand.Parameters.AddWithValue("@DESCRIPTION", pSlider.description);
                oCommand.Parameters.AddWithValue("@IMAGE", pSlider.image);
                oCommand.Parameters.AddWithValue("@STATE", pSlider.state);
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
            String oSql = "SP_DELETESLIDER";

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

        public List<Slider> getAll()
        {
            String sql = "SP_GETALL_SLIDER";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Slider> listSlider = new List<Slider>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Slider oSlider = new Slider();
                    oSlider.code = Convert.ToInt32(oDataRow[0].ToString());
                    oSlider.description = oDataRow[1].ToString();
                    oSlider.image = oDataRow[2].ToString();
                    oSlider.state = Convert.ToInt16(oDataRow[3].ToString());
                    listSlider.Add(oSlider);
                }
                return listSlider;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<Slider> getAllActive()
        {
            String sql = "SP_GETALLACTIVE_SLIDER";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Slider> listSlider = new List<Slider>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Slider oSlider = new Slider();
                    oSlider.code = Convert.ToInt32(oDataRow[0].ToString());
                    oSlider.description = oDataRow[1].ToString();
                    oSlider.image = oDataRow[2].ToString();
                    oSlider.state = Convert.ToInt16(oDataRow[3].ToString());
                    listSlider.Add(oSlider);
                }
                return listSlider;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

    }
}