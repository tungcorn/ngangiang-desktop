namespace ngangiang_desktop
{
    partial class FormDanhSach
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTaoDon = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblNCC = new System.Windows.Forms.Label();
            this.dgvNCC = new System.Windows.Forms.DataGridView();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.lblTongSoDon = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.dgvDonNhap = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblTitle.Location = new System.Drawing.Point(9, 7);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(257, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Đơn nhập hàng";
            // 
            // btnTaoDon
            // 
            this.btnTaoDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaoDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnTaoDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoDon.ForeColor = System.Drawing.Color.White;
            this.btnTaoDon.Location = new System.Drawing.Point(630, 6);
            this.btnTaoDon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTaoDon.Name = "btnTaoDon";
            this.btnTaoDon.Size = new System.Drawing.Size(112, 31);
            this.btnTaoDon.TabIndex = 1;
            this.btnTaoDon.Text = "➕ Tạo đơn";
            this.btnTaoDon.UseVisualStyleBackColor = false;
            this.btnTaoDon.Click += new System.EventHandler(this.btnTaoDon_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(508, 6);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(112, 31);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblNCC
            // 
            this.lblNCC.AutoSize = true;
            this.lblNCC.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNCC.Location = new System.Drawing.Point(10, 41);
            this.lblNCC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNCC.Name = "lblNCC";
            this.lblNCC.Size = new System.Drawing.Size(176, 20);
            this.lblNCC.TabIndex = 3;
            this.lblNCC.Text = "📋 Nhà cung cấp (NCC)";
            // 
            // dgvNCC
            // 
            this.dgvNCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNCC.BackgroundColor = System.Drawing.Color.White;
            this.dgvNCC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNCC.Location = new System.Drawing.Point(13, 59);
            this.dgvNCC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvNCC.Name = "dgvNCC";
            this.dgvNCC.RowHeadersVisible = false;
            this.dgvNCC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNCC.Size = new System.Drawing.Size(735, 106);
            this.dgvNCC.TabIndex = 4;
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSach.Location = new System.Drawing.Point(10, 172);
            this.lblDanhSach.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(142, 20);
            this.lblDanhSach.TabIndex = 5;
            this.lblDanhSach.Text = "📦 Đơn nhập hàng";
            // 
            // lblTongSoDon
            // 
            this.lblTongSoDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSoDon.AutoSize = true;
            this.lblTongSoDon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblTongSoDon.ForeColor = System.Drawing.Color.Gray;
            this.lblTongSoDon.Location = new System.Drawing.Point(615, 175);
            this.lblTongSoDon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongSoDon.Name = "lblTongSoDon";
            this.lblTongSoDon.Size = new System.Drawing.Size(75, 17);
            this.lblTongSoDon.TabIndex = 6;
            this.lblTongSoDon.Text = "Tổng: 0 đơn";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblGrandTotal.Location = new System.Drawing.Point(525, 471);
            this.lblGrandTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(114, 20);
            this.lblGrandTotal.TabIndex = 7;
            this.lblGrandTotal.Text = "Tổng cộng: 0 ₫";
            // 
            // dgvDonNhap
            // 
            this.dgvDonNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDonNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonNhap.Location = new System.Drawing.Point(13, 191);
            this.dgvDonNhap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvDonNhap.MultiSelect = false;
            this.dgvDonNhap.Name = "dgvDonNhap";
            this.dgvDonNhap.RowHeadersVisible = false;
            this.dgvDonNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonNhap.Size = new System.Drawing.Size(735, 268);
            this.dgvDonNhap.TabIndex = 8;
            // 
            // FormDanhSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 494);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnTaoDon);
            this.Controls.Add(this.lblNCC);
            this.Controls.Add(this.dgvNCC);
            this.Controls.Add(this.lblDanhSach);
            this.Controls.Add(this.lblTongSoDon);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.dgvDonNhap);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(774, 527);
            this.Name = "FormDanhSach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Đơn nhập hàng - Ngân Giang";
            this.Load += new System.EventHandler(this.FormDanhSach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label     lblTitle;
        private System.Windows.Forms.Button    btnTaoDon;
        private System.Windows.Forms.Button    btnLamMoi;
        private System.Windows.Forms.Label     lblNCC;
        private System.Windows.Forms.DataGridView dgvNCC;
        private System.Windows.Forms.Label     lblDanhSach;
        private System.Windows.Forms.Label     lblTongSoDon;
        private System.Windows.Forms.Label     lblGrandTotal;
        private System.Windows.Forms.DataGridView dgvDonNhap;
    }
}
