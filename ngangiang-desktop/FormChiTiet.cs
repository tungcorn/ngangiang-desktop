using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form riêng hiển thị chi tiết đơn nhập hàng, đóng vai trò tương đương Modal trên Web.
    /// Bố cục:
    ///   - Panel header xám nhạt (#F8F9FA): 3 cụm MÃ ĐƠN / NHÀ CUNG CẤP / SỐ MẶT HÀNG
    ///   - Bảng mặt hàng: #, Mặt hàng, Đơn vị, Đơn giá, SL, Thành tiền
    ///   - Dưới: TỔNG CỘNG (đỏ đậm, bottom-right) + nút Đóng (bottom-left)
    /// </summary>
    public partial class FormChiTiet : Form
    {
        private readonly int _idDonNhap;

        public FormChiTiet(int idDonNhap)
        {
            InitializeComponent();
            _idDonNhap = idDonNhap;
        }

        private void FormChiTiet_Load(object sender, EventArgs e)
        {
            LoadHeaderInfo();
            LoadChiTietDonNhap();
        }

        /// <summary>
        /// Tải thông tin tóm tắt đơn hàng (Mã đơn, Tên NCC, Số mặt hàng)
        /// để hiển thị vào panel header — tương đương phần info trong Modal Bootstrap.
        /// Dùng SqlParameter (@IdDonNhap) để chống SQL Injection.
        /// </summary>
        private void LoadHeaderInfo()
        {
            try
            {
                const string query = @"
                    SELECT
                        d.Id_DonNhapHang,
                        n.Ten_NCC,
                        COUNT(c.FK_Id_MatHang) AS SoMatHang
                    FROM DonNhapHang d
                    INNER JOIN NCC n ON d.FK_Id_NCC = n.Id_NCC
                    LEFT JOIN ChiTietDonNhap c ON d.Id_DonNhapHang = c.FK_Id_DonNhapHang
                    WHERE d.Id_DonNhapHang = @IdDonNhap
                    GROUP BY d.Id_DonNhapHang, n.Ten_NCC";

                var dt = new DataTable();
                using (var conn = DatabaseHelper.CreateConnection())
                {
                    conn.Open();
                    var ada = new SqlDataAdapter(query, conn);
                    ada.SelectCommand.Parameters.AddWithValue("@IdDonNhap", _idDonNhap);
                    ada.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    lblMaDon.Text      = $"#{row["Id_DonNhapHang"]}";
                    lblNCC.Text        = row["Ten_NCC"].ToString();
                    lblSoMatHang.Text  = row["SoMatHang"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách mặt hàng trong đơn nhập từ CSDL.
        /// Dùng SqlParameter (@IdDonNhap) để chống SQL Injection.
        /// Cột STT tương ứng cột # trên Web Modal.
        /// </summary>
        private void LoadChiTietDonNhap()
        {
            try
            {
                const string query = @"
                    SELECT
                        ROW_NUMBER() OVER (ORDER BY m.Ten_MatHang) AS [STT],
                        m.Ten_MatHang   AS [Ten_MatHang],
                        m.DonViTinh     AS [DonViTinh],
                        m.DonGia        AS [DonGia],
                        c.Count         AS [SoLuong],
                        (c.Count * m.DonGia) AS [ThanhTien]
                    FROM ChiTietDonNhap c
                    INNER JOIN MatHang m ON c.FK_Id_MatHang = m.Id_MatHang
                    WHERE c.FK_Id_DonNhapHang = @IdDonNhap";

                var dt = new DataTable();
                using (var conn = DatabaseHelper.CreateConnection())
                {
                    conn.Open();
                    var ada = new SqlDataAdapter(query, conn);
                    ada.SelectCommand.Parameters.AddWithValue("@IdDonNhap", _idDonNhap);
                    ada.Fill(dt);
                }

                // AutoGenerateColumns = false → dùng định nghĩa cột thủ công bên dưới
                dgvChiTiet.AutoGenerateColumns = false;
                dgvChiTiet.Columns.Clear();

                // Cột #
                dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "STT",
                    HeaderText = "#",
                    Width = 36,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                // Cột Mặt hàng — Fill + WrapText để hiển thị đủ tên dài
                var colMH = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Ten_MatHang",
                    HeaderText = "Mặt hàng",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    ReadOnly = true,
                };
                colMH.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvChiTiet.Columns.Add(colMH);

                // Cột Đơn vị
                dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DonViTinh",
                    HeaderText = "Đơn vị",
                    Width = 70,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                // Cột Đơn giá
                var colDG = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DonGia",
                    HeaderText = "Đơn giá",
                    Width = 130,
                    ReadOnly = true,
                };
                colDG.DefaultCellStyle.Format = "N0";
                colDG.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChiTiet.Columns.Add(colDG);

                // Cột SL
                dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoLuong",
                    HeaderText = "SL",
                    Width = 48,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                // Cột Thành tiền
                var colTT = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ThanhTien",
                    HeaderText = "Thành tiền",
                    Width = 140,
                    ReadOnly = true,
                };
                colTT.DefaultCellStyle.Format = "N0";
                colTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChiTiet.Columns.Add(colTT);

                dgvChiTiet.DataSource = dt;

                // Tính TỔNG CỘNG (giống "TỔNG CỘNG:" trong Modal Web)
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                    total += Convert.ToDecimal(row["ThanhTien"]);

                lblTongTien.Text = $"TỔNG CỘNG:  {total:N0} ₫";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
