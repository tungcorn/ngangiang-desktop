namespace ngangiang_desktop
{
    partial class FormDanhSach
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvNCC = new System.Windows.Forms.DataGridView();
            this.dgvDonNhap = new System.Windows.Forms.DataGridView();
            this.btnTaoDon = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblNCC = new System.Windows.Forms.Label();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.lblTongSoDon = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonNhap)).BeginInit();
            this.SuspendLayout();

            // ─── lblTitle ───────────────────────────────────────────────────
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Đơn nhập hàng";

            // ─── btnTaoDon ──────────────────────────────────────────────────
            this.btnTaoDon.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right));
            this.btnTaoDon.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.btnTaoDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoDon.ForeColor = System.Drawing.Color.White;
            this.btnTaoDon.Location = new System.Drawing.Point(870, 9);
            this.btnTaoDon.Name = "btnTaoDon";
            this.btnTaoDon.Size = new System.Drawing.Size(120, 32);
            this.btnTaoDon.TabIndex = 1;
            this.btnTaoDon.Text = "➕ Tạo đơn";
            this.btnTaoDon.UseVisualStyleBackColor = false;
            this.btnTaoDon.Click += new System.EventHandler(this.btnTaoDon_Click);

            // ─── btnLamMoi ──────────────────────────────────────────────────
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(740, 9);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 32);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // ─── lblNCC ─────────────────────────────────────────────────────
            this.lblNCC.AutoSize = true;
            this.lblNCC.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNCC.Location = new System.Drawing.Point(13, 50);
            this.lblNCC.Name = "lblNCC";
            this.lblNCC.TabIndex = 6;
            this.lblNCC.Text = "📋 Nhà cung cấp (NCC)";

            // ─── dgvNCC ─────────────────────────────────────────────────────
            this.dgvNCC.AllowUserToAddRows = false;
            this.dgvNCC.AllowUserToDeleteRows = false;
            this.dgvNCC.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.dgvNCC.BackgroundColor = System.Drawing.Color.White;
            this.dgvNCC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNCC.Location = new System.Drawing.Point(17, 73);
            this.dgvNCC.MultiSelect = true;
            this.dgvNCC.Name = "dgvNCC";
            this.dgvNCC.RowHeadersVisible = false;
            this.dgvNCC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNCC.Size = new System.Drawing.Size(980, 130);
            this.dgvNCC.TabIndex = 8;

            // ─── lblDanhSach + lblTongSoDon ─────────────────────────────────
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSach.Location = new System.Drawing.Point(13, 212);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.TabIndex = 9;
            this.lblDanhSach.Text = "📦 Đơn nhập hàng";

            this.lblTongSoDon.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right));
            this.lblTongSoDon.AutoSize = true;
            this.lblTongSoDon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblTongSoDon.ForeColor = System.Drawing.Color.Gray;
            this.lblTongSoDon.Location = new System.Drawing.Point(870, 215);
            this.lblTongSoDon.Name = "lblTongSoDon";
            this.lblTongSoDon.TabIndex = 12;
            this.lblTongSoDon.Text = "Tổng: 0 đơn";

            // ─── dgvDonNhap ─────────────────────────────────────────────────
            this.dgvDonNhap.AllowUserToAddRows = false;
            this.dgvDonNhap.AllowUserToDeleteRows = false;
            this.dgvDonNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.dgvDonNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonNhap.Location = new System.Drawing.Point(17, 235);
            this.dgvDonNhap.MultiSelect = false;
            this.dgvDonNhap.Name = "dgvDonNhap";
            this.dgvDonNhap.ReadOnly = false;
            this.dgvDonNhap.RowHeadersVisible = false;
            this.dgvDonNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonNhap.Size = new System.Drawing.Size(980, 330);
            this.dgvDonNhap.TabIndex = 10;

            // ─── lblGrandTotal: Tổng cộng đỏ đậm, bottom-right ─────────────
            this.lblGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right));
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.lblGrandTotal.Location = new System.Drawing.Point(700, 580);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.TabIndex = 13;
            this.lblGrandTotal.Text = "Tổng cộng: 0 ₫";

            // ─── FormDanhSach ────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 608);
            this.MinimumSize = new System.Drawing.Size(1026, 640);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.lblTongSoDon);
            this.Controls.Add(this.dgvDonNhap);
            this.Controls.Add(this.lblDanhSach);
            this.Controls.Add(this.dgvNCC);
            this.Controls.Add(this.lblNCC);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnTaoDon);
            this.Controls.Add(this.lblTitle);
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

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvNCC;
        private System.Windows.Forms.DataGridView dgvDonNhap;
        private System.Windows.Forms.Button btnTaoDon;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.Label lblTongSoDon;
        private System.Windows.Forms.Label lblGrandTotal;
    }
}
