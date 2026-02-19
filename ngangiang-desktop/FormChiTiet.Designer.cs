namespace ngangiang_desktop
{
    partial class FormChiTiet
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlInfo          = new System.Windows.Forms.Panel();
            this.lblMaDonCaption  = new System.Windows.Forms.Label();
            this.lblMaDon         = new System.Windows.Forms.Label();
            this.lblNCCCaption    = new System.Windows.Forms.Label();
            this.lblNCC           = new System.Windows.Forms.Label();
            this.lblSoMHCaption   = new System.Windows.Forms.Label();
            this.lblSoMatHang     = new System.Windows.Forms.Label();
            this.dgvChiTiet       = new System.Windows.Forms.DataGridView();
            // ── Cột dgvChiTiet ───────────────────────────────────────────────
            this.colSTT           = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenMH         = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonVi         = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia        = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong       = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongTien      = new System.Windows.Forms.Label();
            this.btnDong          = new System.Windows.Forms.Button();

            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();

            // ================================================================
            // pnlInfo — header xám nhạt, chứa 3 cụm thông tin đơn hàng
            // ================================================================
            this.pnlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.pnlInfo.BackColor   = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfo.Controls.Add(this.lblMaDonCaption);
            this.pnlInfo.Controls.Add(this.lblMaDon);
            this.pnlInfo.Controls.Add(this.lblNCCCaption);
            this.pnlInfo.Controls.Add(this.lblNCC);
            this.pnlInfo.Controls.Add(this.lblSoMHCaption);
            this.pnlInfo.Controls.Add(this.lblSoMatHang);
            this.pnlInfo.Location    = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name        = "pnlInfo";
            this.pnlInfo.Size        = new System.Drawing.Size(900, 90);
            this.pnlInfo.TabIndex    = 10;

            // ── Cụm MÃ ĐƠN ─────────────────────────────────────────────────
            this.lblMaDonCaption.AutoSize  = true;
            this.lblMaDonCaption.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMaDonCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblMaDonCaption.Location  = new System.Drawing.Point(16, 12);
            this.lblMaDonCaption.Name      = "lblMaDonCaption";
            this.lblMaDonCaption.Text      = "MÃ ĐƠN";

            this.lblMaDon.AutoSize  = true;
            this.lblMaDon.Font      = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblMaDon.ForeColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.lblMaDon.Location  = new System.Drawing.Point(14, 32);
            this.lblMaDon.Name      = "lblMaDon";
            this.lblMaDon.Text      = "#---";

            // ── Cụm NHÀ CUNG CẤP ───────────────────────────────────────────
            this.lblNCCCaption.AutoSize  = true;
            this.lblNCCCaption.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNCCCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblNCCCaption.Location  = new System.Drawing.Point(130, 12);
            this.lblNCCCaption.Name      = "lblNCCCaption";
            this.lblNCCCaption.Text      = "NHÀ CUNG CẤP";

            this.lblNCC.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNCC.Location  = new System.Drawing.Point(128, 32);
            this.lblNCC.Name      = "lblNCC";
            this.lblNCC.Size      = new System.Drawing.Size(560, 50);
            this.lblNCC.Text      = "---";

            // ── Cụm SỐ MẶT HÀNG ────────────────────────────────────────────
            this.lblSoMHCaption.AutoSize  = true;
            this.lblSoMHCaption.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSoMHCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblSoMHCaption.Location  = new System.Drawing.Point(710, 12);
            this.lblSoMHCaption.Name      = "lblSoMHCaption";
            this.lblSoMHCaption.Text      = "SỐ MẶT HÀNG";

            this.lblSoMatHang.AutoSize  = true;
            this.lblSoMatHang.Font      = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblSoMatHang.Location  = new System.Drawing.Point(710, 32);
            this.lblSoMatHang.Name      = "lblSoMatHang";
            this.lblSoMatHang.Text      = "--";

            // ================================================================
            // ── Cột dgvChiTiet ───────────────────────────────────────────────
            // ================================================================
            //
            // colSTT — số thứ tự #
            this.colSTT.DataPropertyName                  = "STT";
            this.colSTT.DefaultCellStyle.Alignment        = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSTT.HeaderText                        = "#";
            this.colSTT.Name                              = "colSTT";
            this.colSTT.ReadOnly                          = true;
            this.colSTT.Width                             = 36;

            // colTenMH — Fill + WrapMode để tên dài tự xuống dòng
            this.colTenMH.AutoSizeMode                    = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenMH.DataPropertyName                = "Ten_MatHang";
            this.colTenMH.DefaultCellStyle.WrapMode       = System.Windows.Forms.DataGridViewTriState.True;
            this.colTenMH.HeaderText                      = "Mặt hàng";
            this.colTenMH.Name                            = "colTenMH";
            this.colTenMH.ReadOnly                        = true;

            // colDonVi
            this.colDonVi.DataPropertyName                = "DonViTinh";
            this.colDonVi.DefaultCellStyle.Alignment      = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDonVi.HeaderText                      = "Đơn vị";
            this.colDonVi.Name                            = "colDonVi";
            this.colDonVi.ReadOnly                        = true;
            this.colDonVi.Width                           = 70;

            // colDonGia — format N0, căn phải
            this.colDonGia.DataPropertyName               = "DonGia";
            this.colDonGia.DefaultCellStyle.Alignment     = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDonGia.DefaultCellStyle.Format        = "N0";
            this.colDonGia.HeaderText                     = "Đơn giá";
            this.colDonGia.Name                           = "colDonGia";
            this.colDonGia.ReadOnly                       = true;
            this.colDonGia.Width                          = 130;

            // colSoLuong — căn giữa
            this.colSoLuong.DataPropertyName              = "SoLuong";
            this.colSoLuong.DefaultCellStyle.Alignment    = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSoLuong.HeaderText                    = "SL";
            this.colSoLuong.Name                          = "colSoLuong";
            this.colSoLuong.ReadOnly                      = true;
            this.colSoLuong.Width                         = 48;

            // colThanhTien — format N0, căn phải
            this.colThanhTien.DataPropertyName            = "ThanhTien";
            this.colThanhTien.DefaultCellStyle.Alignment  = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colThanhTien.DefaultCellStyle.Format     = "N0";
            this.colThanhTien.HeaderText                  = "Thành tiền";
            this.colThanhTien.Name                        = "colThanhTien";
            this.colThanhTien.ReadOnly                    = true;
            this.colThanhTien.Width                       = 140;

            // ================================================================
            // dgvChiTiet
            // ================================================================
            this.dgvChiTiet.AllowUserToAddRows    = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.dgvChiTiet.AutoGenerateColumns                   = false;
            this.dgvChiTiet.BackgroundColor                       = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle                           = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle.Font   = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvChiTiet.ColumnHeadersHeightSizeMode           = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSTT, this.colTenMH, this.colDonVi,
                this.colDonGia, this.colSoLuong, this.colThanhTien });
            this.dgvChiTiet.Location                              = new System.Drawing.Point(0, 90);
            this.dgvChiTiet.Name                                  = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly                              = true;
            this.dgvChiTiet.RowHeadersVisible                     = false;
            this.dgvChiTiet.RowTemplate.Height                    = 28;
            this.dgvChiTiet.SelectionMode                         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size                                  = new System.Drawing.Size(900, 340);
            this.dgvChiTiet.TabIndex                              = 0;

            // ================================================================
            // lblTongTien — TỔNG CỘNG đỏ đậm, bottom-right
            // ================================================================
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right));
            this.lblTongTien.AutoSize  = true;
            this.lblTongTien.Font      = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.lblTongTien.Location  = new System.Drawing.Point(560, 452);
            this.lblTongTien.Name      = "lblTongTien";
            this.lblTongTien.Text      = "TỔNG CỘNG:  0 ₫";

            // ================================================================
            // btnDong — xám, bottom-left
            // ================================================================
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left));
            this.btnDong.BackColor        = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnDong.FlatStyle        = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font             = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.ForeColor        = System.Drawing.Color.White;
            this.btnDong.Location         = new System.Drawing.Point(12, 447);
            this.btnDong.Name             = "btnDong";
            this.btnDong.Size             = new System.Drawing.Size(100, 34);
            this.btnDong.TabIndex         = 3;
            this.btnDong.Text             = "✖ Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click           += new System.EventHandler(this.btnDong_Click);

            // ================================================================
            // FormChiTiet
            // ================================================================
            this.AutoScaleDimensions  = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode        = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize           = new System.Drawing.Size(900, 495);
            this.MinimumSize          = new System.Drawing.Size(820, 460);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlInfo);
            this.FormBorderStyle      = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox          = false;
            this.MinimizeBox          = false;
            this.Name                 = "FormChiTiet";
            this.StartPosition        = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text                 = "Chi tiết đơn nhập hàng";
            this.Load                += new System.EventHandler(this.FormChiTiet_Load);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Controls ─────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblMaDonCaption;
        private System.Windows.Forms.Label lblMaDon;
        private System.Windows.Forms.Label lblNCCCaption;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.Label lblSoMHCaption;
        private System.Windows.Forms.Label lblSoMatHang;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenMH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonVi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnDong;
    }
}
