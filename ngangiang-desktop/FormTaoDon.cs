using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ngangiang_desktop
{
    public partial class FormTaoDon : Form
    {
        public FormTaoDon()
        {
            InitializeComponent();
        }

        private void FormTaoDon_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
            LoadMatHang();
            ThemDongMacDinh();
        }

        /// <summary>
        /// Load danh sách Nhà cung cấp vào ComboBox
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
                cboNCC.SelectedIndex = -1; // Không chọn gì mặc định
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách NCC: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách Mặt hàng để dùng cho ComboBox trong DataGridView
        /// </summary>
        private void LoadMatHang()
        {
            try
            {
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

                // Tạo cột ComboBox trong DataGridView
                DataGridViewComboBoxColumn colMatHang = new DataGridViewComboBoxColumn();
                colMatHang.HeaderText = "Mặt hàng";
                colMatHang.Name = "colMatHang";
                colMatHang.DisplayMember = "DisplayText";
                colMatHang.ValueMember = "Id_MatHang";
                colMatHang.DataSource = dt;
                colMatHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colMatHang.MinimumWidth = 300;

                dgvMatHang.Columns.Add(colMatHang);

                // Cột Số lượng
                DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();
                colSoLuong.HeaderText = "Số lượng";
                colSoLuong.Name = "colSoLuong";
                colSoLuong.Width = 100;
                dgvMatHang.Columns.Add(colSoLuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách mặt hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thêm 1 dòng trống mặc định
        /// </summary>
        private void ThemDongMacDinh()
        {
            dgvMatHang.Rows.Add();
        }

        /// <summary>
        /// Nút Thêm dòng
        /// </summary>
        private void btnThemDong_Click(object sender, EventArgs e)
        {
            dgvMatHang.Rows.Add();
        }

        /// <summary>
        /// Nút Xóa dòng
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
        /// Nút Lưu đơn hàng
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy danh sách mặt hàng từ DataGridView
            Dictionary<int, int> danhSachMatHang = new Dictionary<int, int>();

            foreach (DataGridViewRow row in dgvMatHang.Rows)
            {
                if (row.IsNewRow) continue;

                var cellMatHang = row.Cells["colMatHang"].Value;
                var cellSoLuong = row.Cells["colSoLuong"].Value;

                if (cellMatHang == null || cellSoLuong == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin mặt hàng và số lượng!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idMatHang = Convert.ToInt32(cellMatHang);
                int soLuong;

                if (!int.TryParse(cellSoLuong.ToString(), out soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gộp mặt hàng trùng (cộng dồn số lượng)
                if (danhSachMatHang.ContainsKey(idMatHang))
                {
                    danhSachMatHang[idMatHang] += soLuong;
                }
                else
                {
                    danhSachMatHang[idMatHang] = soLuong;
                }
            }

            if (danhSachMatHang.Count == 0)
            {
                MessageBox.Show("Đơn hàng phải có ít nhất 1 mặt hàng!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lưu vào database với Transaction
            LuuDonHang(Convert.ToInt32(cboNCC.SelectedValue), danhSachMatHang);
        }

        /// <summary>
        /// Lưu đơn hàng vào database (dùng Transaction)
        /// </summary>
        private void LuuDonHang(int idNCC, Dictionary<int, int> danhSachMatHang)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = DatabaseHelper.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                // 1. Insert vào DonNhapHang
                string sqlDonNhap = "INSERT INTO DonNhapHang (FK_Id_NCC) VALUES (@IdNCC); SELECT SCOPE_IDENTITY();";
                SqlCommand cmdDonNhap = new SqlCommand(sqlDonNhap, connection, transaction);
                cmdDonNhap.Parameters.AddWithValue("@IdNCC", idNCC);

                int idDonNhap = Convert.ToInt32(cmdDonNhap.ExecuteScalar());

                // 2. Insert vào ChiTietDonNhap
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

                // Commit transaction
                transaction.Commit();

                MessageBox.Show($"Tạo đơn nhập hàng thành công! Mã đơn: {idDonNhap}", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Rollback nếu có lỗi
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
        /// Nút Hủy
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
