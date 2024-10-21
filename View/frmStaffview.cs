using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace QuanLyNGK.View
{

    public partial class frmStaffview : Form
    {
        function fn = new function();
        String query;
        public frmStaffview()
        {
            InitializeComponent();
        }

        

        private void frmStaffview_Load_1(object sender, EventArgs e)
        {
            query = "select * from NhanVien";
            DataSet ds = fn.GetData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }
    }
}
