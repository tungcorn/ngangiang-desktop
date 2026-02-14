using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ngangiang_desktop
{
    /// <summary>
    /// Lớp tiện ích hỗ trợ kết nối và thực thi truy vấn SQL Server.
    /// Sử dụng ADO.NET (phù hợp với .NET Framework 4.8) để kiểm soát trực tiếp kết nối và truy vấn.
    /// </summary>
    public static class DatabaseHelper
    {
        // Lưu connection string dạng readonly để tránh thay đổi ngoài ý muốn
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["QuanLyNhapHang"].ConnectionString;

        /// <summary>
        /// Tạo và trả về một đối tượng SqlConnection mới.
        /// Hàm này hữu ích khi cần tự quản lý connection (ví dụ: dùng trong Transaction thủ công).
        /// </summary>
        /// <returns>Đối tượng SqlConnection đã được khởi tạo với chuỗi kết nối.</returns>
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Thực thi câu lệnh SQL trả về dữ liệu (SELECT).
        /// Lưu ý: Hàm này không hỗ trợ parameterized query.
        /// Chỉ dùng cho truy vấn tĩnh, không nối chuỗi từ user input để tránh SQL Injection.
        /// </summary>
        /// <param name="query">Câu lệnh SQL SELECT.</param>
        /// <returns>DataTable chứa kết quả truy vấn.</returns>
        public static DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Wrap exception kèm thông báo tiếng Việt để tầng UI hiển thị, giữ stack trace qua InnerException
                    throw new Exception("Lỗi thực thi truy vấn: " + ex.Message, ex);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Thực thi câu lệnh SQL không trả về dữ liệu (INSERT, UPDATE, DELETE).
        /// Lưu ý: Hàm này không hỗ trợ parameterized query.
        /// Chỉ dùng cho truy vấn tĩnh, không nối chuỗi từ user input để tránh SQL Injection.
        /// </summary>
        /// <param name="query">Câu lệnh SQL hành động.</param>
        /// <returns>Số dòng bị ảnh hưởng (Records Affected).</returns>
        public static int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Wrap exception kèm thông báo tiếng Việt để tầng UI hiển thị, giữ stack trace qua InnerException
                    throw new Exception("Lỗi thực thi lệnh: " + ex.Message, ex);
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// Thực thi câu lệnh SQL trả về một giá trị duy nhất (COUNT, SUM, MAX...).
        /// Lưu ý: Hàm này không hỗ trợ parameterized query.
        /// Chỉ dùng cho truy vấn tĩnh, không nối chuỗi từ user input để tránh SQL Injection.
        /// </summary>
        /// <param name="query">Câu lệnh SQL trả về 1 ô dữ liệu.</param>
        /// <returns>Đối tượng kết quả (cần ép kiểu khi sử dụng).</returns>
        public static object ExecuteScalar(string query)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    result = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Wrap exception kèm thông báo tiếng Việt để tầng UI hiển thị, giữ stack trace qua InnerException
                    throw new Exception("Lỗi lấy giá trị đơn: " + ex.Message, ex);
                }
            }
            return result;
        }
    }
}
