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
    /// 3. Mỗi dòng đơn hàng có nút Chi tiết / Sửa / Xóa inline.
    /// 4. Xem chi tiết mặt hàng và thành tiền của từng đơn.
    /// 5. Cung cấp lối tắt để tạo đơn hàng mới hoặc làm mới dữ liệu.
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
            SetupDgvDonNhap();
            LoadDanhSachNCC();
            LoadDanhSachDonNhap();

            // Xóa selection mặc định
            dgvDonNhap.ClearSelection();
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
        /// Thiết lập cột cho bảng Đơn nhập hàng.
        /// Các cột dữ liệu + 3 cột nút hành động inline: Chi tiết, Sửa, Xóa.
        /// </summary>
        private void SetupDgvDonNhap()
        {
            dgvDonNhap.AutoGenerateColumns = false;
            dgvDonNhap.Columns.Clear();

            dgvDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mã đơn",
                HeaderText = "Mã đơn",
                DataPropertyName = "Mã đơn",
                ReadOnly = true,
                Width = 70
            });
            dgvDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nhà cung cấp",
                HeaderText = "Nhà cung cấp",
                DataPropertyName = "Nhà cung cấp",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Số mặt hàng",
                HeaderText = "Số mặt hàng",
                DataPropertyName = "Số mặt hàng",
                ReadOnly = true,
                Width = 100
            });

            var colTongTien = new DataGridViewTextBoxColumn
            {
                Name = "Tổng tiền",
                HeaderText = "Tổng tiền",
                DataPropertyName = "Tổng tiền",
                ReadOnly = true,
                Width = 120,
            };
            colTongTien.DefaultCellStyle.Format = "N0";
            colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDonNhap.Columns.Add(colTongTien);

            // --- Nút hành động inline ---
            // Nút Chi tiết
            var colChiTiet = new DataGridViewButtonColumn
            {
                Name = "btnChiTiet",
                HeaderText = "",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                Width = 75,
                FlatStyle = FlatStyle.Flat,
            };
            colChiTiet.DefaultCellStyle.BackColor = Color.FromArgb(13, 110, 253);
            colChiTiet.DefaultCellStyle.ForeColor = Color.White;
            dgvDonNhap.Columns.Add(colChiTiet);

            // Nút Sửa
            var colSua = new DataGridViewButtonColumn
            {
                Name = "btnSua",
                HeaderText = "",
                Text = "Sửa",
                UseColumnTextForButtonValue = true,
                Width = 55,
                FlatStyle = FlatStyle.Flat,
            };
            colSua.DefaultCellStyle.BackColor = Color.FromArgb(255, 193, 7);
            colSua.DefaultCellStyle.ForeColor = Color.Black;
            dgvDonNhap.Columns.Add(colSua);

            // Nút Xóa
            var colXoa = new DataGridViewButtonColumn
            {
                Name = "btnXoa",
                HeaderText = "",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 55,
                FlatStyle = FlatStyle.Flat,
            };
            colXoa.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            colXoa.DefaultCellStyle.ForeColor = Color.White;
            dgvDonNhap.Columns.Add(colXoa);

            dgvDonNhap.CellClick += DgvDonNhap_CellClick;
        }

        /// <summary>
        /// Sự kiện khi click vào ô trong bảng Đơn nhập.
        /// Xử lý: chọn dòng để xem chi tiết, hoặc nhấn nút hành động inline.
        /// </summary>
        private void DgvDonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int idDonNhap = Convert.ToInt32(dgvDonNhap.Rows[e.RowIndex].Cells["Mã đơn"].Value);
            string colName = dgvDonNhap.Columns[e.ColumnIndex].Name;

            if (colName == "btnChiTiet")
            {
                // Mở Form chi tiết riêng biệt
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
                // Click vào các cột dữ liệu → mở Form chi tiết
                OpenChiTiet(idDonNhap);
            }
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
                cell.Value = !(bool)(cell.Value ?? false); // Đảo trạng thái

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

                // Tính TỔNG CỘNG của tất cả các đơn đang được lọc
                decimal grandTotal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grandTotal += Convert.ToDecimal(row["Tổng tiền"]);
                }

                lblTongSoDon.Text = $"Tổng số đơn: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn nhập: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mở FormChiTiet theo ID đơn hàng dưới dạng Modal Dialog.
        /// </summary>
        private void OpenChiTiet(int idDonNhap)
        {
            FormChiTiet formChiTiet = new FormChiTiet(idDonNhap);
            formChiTiet.ShowDialog();
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
            }
        }

        /// <summary>
        /// Nút Làm mới: Tải lại toàn bộ dữ liệu từ CSDL.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachNCC();
            LoadDanhSachDonNhap();
        }
    }
}
