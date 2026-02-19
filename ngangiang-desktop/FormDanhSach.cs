using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form chính của ứng dụng Desktop.
    /// Chức năng:
    /// 1. Hiển thị bảng NCC có cột checkbox để lọc — pattern "listcheckbox" chuẩn enterprise.
    /// 2. Hiển thị danh sách đơn nhập hàng theo NCC đã tick.
    /// 3. Xem chi tiết mặt hàng và thành tiền của từng đơn.
    /// 4. Cung cấp lối tắt để tạo đơn hàng mới hoặc làm mới dữ liệu.
    /// </summary>
    public partial class FormDanhSach : Form
    {
        public FormDanhSach()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load Form: Thiết lập bảng NCC, tải dữ liệu, trạng thái mặc định.
        /// </summary>
        private void FormDanhSach_Load(object sender, EventArgs e)
        {
            SetupDgvNCC();
            LoadDanhSachNCC();
            LoadDanhSachDonNhap();

            // Xóa selection mặc định
            dgvDonNhap.ClearSelection();
            dgvChiTiet.DataSource = null;
        }

        /// <summary>
        /// Thiết lập cấu trúc cột cho bảng NCC:
        /// Cột 1: CheckBox (Chọn) — để tick lọc
        /// Cột 2-4: Tên NCC, Địa chỉ, Email — thông tin readonly
        /// Lưu Id_NCC trong cột ẩn để dùng khi xây dựng câu lệnh lọc SQL.
        /// </summary>
        private void SetupDgvNCC()
        {
            dgvNCC.Columns.Clear();
            dgvNCC.AutoGenerateColumns = false;

            // Cột checkbox
            var colChon = new DataGridViewCheckBoxColumn
            {
                Name = "Chọn",
                HeaderText = "✔",
                Width = 40,
                FalseValue = false,
                TrueValue = true
            };
            dgvNCC.Columns.Add(colChon);

            // Cột ẩn lưu Id_NCC
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "Id_NCC",
                HeaderText = "ID",
                DataPropertyName = "Id_NCC",
                Visible = false
            };
            dgvNCC.Columns.Add(colId);

            // Cột tên NCC
            var colTen = new DataGridViewTextBoxColumn
            {
                Name = "Ten_NCC",
                HeaderText = "Tên nhà cung cấp",
                DataPropertyName = "Ten_NCC",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgvNCC.Columns.Add(colTen);

            // Cột địa chỉ
            var colDiaChi = new DataGridViewTextBoxColumn
            {
                Name = "DiaChi",
                HeaderText = "Địa chỉ",
                DataPropertyName = "DiaChi",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgvNCC.Columns.Add(colDiaChi);

            // Cột email
            var colEmail = new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                ReadOnly = true,
                Width = 180
            };
            dgvNCC.Columns.Add(colEmail);

            // Thiết lập giao diện
            dgvNCC.RowHeadersVisible = false;
            dgvNCC.AllowUserToResizeRows = false;
            dgvNCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Xử lý khi tick/bỏ tick checkbox → load lại đơn hàng
            dgvNCC.CellContentClick += DgvNCC_CellContentClick;
            dgvNCC.CellClick += DgvNCC_CellClick;
        }

        /// <summary>
        /// Sự kiện khi click vào dòng trong bảng NCC.
        /// Cho phép đảo trạng thái checkbox khi click vào bất kỳ ô nào trên dòng.
        /// </summary>
        private void DgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nếu không phải click trực tiếp vào cột checkbox (cột checkbox đã có handler riêng)
            if (dgvNCC.Columns[e.ColumnIndex].Name != "Chọn")
            {
                var cell = dgvNCC.Rows[e.RowIndex].Cells["Chọn"];
                cell.Value = !(bool)cell.Value; // Đảo trạng thái
                
                dgvNCC.CommitEdit(DataGridViewDataErrorContexts.Commit);
                LoadDanhSachDonNhap();
            }
        }

        /// <summary>
        /// Sự kiện khi click vào ô trong bảng NCC.
        /// Nếu click vào cột checkbox → commit thay đổi ngay và load lại đơn hàng.
        /// </summary>
        private void DgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvNCC.Columns[e.ColumnIndex].Name == "Chọn")
            {
                // Commit ngay để lấy giá trị checkbox mới nhất
                dgvNCC.CommitEdit(DataGridViewDataErrorContexts.Commit);
                LoadDanhSachDonNhap();
            }
        }

        /// <summary>
        /// Tải danh sách NCC từ CSDL vào bảng dgvNCC.
        /// Mặc định không tick NCC nào = hiển thị tất cả đơn hàng.
        /// </summary>
        private void LoadDanhSachNCC()
        {
            try
            {
                string query = "SELECT Id_NCC, Ten_NCC, DiaChi, Email FROM NCC ORDER BY Ten_NCC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvNCC.DataSource = dt;

                // Mặc định: tất cả checkbox unchecked
                foreach (DataGridViewRow row in dgvNCC.Rows)
                {
                    row.Cells["Chọn"].Value = false;
                }

                dgvNCC.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách NCC: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy danh sách Id_NCC đã được tick checkbox trong bảng NCC.
        /// </summary>
        private System.Collections.Generic.List<int> GetSelectedNCCIds()
        {
            var ids = new System.Collections.Generic.List<int>();
            foreach (DataGridViewRow row in dgvNCC.Rows)
            {
                var cellValue = row.Cells["Chọn"].Value;
                if (cellValue != null && (bool)cellValue == true)
                {
                    ids.Add(Convert.ToInt32(row.Cells["Id_NCC"].Value));
                }
            }
            return ids;
        }

        /// <summary>
        /// Tải danh sách đơn nhập hàng từ CSDL.
        /// Nếu có NCC được tick → lọc WHERE FK_Id_NCC IN (...)
        /// Nếu không tick NCC nào → hiển thị tất cả đơn hàng.
        /// </summary>
        private void LoadDanhSachDonNhap()
        {
            try
            {
                var selectedIds = GetSelectedNCCIds();

                // Không tick NCC nào = hiện tất cả; có tick = lọc theo danh sách
                string whereClause = "";
                if (selectedIds.Count > 0)
                    whereClause = $"WHERE d.FK_Id_NCC IN ({string.Join(",", selectedIds)})";

                string query = $@"
                    SELECT 
                        d.Id_DonNhapHang AS [Mã đơn],
                        n.Ten_NCC AS [Nhà cung cấp],
                        COUNT(c.FK_Id_MatHang) AS [Số mặt hàng],
                        ISNULL(SUM(c.Count * m.DonGia), 0) AS [Tổng tiền]
                    FROM DonNhapHang d
                    INNER JOIN NCC n ON d.FK_Id_NCC = n.Id_NCC
                    LEFT JOIN ChiTietDonNhap c ON d.Id_DonNhapHang = c.FK_Id_DonNhapHang
                    LEFT JOIN MatHang m ON c.FK_Id_MatHang = m.Id_MatHang
                    {whereClause}
                    GROUP BY d.Id_DonNhapHang, n.Ten_NCC
                    ORDER BY d.Id_DonNhapHang DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvDonNhap.DataSource = dt;
                dgvDonNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Format cột Tổng tiền trong bảng
                if (dgvDonNhap.Columns["Tổng tiền"] != null)
                    dgvDonNhap.Columns["Tổng tiền"].DefaultCellStyle.Format = "N0";

                // Tính TỔNG CỘNG của tất cả các đơn đang được lọc
                decimal grandTotal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grandTotal += Convert.ToDecimal(row["Tổng tiền"]);
                }

                lblTongSoDon.Text = $"Tổng số đơn: {dt.Rows.Count}";
                lblTongTien.Text = $"Tổng cộng: {grandTotal:N0} ₫";
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
                int idDonNhap = Convert.ToInt32(dgvDonNhap.SelectedRows[0].Cells["Mã đơn"].Value);
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

                if (dgvChiTiet.Columns["Đơn giá"] != null)
                    dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                if (dgvChiTiet.Columns["Thành tiền"] != null)
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn nhập: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mở form Tạo đơn mới (Modal Dialog).
        /// </summary>
        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            FormTaoDon formTaoDon = new FormTaoDon();
            if (formTaoDon.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNCC();
                LoadDanhSachDonNhap();
                dgvChiTiet.DataSource = null;
            }
        }

        /// <summary>
        /// Nút Làm mới: Tải lại toàn bộ dữ liệu từ CSDL.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachNCC();
            LoadDanhSachDonNhap();
            dgvChiTiet.DataSource = null;
        }

        /// <summary>
        /// Nút Bỏ chọn: Bỏ tick toàn bộ checkbox NCC, hiện lại tất cả đơn hàng.
        /// </summary>
        private void btnTatCa_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvNCC.Rows)
            {
                row.Cells["Chọn"].Value = false;
            }
            LoadDanhSachDonNhap();
        }

        /// <summary>
        /// Nút Xem chi tiết: Chức năng đang phát triển.
        /// </summary>
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDonNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xem chi tiết.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Chức năng xem chi tiết đang được phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Nút Sửa: Chức năng đang phát triển.
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDonNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để sửa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Chức năng sửa đơn đang được phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Nút Xóa: Chức năng đang phát triển.
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDonNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xóa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Chức năng xóa đơn đang được phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
