namespace đồ_án_tin_học_1
{
    partial class dsCoPhieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDanhsach = new System.Windows.Forms.DataGridView();
            this.colMa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaGN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVonHoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhoiLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBienDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTenCoPhieu = new System.Windows.Forms.Label();
            this.txtTenCoPhieu = new System.Windows.Forms.TextBox();
            this.btnMuaBan = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.AllowUserToAddRows = false;
            this.dgvDanhsach.AllowUserToDeleteRows = false;
            this.dgvDanhsach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhsach.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhsach.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhsach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMa,
            this.colGiaGN,
            this.colGiaNgay,
            this.colGiaNam,
            this.colVonHoa,
            this.colKhoiLuong,
            this.colBienDo});
            this.dgvDanhsach.Cursor = System.Windows.Forms.Cursors.IBeam;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhsach.Location = new System.Drawing.Point(9, 78);
            this.dgvDanhsach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.ReadOnly = true;
            this.dgvDanhsach.RowHeadersVisible = false;
            this.dgvDanhsach.RowHeadersWidth = 51;
            this.dgvDanhsach.RowTemplate.Height = 24;
            this.dgvDanhsach.Size = new System.Drawing.Size(781, 381);
            this.dgvDanhsach.TabIndex = 0;
            // 
            // colMa
            // 
            this.colMa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMa.HeaderText = "Mã";
            this.colMa.MinimumWidth = 6;
            this.colMa.Name = "colMa";
            this.colMa.ReadOnly = true;
            // 
            // colGiaGN
            // 
            this.colGiaGN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGiaGN.HeaderText = "Giá Gần Nhất";
            this.colGiaGN.MinimumWidth = 6;
            this.colGiaGN.Name = "colGiaGN";
            this.colGiaGN.ReadOnly = true;
            // 
            // colGiaNgay
            // 
            this.colGiaNgay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGiaNgay.HeaderText = "Thay Đổi Hôm Nay";
            this.colGiaNgay.MinimumWidth = 6;
            this.colGiaNgay.Name = "colGiaNgay";
            this.colGiaNgay.ReadOnly = true;
            // 
            // colGiaNam
            // 
            this.colGiaNam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGiaNam.HeaderText = "Thay Đổi Từ Đầu Năm";
            this.colGiaNam.MinimumWidth = 6;
            this.colGiaNam.Name = "colGiaNam";
            this.colGiaNam.ReadOnly = true;
            // 
            // colVonHoa
            // 
            this.colVonHoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colVonHoa.HeaderText = "Vốn Hoá";
            this.colVonHoa.MinimumWidth = 6;
            this.colVonHoa.Name = "colVonHoa";
            this.colVonHoa.ReadOnly = true;
            // 
            // colKhoiLuong
            // 
            this.colKhoiLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKhoiLuong.HeaderText = "Khối Lượng";
            this.colKhoiLuong.MinimumWidth = 6;
            this.colKhoiLuong.Name = "colKhoiLuong";
            this.colKhoiLuong.ReadOnly = true;
            // 
            // colBienDo
            // 
            this.colBienDo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBienDo.HeaderText = "Biên Độ Năm";
            this.colBienDo.MinimumWidth = 6;
            this.colBienDo.Name = "colBienDo";
            this.colBienDo.ReadOnly = true;
            // 
            // lblTenCoPhieu
            // 
            this.lblTenCoPhieu.AutoSize = true;
            this.lblTenCoPhieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenCoPhieu.Location = new System.Drawing.Point(32, 38);
            this.lblTenCoPhieu.Name = "lblTenCoPhieu";
            this.lblTenCoPhieu.Size = new System.Drawing.Size(94, 20);
            this.lblTenCoPhieu.TabIndex = 1;
            this.lblTenCoPhieu.Text = "Tên Cổ Phiếu";
            // 
            // txtTenCoPhieu
            // 
            this.txtTenCoPhieu.Location = new System.Drawing.Point(132, 34);
            this.txtTenCoPhieu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenCoPhieu.Name = "txtTenCoPhieu";
            this.txtTenCoPhieu.Size = new System.Drawing.Size(217, 25);
            this.txtTenCoPhieu.TabIndex = 2;
            this.txtTenCoPhieu.TextChanged += new System.EventHandler(this.txtTenCoPhieu_TextChanged);
            // 
            // btnMuaBan
            // 
            this.btnMuaBan.Location = new System.Drawing.Point(564, 25);
            this.btnMuaBan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMuaBan.Name = "btnMuaBan";
            this.btnMuaBan.Size = new System.Drawing.Size(107, 40);
            this.btnMuaBan.TabIndex = 3;
            this.btnMuaBan.Text = "Mua/Bán";
            this.btnMuaBan.UseVisualStyleBackColor = true;
            this.btnMuaBan.Click += new System.EventHandler(this.btnMua_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(676, 26);
            this.btnQuayLai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(107, 40);
            this.btnQuayLai.TabIndex = 5;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(451, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "Tìm Kiếm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dsCoPhieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 478);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnMuaBan);
            this.Controls.Add(this.txtTenCoPhieu);
            this.Controls.Add(this.lblTenCoPhieu);
            this.Controls.Add(this.dgvDanhsach);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "dsCoPhieu";
            this.Text = "dsCoPhieu";
            this.Load += new System.EventHandler(this.dsCoPhieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhsach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaGN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVonHoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhoiLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBienDo;
        private System.Windows.Forms.Label lblTenCoPhieu;
        private System.Windows.Forms.TextBox txtTenCoPhieu;
        private System.Windows.Forms.Button btnMuaBan;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Button button1;
    }
}