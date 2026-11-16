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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.dgvMarket.AllowUserToAddRows = false;
            this.dgvMarket.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(26)))), ((int)(((byte)(56)))));
            this.dgvMarket.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMarket.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMarket.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.dgvMarket.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMarket.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMarket.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMarket.ColumnHeadersHeight = 40;
            this.dgvMarket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(49)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMarket.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMarket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMarket.EnableHeadersVisualStyles = false;
            this.dgvMarket.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(49)))), ((int)(((byte)(89)))));
            this.dgvMarket.Location = new System.Drawing.Point(20, 90);
            this.dgvMarket.Name = "dgvMarket";
            this.dgvMarket.ReadOnly = true;
            this.dgvMarket.RowHeadersVisible = false;
            this.dgvMarket.RowHeadersWidth = 51;
            this.dgvMarket.RowTemplate.Height = 36;
            this.dgvMarket.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMarket.Size = new System.Drawing.Size(991, 522);
            this.dgvMarket.TabIndex = 0;
            this.dgvMarket.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMarket_CellFormatting);
            // 
            // lblMarketTitle
            // 
            this.lblMarketTitle.AutoSize = true;
            this.lblMarketTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblMarketTitle.Location = new System.Drawing.Point(0, 15);
            this.lblMarketTitle.Name = "lblMarketTitle";
            this.lblMarketTitle.Size = new System.Drawing.Size(392, 38);
            this.lblMarketTitle.TabIndex = 1;
            this.lblMarketTitle.Text = "BẢNG ĐIỆN CHỨNG KHOÁN";
            // 
            // lblMarketTime
            // 
            this.lblMarketTime.AutoSize = true;
            this.lblMarketTime.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblMarketTime.Location = new System.Drawing.Point(20, 55);
            this.lblMarketTime.Name = "lblMarketTime";
            this.lblMarketTime.Size = new System.Drawing.Size(158, 23);
            this.lblMarketTime.TabIndex = 2;
            this.lblMarketTime.Text = "Thoi Gian Cap nhat";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.lblMarketTitle);
            this.panel1.Controls.Add(this.lblMarketTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 90);
            this.panel1.TabIndex = 3;
            // 
            // BangGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1031, 632);
            this.Controls.Add(this.dgvMarket);
            this.Controls.Add(this.panel1);
            this.Name = "BangGia";
            this.Padding = new System.Windows.Forms.Padding(20, 0, 20, 20);
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