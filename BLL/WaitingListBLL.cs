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
    public class WaitingListBLL
    {
        private static WaitingListBLL instance = null;

        public static WaitingListBLL getInstance()
        {
            if (instance == null)
            {
                instance = new WaitingListBLL();
            }
            return instance;
        }


        public Int32 getNextCodeWaitingList()
        {

            String next;
            String oSql = "SP_GETNEXTCODEWAITINGLIST";

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

        public Int32 getNextCodeTentativeShcedule()
        {

            String next;
            String oSql = "SP_GET_NEXT_CODE_TENTATIVE_SHCEDULE";

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

        public Int32 insertWaitingList(WaitingList pWaitingList)
        {
            String oSql = "SP_INSERT_WAITINGLIST";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@WAITING_LIST_ID", pWaitingList.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", pWaitingList.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", pWaitingList.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", pWaitingList.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", pWaitingList.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELLPHONE", pWaitingList.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", pWaitingList.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@OFFER", pWaitingList.academicOffer);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CONTACTED", 0);
                oCommand.Parameters[8].Direction = ParameterDirection.Input;
                DAO.getInstance().executeSQL(oCommand);
                //insertTentative_Schedule(pListTentative_Schedule, pWaitingList.code);
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }


       
        //update waiting list contacted customers 
        public void UpdateWaitingListContacted(Int32 pCodeWaitinglist, bool pContacted)
        {
            String oSql = "SP_MODIFY_WAITING_LIST";
            try
            {
                SqlCommand oCommand = null;
                oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@WAITING_LIST_ID", pCodeWaitinglist);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CONTACTED", pContacted);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }


        //get all the customers
        public DataTable getAllCostumers(Int32 period)
        {
            String sql = "SP_GET_ALL_CUSTOMER_BY_PERIOD";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PERIOD", period);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAllCustomers


        //get all the customers contacted
        public DataTable getAllCostumersContacted(Int32 period)
        {
            String sql = "SP_GET_ALL_CUSTOMER_2";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PERIOD", period);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAllCustomersContacted

        //get all the customers by course
        public DataTable getAllCostumersByCourse(Int32 period)
        {
            String sql = "SP_GET_ALL_CUSTOMER_BY_COURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PERIOD", period);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAllCustomersbycourse
        public DataTable getAllCostumerWT()
        {
            String sql = "SP_GET_ALL_CUSTOMER_WT";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public DataTable getAllCostumerUncontacted()
        {
            String sql = "SP_GET_ALL_CUSTOMER_WT_UNCONNTACTED";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public DataTable getAllCostumerWTByCourse()
        {
            String sql = "SP_GET_ALL_CUSTOMER_WT_BY_COURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
    }
}