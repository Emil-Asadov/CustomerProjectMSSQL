using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectSQL.Model
{
    internal class ClassDbConfig
    {
        public SqlConnection con { get; set; }
        public string GetConnectionString()
        {
            var connectionString = string.Empty;
            //connectionString = $@"Data Source = DESKTOP-L7Q1T20\SQLEXPRESS; Initial Catalog = DB_E03; Integrated security = true";
            connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
            return connectionString;
        }
        public (SqlConnection conRes, string errRes) GetConnection()
        {
            var errOut = string.Empty;
            try
            {
                con = new SqlConnection(GetConnectionString());
                con.Open();
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
                return (con, errOut);
            }
            return (con, errOut);
        }
        public (DataTable dtRes, string errRes) FillDT(SqlCommand com)
        {
            var errOut = string.Empty;
            var dtOut = new DataTable();
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
                using (con)
                {
                    using (com)
                    {
                        com.Connection = con;
                        using (var read = com.ExecuteReader())
                        {
                            dtOut.Load(read);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return (dtOut, errOut);
        }
        public (DataTable dtRes, string errRes) FillDT(string query)
        {
            var errOut = string.Empty;
            var dtOut = new DataTable();
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
                using (con)
                {
                    using (var com = new SqlCommand())
                    {
                        com.CommandText = query;
                        com.CommandType = CommandType.Text;
                        com.Connection = con;
                        using (var read = com.ExecuteReader())
                        {
                            dtOut.Load(read);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return (dtOut, errOut);
        }
        public (int res, string err) InsertDb(SqlCommand com)
        {
            var myTrans = default(SqlTransaction);
            var errOut = string.Empty;
            var resOut = 0;
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (resOut, errOut);
                myTrans = con.BeginTransaction();
                using (com)
                {
                    com.Transaction = myTrans;
                    com.Connection = con;
                    resOut = com.ExecuteNonQuery();
                    myTrans.Commit();
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
                myTrans.Rollback();
                return (resOut, errOut);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return (resOut, errOut);
        }

        public (string valueRes, string errRes) FillValue(SqlCommand com)
        {
            var errOut = string.Empty;
            var valueOut = string.Empty;
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (valueOut, errOut);
                using (con)
                {
                    using (com)
                    {
                        com.Connection = con;
                        valueOut = com.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return (valueOut, errOut);
        }
    }
}
