namespace ngangiang_desktop
{
    partial class FormChiTiet
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblMaDonCaption = new System.Windows.Forms.Label();
            this.lblMaDon = new System.Windows.Forms.Label();
            this.lblNCCCaption = new System.Windows.Forms.Label();
            this.lblNCC = new System.Windows.Forms.Label();
            this.lblSoMHCaption = new System.Windows.Forms.Label();
            this.lblSoMatHang = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();

            // ─── pnlInfo: nền xám nhạt, 3 cột info ngang ───────────────────
            this.pnlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfo.Controls.Add(this.lblMaDonCaption);
            this.pnlInfo.Controls.Add(this.lblMaDon);
            this.pnlInfo.Controls.Add(this.lblNCCCaption);
            this.pnlInfo.Controls.Add(this.lblNCC);
            this.pnlInfo.Controls.Add(this.lblSoMHCaption);
            this.pnlInfo.Controls.Add(this.lblSoMatHang);
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(900, 90);
            this.pnlInfo.TabIndex = 10;

            // ─── Cụm MÃ ĐƠN ────────────────────────────────────────────────
            this.lblMaDonCaption.AutoSize = true;
            this.lblMaDonCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMaDonCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblMaDonCaption.Location = new System.Drawing.Point(16, 12);
            this.lblMaDonCaption.Text = "MÃ ĐƠN";

            this.lblMaDon.AutoSize = true;
            this.lblMaDon.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblMaDon.ForeColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.lblMaDon.Location = new System.Drawing.Point(14, 32);
            this.lblMaDon.Text = "#---";

            // ─── Cụm NHÀ CUNG CẤP ──────────────────────────────────────────
            this.lblNCCCaption.AutoSize = true;
            this.lblNCCCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNCCCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblNCCCaption.Location = new System.Drawing.Point(130, 12);
            this.lblNCCCaption.Text = "NHÀ CUNG CẤP";

            this.lblNCC.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNCC.Location = new System.Drawing.Point(128, 32);
            this.lblNCC.Size = new System.Drawing.Size(560, 50);
            this.lblNCC.Text = "---";

            // ─── Cụm SỐ MẶT HÀNG ───────────────────────────────────────────
            this.lblSoMHCaption.AutoSize = true;
            this.lblSoMHCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSoMHCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblSoMHCaption.Location = new System.Drawing.Point(710, 12);
            this.lblSoMHCaption.Text = "SỐ MẶT HÀNG";

            this.lblSoMatHang.AutoSize = true;
            this.lblSoMatHang.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblSoMatHang.Location = new System.Drawing.Point(710, 32);
            this.lblSoMatHang.Text = "--";

            // ─── dgvChiTiet ─────────────────────────────────────────────────
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 90);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(900, 340);
            this.dgvChiTiet.TabIndex = 0;

            // ─── lblTongTien: TỔNG CỘNG đỏ đậm, bottom-right ───────────────
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.lblTongTien.Location = new System.Drawing.Point(560, 452);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Text = "TỔNG CỘNG:  0 ₫";

            // ─── btnDong: xám, bottom-left ──────────────────────────────────
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(12, 447);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 34);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "✖ Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            // ─── FormChiTiet ────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 495);
            this.MinimumSize = new System.Drawing.Size(820, 460);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChiTiet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết đơn nhập hàng";
            this.Load += new System.EventHandler(this.FormChiTiet_Load);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblMaDonCaption;
        private System.Windows.Forms.Label lblMaDon;
        private System.Windows.Forms.Label lblNCCCaption;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.Label lblSoMHCaption;
        private System.Windows.Forms.Label lblSoMatHang;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnDong;
    }
}
