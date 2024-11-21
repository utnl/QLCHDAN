using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNGK
{
    public partial class UcProduct : UserControl
    {
        public UcProduct()
        {
            InitializeComponent();
        }
        public event EventHandler onSelect = null;
      
        public string id { get; set; }
        public string PName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }

        }
        public string PPrice
        {
            get { return lblPrice.Text ; }
            set { lblPrice.Text = value ; }
        }
        public Image PImage
        {
            get { return txtImage.Image; }
            set { txtImage.Image = value; }
        }


        private void guna2ShadowPanel1_Paint_1(object sender, PaintEventArgs e)
        {
            
        }

        private void txtImage_Click_1(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }

    }
}
