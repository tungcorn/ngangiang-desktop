using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form chi tiết đơn nhập hàng — tương đương Modal trên Web.
    /// Bố cục:
    ///   - Panel header (pnlInfo): MÃ ĐƠN / NHÀ CUNG CẤP / SỐ MẶT HÀNG
    ///   - Bảng mặt hàng (dgvChiTiet): cột định nghĩa trong FormChiTiet.Designer.cs
    ///   - Footer: TỔNG CỘNG (đỏ đậm) + nút Đóng
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

        // ====================================================================
        // Load dữ liệu
        // ====================================================================

        /// <summary>
        /// Tải thông tin tóm tắt: Mã đơn, Tên NCC, Số mặt hàng → điền vào pnlInfo.
        /// Dùng SqlParameter để chống SQL Injection.
        /// </summary>
        private void LoadHeaderInfo()
        {
            try
            {
                const string query = @"
                    SELECT
                        d.Id_DonNhapHang,
                        n.Ten_NCC,
                        d.NgayNhap,
                        COUNT(c.FK_Id_MatHang) AS SoMatHang
                    FROM DonNhapHang d
                    INNER JOIN NCC n ON d.FK_Id_NCC = n.Id_NCC
                    LEFT JOIN ChiTietDonNhap c ON d.Id_DonNhapHang = c.FK_Id_DonNhapHang
                    WHERE d.Id_DonNhapHang = @IdDonNhap
                    GROUP BY d.Id_DonNhapHang, n.Ten_NCC, d.NgayNhap";

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
                    lblMaDon.Text     = $"#{row["Id_DonNhapHang"]}";
                    lblNCC.Text       = row["Ten_NCC"].ToString();
                    lblNgayNhap.Text  = Convert.ToDateTime(row["NgayNhap"]).ToString("dd/MM/yyyy");
                    lblSoMatHang.Text = row["SoMatHang"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách mặt hàng trong đơn nhập.
        /// Cột STT dùng ROW_NUMBER() — tương đương cột # trên Web Modal.
        /// Dùng SqlParameter để chống SQL Injection.
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

                dgvChiTiet.DataSource       = dt;
                // Kích hoạt tự điều chỉnh chiều cao SAU khi có dữ liệu
                // → bắt buộc để WrapMode trên cột "Mặt hàng" thực sự có hiệu lực
                dgvChiTiet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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

        // ====================================================================
        // Nút bấm
        // ====================================================================

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
