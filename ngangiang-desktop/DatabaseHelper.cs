using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ngangiang_desktop
{
    /// <summary>
    /// Lớp tiện ích hỗ trợ kết nối và thực thi truy vấn SQL Server.
    /// Sử dụng ADO.NET truyền thống để tối ưu hiệu năng và khả năng kiểm soát kết nối.
    /// </summary>
    public static class DatabaseHelper
    {
        // Cache connection string để tối ưu hiệu năng và đảm bảo immutability
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
                    // Ném lại exception để tầng UI xử lý và hiển thị thông báo cho người dùng
                    // Giữ nguyên stack trace để dễ debug
                    throw new Exception("Lỗi thực thi truy vấn: " + ex.Message);
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
                    // Ném lại exception để tầng UI xử lý và hiển thị thông báo cho người dùng
                    // Giữ nguyên stack trace để dễ debug
                    throw new Exception("Lỗi thực thi lệnh: " + ex.Message);
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
                    // Ném lại exception để tầng UI xử lý và hiển thị thông báo cho người dùng
                    // Giữ nguyên stack trace để dễ debug
                    throw new Exception("Lỗi lấy giá trị đơn: " + ex.Message);
                }
            }
            return result;
        }
    }
}
