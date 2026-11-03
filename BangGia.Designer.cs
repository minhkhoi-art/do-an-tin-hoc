namespace đồ_án_tin_học_1
{
    partial class BangGia
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
            this.dgvMarket = new System.Windows.Forms.DataGridView();
            this.lblMarketTitle = new System.Windows.Forms.Label();
            this.lblMarketTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarket)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMarket
            // 
            this.dgvMarket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarket.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvMarket.Location = new System.Drawing.Point(-1, 128);
            this.dgvMarket.Name = "dgvMarket";
            this.dgvMarket.RowHeadersWidth = 51;
            this.dgvMarket.RowTemplate.Height = 24;
            this.dgvMarket.Size = new System.Drawing.Size(973, 492);
            this.dgvMarket.TabIndex = 0;
            this.dgvMarket.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgvMarket.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMarket_CellFormatting);
            // 
            // lblMarketTitle
            // 
            this.lblMarketTitle.AutoSize = true;
            this.lblMarketTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMarketTitle.Location = new System.Drawing.Point(-7, 0);
            this.lblMarketTitle.Name = "lblMarketTitle";
            this.lblMarketTitle.Size = new System.Drawing.Size(533, 42);
            this.lblMarketTitle.TabIndex = 1;
            this.lblMarketTitle.Text = "BẢNG ĐIỆN CHỨNG KHOÁN";
            this.lblMarketTitle.Click += new System.EventHandler(this.lblMarketTitle_Click);
            // 
            // lblMarketTime
            // 
            this.lblMarketTime.AutoSize = true;
            this.lblMarketTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketTime.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMarketTime.Location = new System.Drawing.Point(3, 57);
            this.lblMarketTime.Name = "lblMarketTime";
            this.lblMarketTime.Size = new System.Drawing.Size(164, 22);
            this.lblMarketTime.TabIndex = 2;
            this.lblMarketTime.Text = "Thoi Gian Cap nhat";
            this.lblMarketTime.Click += new System.EventHandler(this.lblMarketTime_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGreen;
            this.panel1.Controls.Add(this.lblMarketTitle);
            this.panel1.Controls.Add(this.lblMarketTime);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 127);
            this.panel1.TabIndex = 3;
            // 
            // BangGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 632);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvMarket);
            this.Name = "BangGia";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarket)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMarket;
        private System.Windows.Forms.Label lblMarketTitle;
        private System.Windows.Forms.Label lblMarketTime;
        private System.Windows.Forms.Panel panel1;
    }
}