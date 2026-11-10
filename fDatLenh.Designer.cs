namespace đồ_án_tin_học_1
{
    partial class fDatLenh
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
            this.lblMaCK = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblSoDu = new System.Windows.Forms.Label();
            this.rbtnMua = new System.Windows.Forms.RadioButton();
            this.rbtnBan = new System.Windows.Forms.RadioButton();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.cbMaCP = new System.Windows.Forms.ComboBox();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMaCK
            // 
            this.lblMaCK.AutoSize = true;
            this.lblMaCK.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaCK.Location = new System.Drawing.Point(7, 27);
            this.lblMaCK.Name = "lblMaCK";
            this.lblMaCK.Size = new System.Drawing.Size(177, 32);
            this.lblMaCK.TabIndex = 0;
            this.lblMaCK.Text = "Mã Cổ Phiếu";
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGia.Location = new System.Drawing.Point(8, 108);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(56, 29);
            this.lblGia.TabIndex = 1;
            this.lblGia.Text = "Giá ";
            this.lblGia.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(8, 175);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(116, 29);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số Lượng";
            // 
            // lblSoDu
            // 
            this.lblSoDu.AutoSize = true;
            this.lblSoDu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoDu.Location = new System.Drawing.Point(8, 315);
            this.lblSoDu.Name = "lblSoDu";
            this.lblSoDu.Size = new System.Drawing.Size(72, 25);
            this.lblSoDu.TabIndex = 3;
            this.lblSoDu.Text = "Số Dư";
            // 
            // rbtnMua
            // 
            this.rbtnMua.AutoSize = true;
            this.rbtnMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnMua.ForeColor = System.Drawing.Color.DarkGreen;
            this.rbtnMua.Location = new System.Drawing.Point(205, 257);
            this.rbtnMua.Name = "rbtnMua";
            this.rbtnMua.Size = new System.Drawing.Size(83, 33);
            this.rbtnMua.TabIndex = 4;
            this.rbtnMua.TabStop = true;
            this.rbtnMua.Text = "Mua";
            this.rbtnMua.UseVisualStyleBackColor = true;
            // 
            // rbtnBan
            // 
            this.rbtnBan.AutoSize = true;
            this.rbtnBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnBan.ForeColor = System.Drawing.Color.LightSlateGray;
            this.rbtnBan.Location = new System.Drawing.Point(403, 257);
            this.rbtnBan.Name = "rbtnBan";
            this.rbtnBan.Size = new System.Drawing.Size(79, 33);
            this.rbtnBan.TabIndex = 5;
            this.rbtnBan.TabStop = true;
            this.rbtnBan.Text = "Bán";
            this.rbtnBan.UseVisualStyleBackColor = true;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnXacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnXacNhan.Location = new System.Drawing.Point(205, 378);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(288, 60);
            this.btnXacNhan.TabIndex = 6;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // cbMaCP
            // 
            this.cbMaCP.FormattingEnabled = true;
            this.cbMaCP.Items.AddRange(new object[] {
            "FPT",
            "VHM",
            "VIC",
            "MBB"});
            this.cbMaCP.Location = new System.Drawing.Point(242, 37);
            this.cbMaCP.Name = "cbMaCP";
            this.cbMaCP.Size = new System.Drawing.Size(196, 24);
            this.cbMaCP.TabIndex = 7;
            // 
            // txtGia
            // 
            this.txtGia.Location = new System.Drawing.Point(242, 115);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(196, 22);
            this.txtGia.TabIndex = 8;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(242, 182);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(196, 22);
            this.txtSoLuong.TabIndex = 9;
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(18, 378);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(106, 60);
            this.btnQuayLai.TabIndex = 10;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // fDatLenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 487);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.txtGia);
            this.Controls.Add(this.cbMaCP);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.rbtnBan);
            this.Controls.Add(this.rbtnMua);
            this.Controls.Add(this.lblSoDu);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.lblGia);
            this.Controls.Add(this.lblMaCK);
            this.Name = "fDatLenh";
            this.Text = "fMuaBan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaCK;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblSoDu;
        private System.Windows.Forms.RadioButton rbtnMua;
        private System.Windows.Forms.RadioButton rbtnBan;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.ComboBox cbMaCP;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Button btnQuayLai;
    }
}