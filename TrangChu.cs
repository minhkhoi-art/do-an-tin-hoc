using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // quay lại form1
            this.Close();
        }
    }
}
