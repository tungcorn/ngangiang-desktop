using System;
using System.Data;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    public partial class FormDanhSach : Form
    {
        public FormDanhSach()
        {
            InitializeComponent();
        }

        private void FormDanhSach_Load(object sender, EventArgs e)
        {
            LoadDanhSachDonNhap();
        }

        /// <summary>
        /// Load danh sách đơn nhập hàng vào DataGridView
        /// </summary>
        private void LoadDanhSachDonNhap()
        {
            try
            {
                string query = @"
                    SELECT 
                        d.Id_DonNhapHang AS [Mã đơn],
                        n.Ten_NCC AS [Nhà cung cấp],
                        n.DiaChi AS [Địa chỉ],
                        n.Email AS [Email]
                    FROM DonNhapHang d
                    INNER JOIN NCC n ON d.FK_Id_NCC = n.Id_NCC
                    ORDER BY d.Id_DonNhapHang DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvDonNhap.DataSource = dt;

                // Tự động điều chỉnh độ rộng cột
                dgvDonNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Hiển thị tổng số đơn
                lblTongSoDon.Text = $"Tổng: {dt.Rows.Count} đơn nhập hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn nhập: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi click vào 1 dòng trong danh sách đơn nhập → Load chi tiết
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
        /// Load chi tiết đơn nhập hàng vào DataGridView thứ 2
        /// </summary>
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

                DataTable dt = DatabaseHelper.ExecuteQuery(query, 
                    new System.Data.SqlClient.SqlParameter("@IdDonNhap", idDonNhap));

                dgvChiTiet.DataSource = dt;
                dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Format cột tiền tệ
                if (dgvChiTiet.Columns["Đơn giá"] != null)
                    dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                
                if (dgvChiTiet.Columns["Thành tiền"] != null)
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";

                // Tính và hiển thị tổng tiền đơn hàng
                decimal tongTien = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongTien += Convert.ToDecimal(row["Thành tiền"]);
                }
                lblTongTien.Text = $"Tổng tiền đơn hàng: {tongTien:N0} ₫";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn nhập: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Tạo đơn mới
        /// </summary>
        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            FormTaoDon formTaoDon = new FormTaoDon();
            if (formTaoDon.ShowDialog() == DialogResult.OK)
            {
                // Sau khi tạo đơn thành công → Refresh danh sách
                LoadDanhSachDonNhap();
            }
        }

        /// <summary>
        /// Nút Làm mới
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachDonNhap();
        }
    }
}
