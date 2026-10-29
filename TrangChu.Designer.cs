namespace đồ_án_tin_học_1
{
    partial class TrangChu
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
            this.btnMuaBan = new System.Windows.Forms.Button();
            this.btnDS = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnTTTK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMuaBan
            // 
            this.btnMuaBan.Location = new System.Drawing.Point(69, 77);
            this.btnMuaBan.Name = "btnMuaBan";
            this.btnMuaBan.Size = new System.Drawing.Size(115, 50);
            this.btnMuaBan.TabIndex = 0;
            this.btnMuaBan.Text = "Mua/Bán";
            this.btnMuaBan.UseVisualStyleBackColor = true;
            // 
            // btnDS
            // 
            this.btnDS.Location = new System.Drawing.Point(259, 77);
            this.btnDS.Name = "btnDS";
            this.btnDS.Size = new System.Drawing.Size(115, 50);
            this.btnDS.TabIndex = 1;
            this.btnDS.Text = "Danh sách cổ phiếu";
            this.btnDS.UseVisualStyleBackColor = true;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(259, 213);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(115, 50);
            this.btnDangXuat.TabIndex = 2;
            this.btnDangXuat.Text = "Đăng Xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnTTTK
            // 
            this.btnTTTK.Location = new System.Drawing.Point(69, 213);
            this.btnTTTK.Name = "btnTTTK";
            this.btnTTTK.Size = new System.Drawing.Size(115, 50);
            this.btnTTTK.TabIndex = 3;
            this.btnTTTK.Text = "Thông Tin Tài Khoản";
            this.btnTTTK.UseVisualStyleBackColor = true;
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 342);
            this.Controls.Add(this.btnTTTK);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.btnDS);
            this.Controls.Add(this.btnMuaBan);
            this.Name = "TrangChu";
            this.Text = "TrangChu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMuaBan;
        private System.Windows.Forms.Button btnDS;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnTTTK;
    }
}