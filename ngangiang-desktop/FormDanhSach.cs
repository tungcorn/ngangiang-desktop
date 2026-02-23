using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form chính của ứng dụng Desktop — Quản lý Đơn nhập hàng.
    /// Chức năng:
    ///   1. Bảng NCC có checkbox để lọc đơn hàng.
    ///   2. Danh sách đơn hàng với cột Mặt hàng, Tổng tiền.
    ///   3. Nút hành động inline (Chi tiết / Sửa / Xóa) cho từng dòng.
    ///   4. Hiển thị Tổng số đơn và Tổng cộng tiền.
    /// Cấu trúc cột DataGridView được định nghĩa trong SetupDgvNCC() và SetupDgvDonNhap().
    /// </summary>
    public partial class FormDanhSach : Form
    {
        public FormDanhSach()
        {
            InitializeComponent();
            // Đặt kích thước ở đây để không bị VS Designer reset
            this.ClientSize = new System.Drawing.Size(1010, 608);
            this.MinimumSize = new System.Drawing.Size(1026, 640);
            SetupDgvNCC();
            SetupDgvDonNhap();
        }

        // ====================================================================
        // Khởi tạo cột DataGridView
        // ====================================================================

        /// <summary>
        /// Khai báo cột cho bảng Nhà Cung Cấp (NCC).
        /// AutoGenerateColumns = false để dùng cột thủ công.
        /// </summary>
        private void SetupDgvNCC()
        {
            dgvNCC.AutoGenerateColumns = false;
            dgvNCC.AllowUserToAddRows    = false;
            dgvNCC.AllowUserToDeleteRows = false;
            dgvNCC.AllowUserToResizeRows = false;
            dgvNCC.MultiSelect           = true;

            // Áp dụng style cho header
            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240),
                Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
            };
            dgvNCC.ColumnHeadersDefaultCellStyle = headerStyle;

            // Checkbox tick lọc
            var colChon = new DataGridViewCheckBoxColumn
            {
                Name       = "colChon",
                HeaderText = "✔",
                FalseValue = false,
                TrueValue  = true,
                Width      = 40
            };

            // ID NCC — ẩn
            var colIdNCC = new DataGridViewTextBoxColumn
            {
                Name             = "colIdNCC",
                DataPropertyName = "Id_NCC",
                HeaderText       = "ID",
                ReadOnly         = true,
                Visible          = false
            };

            // Tên NCC — 40%
            var colTenNCC = new DataGridViewTextBoxColumn
            {
                Name             = "colTenNCC",
                DataPropertyName = "Ten_NCC",
                HeaderText       = "Tên nhà cung cấp",
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight       = 40F,
                ReadOnly         = true
            };

            // Địa chỉ — 60%
            var colDiaChi = new DataGridViewTextBoxColumn
            {
                Name             = "colDiaChi",
                DataPropertyName = "DiaChi",
                HeaderText       = "Địa chỉ",
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight       = 60F,
                ReadOnly         = true
            };

            // Email — 180px cố định
            var colEmail = new DataGridViewTextBoxColumn
            {
                Name             = "colEmail",
                DataPropertyName = "Email",
                HeaderText       = "Email",
                Width            = 180,
                ReadOnly         = true
            };

            dgvNCC.Columns.AddRange(colChon, colIdNCC, colTenNCC, colDiaChi, colEmail);

            dgvNCC.CellContentClick += DgvNCC_CellContentClick;
            dgvNCC.CellClick        += DgvNCC_CellClick;
        }

        /// <summary>
        /// Khai báo cột cho bảng Đơn nhập hàng.
        /// Dùng DataPropertyName khớp với tên cột alias trong SQL query.
        /// </summary>
        private void SetupDgvDonNhap()
        {
            dgvDonNhap.AutoGenerateColumns = false;
            dgvDonNhap.AllowUserToAddRows    = false;
            dgvDonNhap.AllowUserToDeleteRows = false;

            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240),
                Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
            };
            dgvDonNhap.ColumnHeadersDefaultCellStyle = headerStyle;

            // Mã đơn — 70px
            var colMaDon = new DataGridViewTextBoxColumn
            {
                Name             = "colMaDon",
                DataPropertyName = "Mã đơn",
                HeaderText       = "Mã đơn",
                Width            = 70,
                ReadOnly         = true
            };

            // Nhà cung cấp — tự giãn 35% phần còn lại
            var colNCC = new DataGridViewTextBoxColumn
            {
                Name             = "colNhaCungCap",
                DataPropertyName = "Nhà cung cấp",
                HeaderText       = "Nhà cung cấp",
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight       = 35F,
                ReadOnly         = true
            };

            // Ngày nhập — 90px căn giữa
            var colNgayNhap = new DataGridViewTextBoxColumn
            {
                Name             = "colNgayNhap",
                DataPropertyName = "Ngày nhập",
                HeaderText       = "Ngày nhập",
                Width            = 90,
                ReadOnly         = true
            };
            colNgayNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Mặt hàng — Fill 55%, WrapMode để hiển thị tên dài
            var colMatHang = new DataGridViewTextBoxColumn
            {
                Name             = "colMatHang",
                DataPropertyName = "Mặt hàng",
                HeaderText       = "Mặt hàng",
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight       = 55F,
                ReadOnly         = true
            };
            colMatHang.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Số MH — 55px căn giữa
            var colSoMH = new DataGridViewTextBoxColumn
            {
                Name             = "colSoMatHang",
                DataPropertyName = "Số mặt hàng",
                HeaderText       = "Số MH",
                Width            = 55,
                ReadOnly         = true
            };
            colSoMH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Tổng tiền — 120px căn phải N0
            var colTongTien = new DataGridViewTextBoxColumn
            {
                Name             = "colTongTien",
                DataPropertyName = "Tổng tiền",
                HeaderText       = "Tổng tiền",
                Width            = 120,
                ReadOnly         = true
            };
            colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongTien.DefaultCellStyle.Format    = "N0";

            // Nút Chi tiết — xanh
            var colChiTiet = new DataGridViewButtonColumn
            {
                Name                       = "btnChiTiet",
                HeaderText                 = "",
                Text                       = "👁",
                UseColumnTextForButtonValue = true,
                FlatStyle                  = FlatStyle.Flat,
                Width                      = 36
            };
            colChiTiet.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            colChiTiet.DefaultCellStyle.ForeColor = System.Drawing.Color.White;

            // Nút Sửa — vàng
            var colSua = new DataGridViewButtonColumn
            {
                Name                       = "btnSua",
                HeaderText                 = "",
                Text                       = "✏️",
                UseColumnTextForButtonValue = true,
                FlatStyle                  = FlatStyle.Flat,
                Width                      = 36
            };
            colSua.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            colSua.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            // Nút Xóa — đỏ
            var colXoa = new DataGridViewButtonColumn
            {
                Name                       = "btnXoa",
                HeaderText                 = "",
                Text                       = "🗑️",
                UseColumnTextForButtonValue = true,
                FlatStyle                  = FlatStyle.Flat,
                Width                      = 36
            };
            colXoa.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            colXoa.DefaultCellStyle.ForeColor = System.Drawing.Color.White;

            dgvDonNhap.Columns.AddRange(
                colMaDon, colNCC, colNgayNhap, colMatHang, colSoMH, colTongTien,
                colChiTiet, colSua, colXoa);

            dgvDonNhap.CellClick += DgvDonNhap_CellClick;
        }

        // ====================================================================
        // Form Load
        // ====================================================================

        private void FormDanhSach_Load(object sender, EventArgs e)
        {
            LoadDanhSachNCC();
            LoadDanhSachDonNhap();
            dgvDonNhap.ClearSelection();
        }

        // ====================================================================
        // Sự kiện DataGridView NCC
        // ====================================================================

        /// <summary>
        /// Click vào bất kỳ ô nào trong dòng NCC → đảo trạng thái checkbox lọc.
        /// </summary>
        private void DgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Chỉ xử lý nếu click vào cột ngoài checkbox (cột checkbox tự xử lý qua CellContentClick)
            if (dgvNCC.Columns[e.ColumnIndex].Name != "colChon")
            {
                var cell = dgvNCC.Rows[e.RowIndex].Cells["colChon"];
                cell.Value = !(bool)(cell.Value ?? false);
                dgvNCC.CommitEdit(DataGridViewDataErrorContexts.Commit);
                LoadDanhSachDonNhap();
            }
        }

        /// <summary>
        /// Click trực tiếp vào ô checkbox → commit ngay và reload đơn hàng.
        /// </summary>
        private void DgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvNCC.Columns[e.ColumnIndex].Name == "colChon")
            {
                dgvNCC.CommitEdit(DataGridViewDataErrorContexts.Commit);
                LoadDanhSachDonNhap();
            }
        }

        // ====================================================================
        // Sự kiện DataGridView Đơn nhập
        // ====================================================================

        /// <summary>
        /// Click vào bảng đơn nhập:
        ///   - btnChiTiet / click vào ô dữ liệu → mở FormChiTiet
        ///   - btnSua     → thông báo đang phát triển
        ///   - btnXoa     → xác nhận và thông báo đang phát triển
        /// </summary>
        private void DgvDonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int    idDonNhap = Convert.ToInt32(dgvDonNhap.Rows[e.RowIndex].Cells["colMaDon"].Value);
            string colName   = dgvDonNhap.Columns[e.ColumnIndex].Name;

            if (colName == "btnChiTiet")
            {
                OpenChiTiet(idDonNhap);
            }
            else if (colName == "btnSua")
            {
                MessageBox.Show($"Chức năng sửa đơn #{idDonNhap} đang được phát triển.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (colName == "btnXoa")
            {
                var confirm = MessageBox.Show(
                    $"Bạn có chắc muốn xóa đơn #{idDonNhap}?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    MessageBox.Show($"Chức năng xóa đơn #{idDonNhap} đang được phát triển.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Click vào cột dữ liệu → cũng mở chi tiết
                OpenChiTiet(idDonNhap);
            }
        }

        // ====================================================================
        // Load dữ liệu
        // ====================================================================

        /// <summary>
        /// Tải danh sách NCC vào dgvNCC. Mặc định không tick NCC nào.
        /// </summary>
        private void LoadDanhSachNCC()
        {
            try
            {
                string    query = "SELECT Id_NCC, Ten_NCC, DiaChi, Email FROM NCC ORDER BY Ten_NCC";
                DataTable dt    = DatabaseHelper.ExecuteQuery(query);
                dgvNCC.DataSource = dt;

                // Reset checkbox — DataSource gán lại nên phải set lại giá trị False
                foreach (DataGridViewRow row in dgvNCC.Rows)
                    row.Cells["colChon"].Value = false;

                dgvNCC.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách NCC: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Trả về danh sách Id_NCC đang được tick chọn trong dgvNCC.
        /// </summary>
        private List<int> GetSelectedNCCIds()
        {
            var ids = new List<int>();
            foreach (DataGridViewRow row in dgvNCC.Rows)
            {
                var v = row.Cells["colChon"].Value;
                if (v != null && (bool)v)
                    ids.Add(Convert.ToInt32(row.Cells["colIdNCC"].Value));
            }
            return ids;
        }

        /// <summary>
        /// Tải danh sách đơn nhập hàng.
        /// Lọc theo NCC đã tick (nếu không tick → hiển thị tất cả).
        /// Cột "Mặt hàng" dùng STRING_AGG để ghép tên MH bằng dấu phẩy.
        /// </summary>
        private void LoadDanhSachDonNhap()
        {
            try
            {
                var    ids         = GetSelectedNCCIds();
                var    conditions  = new List<string>();

                if (ids.Count > 0)
                    conditions.Add($"d.FK_Id_NCC IN ({string.Join(",", ids)})");

                // Lọc theo khoảng ngày nhập (chỉ lọc khi checkbox trong DateTimePicker được tick)
                if (dtpTuNgay.Checked)
                    conditions.Add($"d.NgayNhap >= '{dtpTuNgay.Value:yyyy-MM-dd}'");
                if (dtpDenNgay.Checked)
                    conditions.Add($"d.NgayNhap <= '{dtpDenNgay.Value:yyyy-MM-dd}'");

                string whereClause = conditions.Count > 0
                    ? "WHERE " + string.Join(" AND ", conditions)
                    : "";

                string query = $@"
                    SELECT
                        d.Id_DonNhapHang        AS [Mã đơn],
                        n.Ten_NCC               AS [Nhà cung cấp],
                        FORMAT(d.NgayNhap, 'dd/MM/yyyy') AS [Ngày nhập],
                        STRING_AGG(m.Ten_MatHang, ', ')
                                                AS [Mặt hàng],
                        COUNT(c.FK_Id_MatHang)  AS [Số mặt hàng],
                        ISNULL(SUM(c.Count * m.DonGia), 0) AS [Tổng tiền]
                    FROM DonNhapHang d
                    INNER JOIN NCC n ON d.FK_Id_NCC = n.Id_NCC
                    LEFT JOIN ChiTietDonNhap c ON d.Id_DonNhapHang = c.FK_Id_DonNhapHang
                    LEFT JOIN MatHang m ON c.FK_Id_MatHang = m.Id_MatHang
                    {whereClause}
                    GROUP BY d.Id_DonNhapHang, n.Ten_NCC, d.NgayNhap
                    ORDER BY d.Id_DonNhapHang DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvDonNhap.DataSource        = dt;
                dgvDonNhap.AutoSizeRowsMode  = DataGridViewAutoSizeRowsMode.AllCells;

                decimal grandTotal = 0;
                foreach (DataRow row in dt.Rows)
                    grandTotal += Convert.ToDecimal(row["Tổng tiền"]);

                lblTongSoDon.Text  = $"Tổng: {dt.Rows.Count} đơn";
                lblGrandTotal.Text = $"Tổng cộng: {grandTotal:N0} ₫";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn nhập: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================================================================
        // Helpers & nút bấm
        // ====================================================================

        private void OpenChiTiet(int idDonNhap)
        {
            new FormChiTiet(idDonNhap).ShowDialog();
        }

        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            var form = new FormTaoDon();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNCC();
                LoadDanhSachDonNhap();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Checked = false;
            dtpDenNgay.Checked = false;
            LoadDanhSachNCC();
            LoadDanhSachDonNhap();
        }

        /// <summary>
        /// Click nút "Lọc" theo khoảng ngày nhập.
        /// </summary>
        private void btnLocNgay_Click(object sender, EventArgs e)
        {
            LoadDanhSachDonNhap();
        }
    }
}
