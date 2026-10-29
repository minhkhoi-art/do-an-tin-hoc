namespace đồ_án_tin_học_1
{
    partial class ThongTinTaiKhoan
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
            this.dgvDanhsachDangGiu = new System.Windows.Forms.DataGridView();
            this.colMa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThayDoiHomNay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThayDoiTuDauNam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaMua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongGiaTri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongLaiLo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblTienMat = new System.Windows.Forms.Label();
            this.lblDangDauTu = new System.Windows.Forms.Label();
            this.lblGiaVon = new System.Windows.Forms.Label();
            this.lblTongTam = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTienMat1 = new System.Windows.Forms.Label();
            this.lblDangDauTu1 = new System.Windows.Forms.Label();
            this.lblGiaVon1 = new System.Windows.Forms.Label();
            this.lblTongTam1 = new System.Windows.Forms.Label();
            this.lblTong1 = new System.Windows.Forms.Label();
            this.lblDangNamGiu = new System.Windows.Forms.Label();
            this.btnQuayLai = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsachDangGiu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDanhsachDangGiu
            // 
            this.dgvDanhsachDangGiu.AllowUserToAddRows = false;
            this.dgvDanhsachDangGiu.AllowUserToDeleteRows = false;
            this.dgvDanhsachDangGiu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhsachDangGiu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDanhsachDangGiu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhsachDangGiu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMa,
            this.colGiaHT,
            this.colThayDoiHomNay,
            this.colThayDoiTuDauNam,
            this.colGiaMua,
            this.colSoLuong,
            this.colTongGiaTri,
            this.colTongLaiLo});
            this.dgvDanhsachDangGiu.Location = new System.Drawing.Point(10, 212);
            this.dgvDanhsachDangGiu.Name = "dgvDanhsachDangGiu";
            this.dgvDanhsachDangGiu.ReadOnly = true;
            this.dgvDanhsachDangGiu.RowHeadersVisible = false;
            this.dgvDanhsachDangGiu.RowHeadersWidth = 51;
            this.dgvDanhsachDangGiu.RowTemplate.Height = 24;
            this.dgvDanhsachDangGiu.Size = new System.Drawing.Size(680, 233);
            this.dgvDanhsachDangGiu.TabIndex = 0;
            // 
            // colMa
            // 
            this.colMa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMa.HeaderText = "Mã";
            this.colMa.MinimumWidth = 6;
            this.colMa.Name = "colMa";
            this.colMa.ReadOnly = true;
            // 
            // colGiaHT
            // 
            this.colGiaHT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGiaHT.HeaderText = "Giá Hiện Tại";
            this.colGiaHT.MinimumWidth = 6;
            this.colGiaHT.Name = "colGiaHT";
            this.colGiaHT.ReadOnly = true;
            // 
            // colThayDoiHomNay
            // 
            this.colThayDoiHomNay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colThayDoiHomNay.HeaderText = "Thay Đổi Hôm Nay";
            this.colThayDoiHomNay.MinimumWidth = 6;
            this.colThayDoiHomNay.Name = "colThayDoiHomNay";
            this.colThayDoiHomNay.ReadOnly = true;
            // 
            // colThayDoiTuDauNam
            // 
            this.colThayDoiTuDauNam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colThayDoiTuDauNam.HeaderText = "Thay Đổi Từ Đầu Năm";
            this.colThayDoiTuDauNam.MinimumWidth = 6;
            this.colThayDoiTuDauNam.Name = "colThayDoiTuDauNam";
            this.colThayDoiTuDauNam.ReadOnly = true;
            // 
            // colGiaMua
            // 
            this.colGiaMua.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGiaMua.HeaderText = "Giá Mua";
            this.colGiaMua.MinimumWidth = 6;
            this.colGiaMua.Name = "colGiaMua";
            this.colGiaMua.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSoLuong.HeaderText = "Số Lượng";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // colTongGiaTri
            // 
            this.colTongGiaTri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongGiaTri.HeaderText = "Tổng Giá Trị";
            this.colTongGiaTri.MinimumWidth = 6;
            this.colTongGiaTri.Name = "colTongGiaTri";
            this.colTongGiaTri.ReadOnly = true;
            // 
            // colTongLaiLo
            // 
            this.colTongLaiLo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongLaiLo.HeaderText = "Tổng Lãi / Lỗ";
            this.colTongLaiLo.MinimumWidth = 6;
            this.colTongLaiLo.Name = "colTongLaiLo";
            this.colTongLaiLo.ReadOnly = true;
            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.AutoSize = true;
            this.lblTongGiaTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongGiaTri.Location = new System.Drawing.Point(19, 18);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(73, 20);
            this.lblTongGiaTri.TabIndex = 1;
            this.lblTongGiaTri.Text = "Giá Trị ";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.Location = new System.Drawing.Point(19, 45);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(119, 20);
            this.lblTongTien.TabIndex = 2;
            this.lblTongTien.Text = "$100.000.000";
            // 
            // lblTienMat
            // 
            this.lblTienMat.AutoSize = true;
            this.lblTienMat.Location = new System.Drawing.Point(20, 97);
            this.lblTienMat.Name = "lblTienMat";
            this.lblTienMat.Size = new System.Drawing.Size(59, 17);
            this.lblTienMat.TabIndex = 3;
            this.lblTienMat.Text = "Tiền Mặt";
            // 
            // lblDangDauTu
            // 
            this.lblDangDauTu.AutoSize = true;
            this.lblDangDauTu.Location = new System.Drawing.Point(165, 97);
            this.lblDangDauTu.Name = "lblDangDauTu";
            this.lblDangDauTu.Size = new System.Drawing.Size(85, 17);
            this.lblDangDauTu.TabIndex = 4;
            this.lblDangDauTu.Text = "Đang Đầu Tư";
            // 
            // lblGiaVon
            // 
            this.lblGiaVon.AutoSize = true;
            this.lblGiaVon.Location = new System.Drawing.Point(304, 97);
            this.lblGiaVon.Name = "lblGiaVon";
            this.lblGiaVon.Size = new System.Drawing.Size(54, 17);
            this.lblGiaVon.TabIndex = 5;
            this.lblGiaVon.Text = "Giá Vốn";
            // 
            // lblTongTam
            // 
            this.lblTongTam.AutoSize = true;
            this.lblTongTam.Location = new System.Drawing.Point(432, 97);
            this.lblTongTam.Name = "lblTongTam";
            this.lblTongTam.Size = new System.Drawing.Size(118, 17);
            this.lblTongTam.TabIndex = 6;
            this.lblTongTam.Text = "Tổng Tạm Lãi / Lỗ ";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(583, 97);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(85, 17);
            this.lblTong.TabIndex = 7;
            this.lblTong.Text = "Tổng Lãi / Lỗ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(22, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(88, 1);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(168, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(88, 1);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(307, 83);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(88, 1);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.Location = new System.Drawing.Point(435, 83);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(114, 1);
            this.panel4.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.Location = new System.Drawing.Point(585, 83);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(88, 1);
            this.panel5.TabIndex = 12;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel6.Location = new System.Drawing.Point(22, 14);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1488, 1);
            this.panel6.TabIndex = 9;
            // 
            // lblTienMat1
            // 
            this.lblTienMat1.AutoSize = true;
            this.lblTienMat1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienMat1.Location = new System.Drawing.Point(20, 126);
            this.lblTienMat1.Name = "lblTienMat1";
            this.lblTienMat1.Size = new System.Drawing.Size(119, 20);
            this.lblTienMat1.TabIndex = 13;
            this.lblTienMat1.Text = "$100.000.000";
            // 
            // lblDangDauTu1
            // 
            this.lblDangDauTu1.AutoSize = true;
            this.lblDangDauTu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangDauTu1.Location = new System.Drawing.Point(165, 126);
            this.lblDangDauTu1.Name = "lblDangDauTu1";
            this.lblDangDauTu1.Size = new System.Drawing.Size(54, 20);
            this.lblDangDauTu1.TabIndex = 14;
            this.lblDangDauTu1.Text = "$0.00";
            // 
            // lblGiaVon1
            // 
            this.lblGiaVon1.AutoSize = true;
            this.lblGiaVon1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaVon1.Location = new System.Drawing.Point(304, 126);
            this.lblGiaVon1.Name = "lblGiaVon1";
            this.lblGiaVon1.Size = new System.Drawing.Size(54, 20);
            this.lblGiaVon1.TabIndex = 15;
            this.lblGiaVon1.Text = "$0.00";
            // 
            // lblTongTam1
            // 
            this.lblTongTam1.AutoSize = true;
            this.lblTongTam1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTam1.Location = new System.Drawing.Point(432, 126);
            this.lblTongTam1.Name = "lblTongTam1";
            this.lblTongTam1.Size = new System.Drawing.Size(54, 20);
            this.lblTongTam1.TabIndex = 16;
            this.lblTongTam1.Text = "$0.00";
            // 
            // lblTong1
            // 
            this.lblTong1.AutoSize = true;
            this.lblTong1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong1.Location = new System.Drawing.Point(583, 126);
            this.lblTong1.Name = "lblTong1";
            this.lblTong1.Size = new System.Drawing.Size(54, 20);
            this.lblTong1.TabIndex = 17;
            this.lblTong1.Text = "$0.00";
            // 
            // lblDangNamGiu
            // 
            this.lblDangNamGiu.AutoSize = true;
            this.lblDangNamGiu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangNamGiu.Location = new System.Drawing.Point(12, 189);
            this.lblDangNamGiu.Name = "lblDangNamGiu";
            this.lblDangNamGiu.Size = new System.Drawing.Size(131, 20);
            this.lblDangNamGiu.TabIndex = 18;
            this.lblDangNamGiu.Text = "Đang Nắm Giữ";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(555, 177);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(118, 29);
            this.btnQuayLai.TabIndex = 19;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // ThongTinTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 478);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.lblDangNamGiu);
            this.Controls.Add(this.lblTong1);
            this.Controls.Add(this.lblTongTam1);
            this.Controls.Add(this.lblGiaVon1);
            this.Controls.Add(this.lblDangDauTu1);
            this.Controls.Add(this.lblTienMat1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTong);
            this.Controls.Add(this.lblTongTam);
            this.Controls.Add(this.lblGiaVon);
            this.Controls.Add(this.lblDangDauTu);
            this.Controls.Add(this.lblTienMat);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblTongGiaTri);
            this.Controls.Add(this.dgvDanhsachDangGiu);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ThongTinTaiKhoan";
            this.Text = "ThongTinTaiKhoan";
            this.Load += new System.EventHandler(this.ThongTinTaiKhoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsachDangGiu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhsachDangGiu;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblTienMat;
        private System.Windows.Forms.Label lblDangDauTu;
        private System.Windows.Forms.Label lblGiaVon;
        private System.Windows.Forms.Label lblTongTam;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTienMat1;
        private System.Windows.Forms.Label lblDangDauTu1;
        private System.Windows.Forms.Label lblGiaVon1;
        private System.Windows.Forms.Label lblTongTam1;
        private System.Windows.Forms.Label lblTong1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThayDoiHomNay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThayDoiTuDauNam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaMua;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongGiaTri;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongLaiLo;
        private System.Windows.Forms.Label lblDangNamGiu;
        private System.Windows.Forms.Button btnQuayLai;
    }
}