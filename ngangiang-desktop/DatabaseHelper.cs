using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ngangiang_desktop
{
    /// <summary>
    /// Class tiện ích để quản lý kết nối và thực thi câu lệnh SQL
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Lấy connection string từ App.config
        /// </summary>
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["QuanLyNhapHang"].ConnectionString;
        }

        /// <summary>
        /// Thực thi câu SELECT và trả về DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Thực thi câu INSERT/UPDATE/DELETE và trả về số dòng bị ảnh hưởng
        /// </summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Thực thi câu lệnh và trả về giá trị đầu tiên (dùng cho COUNT, SCOPE_IDENTITY...)
        /// </summary>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }

        /// <summary>
        /// Tạo SqlConnection mới (dùng cho Transaction)
        /// </summary>
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
