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
            this.lblTieude = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMaCK
            // 
            this.lblMaCK.AutoSize = true;
            this.lblMaCK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaCK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblMaCK.Location = new System.Drawing.Point(14, 54);
            this.lblMaCK.Name = "lblMaCK";
            this.lblMaCK.Size = new System.Drawing.Size(105, 20);
            this.lblMaCK.TabIndex = 0;
            this.lblMaCK.Text = "Mã Cổ Phiếu";
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblGia.Location = new System.Drawing.Point(14, 124);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(40, 20);
            this.lblGia.TabIndex = 1;
            this.lblGia.Text = "Giá ";
            this.lblGia.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSoLuong.Location = new System.Drawing.Point(14, 194);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(80, 20);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số Lượng";
            // 
            // lblSoDu
            // 
            this.lblSoDu.AutoSize = true;
            this.lblSoDu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoDu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblSoDu.Location = new System.Drawing.Point(14, 329);
            this.lblSoDu.Name = "lblSoDu";
            this.lblSoDu.Size = new System.Drawing.Size(61, 20);
            this.lblSoDu.TabIndex = 3;
            this.lblSoDu.Text = "Số Dư";
            // 
            // rbtnMua
            // 
            this.rbtnMua.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnMua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.rbtnMua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(74)))), ((int)(((byte)(107)))));
            this.rbtnMua.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.rbtnMua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnMua.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnMua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(247)))), ((int)(((byte)(208)))));
            this.rbtnMua.Location = new System.Drawing.Point(14, 269);
            this.rbtnMua.Name = "rbtnMua";
            this.rbtnMua.Size = new System.Drawing.Size(160, 40);
            this.rbtnMua.TabIndex = 4;
            this.rbtnMua.TabStop = true;
            this.rbtnMua.Text = "Mua";
            this.rbtnMua.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnMua.UseVisualStyleBackColor = false;
            // 
            // rbtnBan
            // 
            this.rbtnBan.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnBan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.rbtnBan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(74)))), ((int)(((byte)(107)))));
            this.rbtnBan.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.rbtnBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnBan.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.rbtnBan.Location = new System.Drawing.Point(184, 269);
            this.rbtnBan.Name = "rbtnBan";
            this.rbtnBan.Size = new System.Drawing.Size(160, 40);
            this.rbtnBan.TabIndex = 5;
            this.rbtnBan.TabStop = true;
            this.rbtnBan.Text = "Bán";
            this.rbtnBan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnBan.UseVisualStyleBackColor = false;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(14, 379);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(330, 50);
            this.btnXacNhan.TabIndex = 6;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // cbMaCP
            // 
            this.cbMaCP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.cbMaCP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaCP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMaCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMaCP.ForeColor = System.Drawing.Color.White;
            this.cbMaCP.FormattingEnabled = true;
            this.cbMaCP.Items.AddRange(new object[] {
            "FPT",
            "VHM",
            "VIC",
            "MBB"});
            this.cbMaCP.Location = new System.Drawing.Point(14, 79);
            this.cbMaCP.Name = "cbMaCP";
            this.cbMaCP.Size = new System.Drawing.Size(330, 30);
            this.cbMaCP.TabIndex = 2;
            this.cbMaCP.DropDownClosed += new System.EventHandler(this.cbMaCP_DropDownClosed);
            // 
            // txtGia
            // 
            this.txtGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.txtGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGia.ForeColor = System.Drawing.Color.White;
            this.txtGia.Location = new System.Drawing.Point(14, 149);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(330, 28);
            this.txtGia.TabIndex = 1;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.txtSoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoLuong.ForeColor = System.Drawing.Color.White;
            this.txtSoLuong.Location = new System.Drawing.Point(14, 219);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(330, 28);
            this.txtSoLuong.TabIndex = 0;
            // 
            // lblTieude
            // 
            this.lblTieude.AutoSize = true;
            this.lblTieude.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieude.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblTieude.Location = new System.Drawing.Point(14, 4);
            this.lblTieude.Name = "lblTieude";
            this.lblTieude.Size = new System.Drawing.Size(129, 38);
            this.lblTieude.TabIndex = 11;
            this.lblTieude.Text = "Đặt lệnh";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.panel1.Controls.Add(this.lblTieude);
            this.panel1.Controls.Add(this.txtSoLuong);
            this.panel1.Controls.Add(this.txtGia);
            this.panel1.Controls.Add(this.cbMaCP);
            this.panel1.Controls.Add(this.btnXacNhan);
            this.panel1.Controls.Add(this.rbtnBan);
            this.panel1.Controls.Add(this.rbtnMua);
            this.panel1.Controls.Add(this.lblSoDu);
            this.panel1.Controls.Add(this.lblSoLuong);
            this.panel1.Controls.Add(this.lblGia);
            this.panel1.Controls.Add(this.lblMaCK);
            this.panel1.Location = new System.Drawing.Point(310, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 470);
            this.panel1.TabIndex = 12;
            // 
            // fDatLenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(998, 692);
            this.Controls.Add(this.panel1);
            this.Name = "fDatLenh";
            this.Text = "fMuaBan";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label lblTieude;
        private System.Windows.Forms.Panel panel1;
    }
}