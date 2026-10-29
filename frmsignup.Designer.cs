namespace đồ_án_tin_học_1
{
    partial class frmsignup
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
            this.lblTTK = new System.Windows.Forms.Label();
            this.lblMK = new System.Windows.Forms.Label();
            this.txtTenTk = new System.Windows.Forms.TextBox();
            this.txtMk = new System.Windows.Forms.TextBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.ckBHienMk = new System.Windows.Forms.CheckBox();
            this.btnTaoTK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTTK
            // 
            this.lblTTK.AutoSize = true;
            this.lblTTK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTTK.Location = new System.Drawing.Point(35, 100);
            this.lblTTK.Name = "lblTTK";
            this.lblTTK.Size = new System.Drawing.Size(117, 20);
            this.lblTTK.TabIndex = 0;
            this.lblTTK.Text = "Tên Tài Khoản";
            // 
            // lblMK
            // 
            this.lblMK.AutoSize = true;
            this.lblMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMK.Location = new System.Drawing.Point(72, 181);
            this.lblMK.Name = "lblMK";
            this.lblMK.Size = new System.Drawing.Size(80, 20);
            this.lblMK.TabIndex = 1;
            this.lblMK.Text = "Mật Khẩu";
            // 
            // txtTenTk
            // 
            this.txtTenTk.Location = new System.Drawing.Point(168, 100);
            this.txtTenTk.Name = "txtTenTk";
            this.txtTenTk.Size = new System.Drawing.Size(156, 22);
            this.txtTenTk.TabIndex = 2;
            // 
            // txtMk
            // 
            this.txtMk.Location = new System.Drawing.Point(168, 181);
            this.txtMk.Name = "txtMk";
            this.txtMk.Size = new System.Drawing.Size(156, 22);
            this.txtMk.TabIndex = 3;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Red;
            this.lblTieuDe.Location = new System.Drawing.Point(139, 34);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(156, 25);
            this.lblTieuDe.TabIndex = 4;
            this.lblTieuDe.Text = "Tạo Tài Khoản";
            // 
            // ckBHienMk
            // 
            this.ckBHienMk.AutoSize = true;
            this.ckBHienMk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckBHienMk.Location = new System.Drawing.Point(168, 238);
            this.ckBHienMk.Name = "ckBHienMk";
            this.ckBHienMk.Size = new System.Drawing.Size(115, 20);
            this.ckBHienMk.TabIndex = 5;
            this.ckBHienMk.Text = "Hiện Mật Khẩu";
            this.ckBHienMk.UseVisualStyleBackColor = true;
            this.ckBHienMk.CheckedChanged += new System.EventHandler(this.ckBHienMk_CheckedChanged);
            // 
            // btnTaoTK
            // 
            this.btnTaoTK.Location = new System.Drawing.Point(146, 328);
            this.btnTaoTK.Name = "btnTaoTK";
            this.btnTaoTK.Size = new System.Drawing.Size(149, 38);
            this.btnTaoTK.TabIndex = 6;
            this.btnTaoTK.Text = "Tạo Tài Khoản";
            this.btnTaoTK.UseVisualStyleBackColor = true;
            this.btnTaoTK.Click += new System.EventHandler(this.btnTaoTK_Click);
            // 
            // frmsignup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.btnTaoTK);
            this.Controls.Add(this.ckBHienMk);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.txtMk);
            this.Controls.Add(this.txtTenTk);
            this.Controls.Add(this.lblMK);
            this.Controls.Add(this.lblTTK);
            this.Name = "frmsignup";
            this.Text = "frmsignup";
            this.Load += new System.EventHandler(this.frmsignup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTTK;
        private System.Windows.Forms.Label lblMK;
        private System.Windows.Forms.TextBox txtTenTk;
        private System.Windows.Forms.TextBox txtMk;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox ckBHienMk;
        private System.Windows.Forms.Button btnTaoTK;
    }
}