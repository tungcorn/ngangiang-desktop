using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    /// <summary>
    /// Form hiển thị chi tiết của một đơn nhập hàng.
    /// Đây là "form riêng" theo yêu cầu để đạt được sự nhất quán với bản Web (Modal).
    /// </summary>
    public partial class FormChiTiet : Form
    {
        private int _idDonNhap;

        public FormChiTiet(int idDonNhap)
        {
            InitializeComponent();
            _idDonNhap = idDonNhap;
        }

        private void FormChiTiet_Load(object sender, EventArgs e)
        {
            lblMaDon.Text = $"Mã đơn: #{_idDonNhap}";
            LoadChiTietDonNhap();
        }

        /// <summary>
        /// Tải danh sách chi tiết mặt hàng của đơn nhập từ CSDL.
        /// </summary>
        private void LoadChiTietDonNhap()
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
                    adapter.SelectCommand.Parameters.AddWithValue("@IdDonNhap", _idDonNhap);
                    adapter.Fill(dt);
                }

                dgvChiTiet.DataSource = dt;
                dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvChiTiet.Columns["Đơn giá"] != null)
                    dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                if (dgvChiTiet.Columns["Thành tiền"] != null)
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";

                // Tính tổng tiền đơn hàng để hiển thị
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["Thành tiền"]);
                }
                lblTongTien.Text = $"Tổng tiền đơn hàng: {total:N0} ₫";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
