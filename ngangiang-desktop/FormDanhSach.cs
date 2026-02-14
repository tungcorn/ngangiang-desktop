using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form chính của ứng dụng Desktop.
    /// Chức năng:
    /// 1. Hiển thị danh sách tất cả các đơn nhập hàng (kết hợp thông tin NCC).
    /// 2. Xem chi tiết mặt hàng và thành tiền của từng đơn.
    /// 3. Cung cấp lối tắt để tạo đơn hàng mới hoặc làm mới dữ liệu.
    /// </summary>
    public partial class FormDanhSach : Form
    {
        public FormDanhSach()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load Form: Tải dữ liệu ban đầu và thiết lập trạng thái mặc định.
        /// </summary>
        private void FormDanhSach_Load(object sender, EventArgs e)
        {
            LoadDanhSachDonNhap();
            
            // Xóa selection mặc định để giao diện sạch hơn (tránh hiển thị chi tiết ngẫu nhiên)
            dgvDonNhap.ClearSelection();
            dgvChiTiet.DataSource = null;
            lblTongTien.Text = "Tổng tiền: 0 ₫";
        }

        /// <summary>
        /// Tải danh sách đơn nhập hàng từ CSDL.
        /// Sử dụng kỹ thuật JOIN bảng để hiển thị tên Nhà cung cấp thay vì ID.
        /// </summary>
        private void LoadDanhSachDonNhap()
        {
            try
            {
                // Query kết hợp bảng DonNhapHang và NCC
                string query = @"
                    SELECT 
                        d.Id_DonNhapHang AS [Mã đơn],
                        n.Ten_NCC AS [Nhà cung cấp],
                        n.DiaChi AS [Địa chỉ],
                        n.Email AS [Email]
                    FROM DonNhapHang d
                    INNER JOIN NCC n ON d.FK_Id_NCC = n.Id_NCC
                    ORDER BY d.Id_DonNhapHang DESC"; // Sắp xếp đơn mới nhất lên đầu

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvDonNhap.DataSource = dt;

                // Tự động điều chỉnh độ rộng cột để lấp đầy bảng
                dgvDonNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Cập nhật thống kê tổng số đơn
                lblTongSoDon.Text = $"Tổng số đơn: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn nhập: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng chọn một dòng trong bảng Đơn Nhập.
        /// Hệ thống sẽ tự động tải chi tiết của đơn hàng đó.
        /// </summary>
        private void dgvDonNhap_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonNhap.SelectedRows.Count > 0)
            {
                // Lấy ID từ cột đầu tiên (Mã đơn)
                int idDonNhap = Convert.ToInt32(dgvDonNhap.SelectedRows[0].Cells["Mã đơn"].Value);
                
                // Gọi hàm load chi tiết
                LoadChiTietDonNhap(idDonNhap);
            }
        }

        /// <summary>
        /// Tải danh sách chi tiết mặt hàng của một đơn nhập cụ thể.
        /// Thực hiện tính toán cột Thành tiền (Số lượng * Đơn giá) và Tổng tiền đơn hàng.
        /// </summary>
        /// <param name="idDonNhap">Mã đơn nhập hàng cần xem chi tiết.</param>
        private void LoadChiTietDonNhap(int idDonNhap)
        {
            try
            {
                // Sử dụng parameterized query để tránh SQL Injection
                string query = @"
                    SELECT 
                        m.Ten_MatHang AS [Mặt hàng],
                        m.DonViTinh AS [Đơn vị],
                        c.Count AS [Số lượng],
                        m.DonGia AS [Đơn giá],
                        (c.Count * m.DonGia) AS [Thành tiền]
                    FROM ChiTietDonNhap c
                    INNER JOIN MatHang m ON c.FK_Id_MatHang = m.Id_MatHang
                    WHERE c.FK_Id_DonNhapHang = @IdDonNhap";

                DataTable dt = new DataTable();
                using (SqlConnection connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@IdDonNhap", idDonNhap);
                    adapter.Fill(dt);
                }

                dgvChiTiet.DataSource = dt;
                dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Format định dạng tiền tệ (tách hàng nghìn bằng dấu phẩy)
                if (dgvChiTiet.Columns["Đơn giá"] != null)
                    dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                
                if (dgvChiTiet.Columns["Thành tiền"] != null)
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";

                // Tính tổng cộng tiền hàng
                decimal tongTien = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongTien += Convert.ToDecimal(row["Thành tiền"]);
                }
                
                // Hiển thị tổng tiền
                lblTongTien.Text = $"Tổng tiền: {tongTien:N0} ₫";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn nhập: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mở form Tạo đơn mới (Modal Dialog).
        /// Nếu tạo thành công (DialogResult.OK), tải lại danh sách để cập nhật dữ liệu mới.
        /// </summary>
        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            FormTaoDon formTaoDon = new FormTaoDon();
            if (formTaoDon.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachDonNhap();
                // Reset lại chi tiết sau khi thêm mới
                dgvChiTiet.DataSource = null;
                lblTongTien.Text = "Tổng tiền: 0 ₫";
            }
        }

        /// <summary>
        /// Nút Làm mới: Tải lại toàn bộ dữ liệu từ CSDL.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachDonNhap();
            dgvChiTiet.DataSource = null;
            lblTongTien.Text = "Tổng tiền: 0 ₫";
        }
    }
}
