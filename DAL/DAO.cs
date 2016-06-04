using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class DAO
    {
        private static DAO instance = null;
        public SqlConnection oConnection { set; get; }
        public DAO() { }

        public static DAO getInstance(){
            if (instance == null)
            {
                instance = new DAO();
            }
            return instance;
        }

        /**
         * Method to connect with the database 
        */
        public void dbConnect()
        {
            try
            {
                String connection = ConfigurationManager.ConnectionStrings["ProUTN_CS"].ConnectionString;
                oConnection = new SqlConnection(connection);
                oConnection.Open();
            }catch(Exception ex){
                throw new Exception("Error " + "\n" + ex.Message + "\n");
            }
        }//End dbConnect

        /**
         * Method to execute a insert,update or delete
        */
        public Int32 executeSQL(SqlCommand oCommand)
        {
            Int32 quantityRecord = 0;
            dbConnect();
            using (SqlTransaction transaction = oConnection.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    oCommand.Connection = oConnection;
                    oCommand.Transaction = transaction;
                    quantityRecord = oCommand.ExecuteNonQuery();
                    transaction.Commit();
                }catch(Exception ex){
                    throw new Exception("Error to execute sql" + "\n" + ex.Message + "\n" + oCommand.CommandText);
                }
                finally
                {
                    oConnection.Close();//To close the connection
                }
            }
            return quantityRecord;
        }//End executeSQL

        /**
         * Method to execute a list of inserts, updates or deletes
        */
        public Int32 executeSQL(List<SqlCommand> listsComands)
        {
            String commandText = "";
            Int32 quantitySql = 0;
            
            dbConnect(); //To connect with the database
            using (SqlTransaction transaction = oConnection.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    foreach (SqlCommand oCommand in listsComands)
                    {
                        oCommand.Connection = oConnection;
                        oCommand.Transaction = transaction;
                        commandText = oCommand.CommandText;
                        oCommand.ExecuteNonQuery();//Execute the query
                        quantitySql++;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error to execute sql " + "\n" + ex.Message + "\n" + commandText);
                }
                finally
                {
                    oConnection.Close(); //To close the connection
                }
            }
            return quantitySql;
        }//End executeSQL

        /** 
         * Method to execute query
         * 
        */
        public DataTable executeQuery(SqlCommand oCommand)
        {
            DataTable oDataTable = new DataTable();
            dbConnect();
            try
            {
                oCommand.Connection = oConnection;
                using (SqlDataAdapter adapter = new SqlDataAdapter(oCommand))
                {
                    adapter.Fill(oDataTable);
                }
                return oDataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to execute query " + "\n" + ex.Message + "\n" + oCommand.CommandText);
            }
            finally
            {
                oConnection.Close(); //To close the connection
            }
        }//executeQuery

        /**
         * Method to execute a query that to return one value
         * 
        */
        public String executeQueryScalar(SqlCommand oCommand)
        {
            String result = "";
            dbConnect(); //To connect with the database
            try
            {
                oCommand.Connection = oConnection;
                result = oCommand.ExecuteScalar().ToString(); //Execute the query
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to execute query " + "\n" + ex.Message + "\n" + oCommand.CommandText);
            }
            finally
            {
                oConnection.Close(); //To close the connection
            }
        }//End executeQueryScalar
    }//End DAO
}