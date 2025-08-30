using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Movie_Management_API.Classes
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        // Constructor takes IConfiguration to read from appsettings.json
        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defConn");
        }

        /// <summary>
        /// Executes a SELECT query and returns results in a DataTable
        /// </summary>
        public DataTable ExecuteSelect(string query, List<SqlParameter>? parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Executes INSERT/UPDATE/DELETE query and returns number of affected rows
        /// </summary>
        public int ExecuteNonQuery(string query, List<SqlParameter>? parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// Executes a stored procedure and returns results in a DataTable
        /// </summary>
        public DataTable ExecuteStoredProcedure(string procedureName, List<SqlParameter>? parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Executes a stored procedure that modifies data (Insert/Update/Delete)
        /// </summary>
        public int ExecuteStoredProcedureNonQuery(string procedureName, List<SqlParameter>? parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }
}
