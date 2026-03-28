using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LeadManagementSystem.Data
{
    public class DbHelper
    {
        private string connStr = ConfigurationManager.ConnectionStrings["LeadDB"].ConnectionString;

        public DataTable ExecuteSelect(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                {
                    // Ensure ADO.NET receives DBNull.Value for null parameter values
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null) p.Value = DBNull.Value;
                    }
                    cmd.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public int ExecuteDML(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                {
                    // Ensure ADO.NET receives DBNull.Value for null parameter values
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null) p.Value = DBNull.Value;
                    }
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                {
                    // Ensure ADO.NET receives DBNull.Value for null parameter values
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null) p.Value = DBNull.Value;
                    }
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        internal int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null) p.Value = DBNull.Value;
                    }
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}