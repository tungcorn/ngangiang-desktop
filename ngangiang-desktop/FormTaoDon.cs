using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form tạo mới đơn nhập hàng.
    /// Chức năng chính: Chọn NCC, thêm mặt hàng vào lưới, và lưu đơn hàng vào CSDL.
    /// </summary>
    public partial class FormTaoDon : Form
    {
        public FormTaoDon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load Form: Tải dữ liệu NCC, Mặt hàng và thêm dòng nhập liệu mặc định.
        /// </summary>
        private void FormTaoDon_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
            LoadMatHang();
            ThemDongMacDinh();
        }

        /// <summary>
        /// Tải danh sách Nhà cung cấp từ CSDL và đổ vào ComboBox.
        /// </summary>
        private void LoadNhaCungCap()
        {
            try
            {
                string query = "SELECT Id_NCC, Ten_NCC FROM NCC ORDER BY Ten_NCC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                cboNCC.DisplayMember = "Ten_NCC";
                cboNCC.ValueMember = "Id_NCC";
                cboNCC.DataSource = dt;
                cboNCC.SelectedIndex = -1; // Bắt buộc người dùng chọn NCC (tránh chọn nhầm giá trị đầu tiên)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách NCC: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách Mặt hàng và tạo cột ComboBox trong DataGridView.
        /// Sử dụng kỹ thuật hiển thị [Tên Loại] + Tên Mặt hàng để tối ưu UX.
        /// </summary>
        private void LoadMatHang()
        {
            try
            {
                // JOIN bảng LoaiHang để hiển thị tên loại
                string query = @"
                    SELECT 
                        m.Id_MatHang, 
                        '[' + l.Name + '] ' + m.Ten_MatHang + ' - ' + FORMAT(m.DonGia, 'N0') AS DisplayText,
                        m.Ten_MatHang,
                        m.DonGia
                    FROM MatHang m
                    INNER JOIN LoaiHang l ON m.FK_Id_LoaiHang = l.Id_LoaiHang
                    ORDER BY l.Name, m.Ten_MatHang";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                // Tạo cột ComboBox chọn mặt hàng
                DataGridViewComboBoxColumn colMatHang = new DataGridViewComboBoxColumn();
                colMatHang.HeaderText = "Mặt hàng";
                colMatHang.Name = "colMatHang";
                colMatHang.DisplayMember = "DisplayText";
                colMatHang.ValueMember = "Id_MatHang";
                colMatHang.DataSource = dt;
                colMatHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colMatHang.MinimumWidth = 300; // Đủ rộng để hiển thị "[Loại] Tên mặt hàng - Giá"

                dgvMatHang.Columns.Add(colMatHang);

                // Tạo cột nhập Số lượng
                DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();
                colSoLuong.HeaderText = "Số lượng";
                colSoLuong.Name = "colSoLuong";
                colSoLuong.Width = 100; // Vừa đủ cho số lượng 4-5 chữ số
                dgvMatHang.Columns.Add(colSoLuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách mặt hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thêm một dòng trống vào lưới để người dùng bắt đầu nhập liệu ngay.
        /// </summary>
        private void ThemDongMacDinh()
        {
            dgvMatHang.Rows.Add();
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Thêm dòng".
        /// </summary>
        private void btnThemDong_Click(object sender, EventArgs e)
        {
            dgvMatHang.Rows.Add();
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Xóa dòng".
        /// Chỉ xóa khi có ít nhất 1 dòng dữ liệu.
        /// </summary>
        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvMatHang.SelectedRows.Count > 0 && dgvMatHang.Rows.Count > 1)
            {
                dgvMatHang.Rows.RemoveAt(dgvMatHang.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Phải có ít nhất 1 dòng mặt hàng!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Lưu".
        /// Thực hiện Validation, gộp hàng trùng và gọi hàm lưu vào DB.
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // ===== KIỂM TRA DỮ LIỆU ĐẦU VÀO =====
            // 1. Kiểm tra đã chọn NCC chưa
            if (cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Duyệt qua lưới để lấy danh sách mặt hàng và số lượng
            Dictionary<int, int> danhSachMatHang = new Dictionary<int, int>();

            foreach (DataGridViewRow row in dgvMatHang.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua dòng mới chưa nhập

                var cellMatHang = row.Cells["colMatHang"].Value;
                var cellSoLuong = row.Cells["colSoLuong"].Value;

                // Validate dữ liệu trống
                if (cellMatHang == null || cellSoLuong == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin mặt hàng và số lượng!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idMatHang = Convert.ToInt32(cellMatHang);
                int soLuong;

                // Validate số lượng phải là số nguyên dương
                if (!int.TryParse(cellSoLuong.ToString(), out soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gộp số lượng nếu mặt hàng bị chọn trùng (tránh duplicate trong CSDL)
                if (danhSachMatHang.ContainsKey(idMatHang))
                {
                    danhSachMatHang[idMatHang] += soLuong;
                }
                else
                {
                    danhSachMatHang[idMatHang] = soLuong;
                }
            }

            // Kiểm tra phải có ít nhất 1 mặt hàng hợp lệ
            if (danhSachMatHang.Count == 0)
            {
                MessageBox.Show("Đơn hàng phải có ít nhất 1 mặt hàng!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm lưu dữ liệu
            LuuDonHang(Convert.ToInt32(cboNCC.SelectedValue), dtpNgayNhap.Value.Date, danhSachMatHang);
        }

        /// <summary>
        /// Lưu đơn hàng vào CSDL sử dụng SQL Transaction.
        /// Đảm bảo tính toàn vẹn dữ liệu: Insert DonNhapHang -> Insert ChiTietDonNhap.
        /// Nếu lỗi ở bất kỳ bước nào, toàn bộ sẽ được Rollback.
        /// </summary>
        /// <param name="idNCC">ID Nhà cung cấp.</param>
        /// <param name="ngayNhap">Ngày nhập hàng do người dùng chọn.</param>
        /// <param name="danhSachMatHang">Dictionary chứa ID Mặt hàng và Số lượng.</param>
        private void LuuDonHang(int idNCC, DateTime ngayNhap, Dictionary<int, int> danhSachMatHang)
        {
            // Quản lý connection/transaction thủ công bằng try/catch/finally để Rollback khi lỗi
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = DatabaseHelper.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                // Bước 1: Insert vào bảng DonNhapHang và lấy ID vừa tạo
                // Dùng SCOPE_IDENTITY() thay vì @@IDENTITY để tránh lấy nhầm ID từ trigger
                string sqlDonNhap = "INSERT INTO DonNhapHang (FK_Id_NCC, NgayNhap) VALUES (@IdNCC, @NgayNhap); SELECT SCOPE_IDENTITY();";
                SqlCommand cmdDonNhap = new SqlCommand(sqlDonNhap, connection, transaction);
                cmdDonNhap.Parameters.AddWithValue("@IdNCC", idNCC);
                cmdDonNhap.Parameters.AddWithValue("@NgayNhap", ngayNhap);

                int idDonNhap = Convert.ToInt32(cmdDonNhap.ExecuteScalar());

                // Bước 2: Insert từng mặt hàng vào bảng ChiTietDonNhap
                foreach (var item in danhSachMatHang)
                {
                    string sqlChiTiet = @"
                        INSERT INTO ChiTietDonNhap (FK_Id_DonNhapHang, FK_Id_MatHang, Count) 
                        VALUES (@IdDonNhap, @IdMatHang, @SoLuong)";

                    SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, connection, transaction);
                    cmdChiTiet.Parameters.AddWithValue("@IdDonNhap", idDonNhap);
                    cmdChiTiet.Parameters.AddWithValue("@IdMatHang", item.Key);
                    cmdChiTiet.Parameters.AddWithValue("@SoLuong", item.Value);
                    cmdChiTiet.ExecuteNonQuery();
                }

                transaction.Commit();

                MessageBox.Show($"Tạo đơn nhập hàng thành công! Mã đơn: {idDonNhap}", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Nếu có bất kỳ lỗi nào, hoàn tác Transaction
                transaction?.Rollback();

                MessageBox.Show($"Lỗi khi lưu đơn hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Hủy": Đóng form mà không lưu dữ liệu.
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
