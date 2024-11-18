using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNGK.View;

namespace QuanLyNGK
{
    public partial class fManage : Form
    {
       

        public fManage()
        {
            InitializeComponent();
            PanelMenu.Visible = false;
            btnPOS.Location = new Point(0, btnCategories.Location.Y + btnCategories.Height + 10);
            btnStatistics.Location = new Point(0, btnPOS.Location.Y + btnPOS.Height + 10);
            PanelReport.Visible = false;
            PanelReport.Location = new Point(0, btnStatistics.Location.Y + btnStatistics.Height + 10);
        }


        public void AddControls(Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }


        private void fManage_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello," + Login.USER;
        }
        public static string user = Login.USER;


        private void btnCategories_Click(object sender, EventArgs e)
        {
            
            PanelReport.Visible = false;
            PanelMenu.Visible = !PanelMenu.Visible;
        }

        private void btnProduct_Click_1(object sender, EventArgs e)
        {
            AddControls(new frmProductView());
        }

        private void PanelMenu_VisibleChanged(object sender, EventArgs e)
        {
            if (!PanelMenu.Visible)
            {
               
                btnPOS.Location = new Point(0, btnCategories.Location.Y + btnCategories.Height + 10);
                btnStatistics.Location = new Point(0, btnPOS.Location.Y + btnPOS.Height + 10);
            }
            else
            {
              
                btnPOS.Location = new Point(3, 650); 
                btnStatistics.Location = new Point(0, btnPOS.Location.Y + btnPOS.Height + 10);
            }
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            PanelMenu.Visible = false;
            PanelReport.Visible = !PanelReport.Visible;
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffview());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            AddControls(new frmNCCview());
        }
    }
}
