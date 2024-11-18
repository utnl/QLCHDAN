using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace QuanLyNGK
{
    public partial class frmProductAdd : Form
    {
        private ketnoiSQL db = new ketnoiSQL();

        public frmProductAdd()
        {
            InitializeComponent();

        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                textBoxImagePath.Text = selectedFile;
                picProduct.Image = Image.FromFile(selectedFile);
            }
        }

        public void clear()
        {
            txtID.Text = txtName.Text = txtMCC.Text = txtPrice.Text = txtQuantity.Text = string.Empty;
            guna2ComboBox1.SelectedIndex = 0;
            picProduct.Image = null;
        }

       

        private void txtMCC_DropDown(object sender, EventArgs e)
        {
            string query = "SELECT maNCC FROM NhaCungUng";
            DataTable dataTable = new DataTable();

            try
            {
                db.openConnect();
                using (SqlCommand command = new SqlCommand(query, db.getConnection()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }

                txtMCC.Items.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    txtMCC.Items.Add(row["maNCC"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                db.closeConnect();
            }
        }
        public string id, name, nsx, nhh, price, mcc, sl;
        public byte[] productImage;


        public void UpdateInfo()
        {
            label6.Text = "Edit Product Information";
            guna2Button1.Text = "Update";
            txtID.Visible = true;
            txtID.ReadOnly = true;
            txtID.Text = id;
            txtName.Text = name;
            txtPrice.Text = price;
            txtQuantity.Text = sl;
            guna2ComboBox1.Text = guna2ComboBox1.Items.Contains(name) ? name : "Đồ uống";
            dateNSX.Text=nsx;
            dateNHH.Text = nhh;


            if (productImage != null)
            {
                picProduct.Image = byteArrayToImage(productImage);
            }
        }

        private Image byteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string loaiSP = guna2ComboBox1.SelectedItem.ToString();
            string ngaySX = dateNSX.Value.ToString("yyyy-MM-dd");
            string ngayHH = dateNHH.Value.ToString("yyyy-MM-dd");
            byte[] imageBytes = !string.IsNullOrEmpty(textBoxImagePath.Text) ? File.ReadAllBytes(textBoxImagePath.Text) : null;
            if (!string.IsNullOrEmpty(textBoxImagePath.Text))
            {
               
                imageBytes = File.ReadAllBytes(textBoxImagePath.Text);
            }
            else if (productImage != null)
            {
            
                imageBytes = productImage;
            }
            if (guna2Button1.Text == "Add")
            {
                bool isAdded = db.AddProduct(txtID.Text, loaiSP, txtName.Text, ngaySX, ngayHH, int.Parse(txtPrice.Text), txtMCC.Text, imageBytes, int.Parse(txtQuantity.Text));
                if (isAdded)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    clear();
                }
            }
            else if (guna2Button1.Text == "Update")
            {
                bool isUpdated = db.UpdateProduct(txtID.Text, loaiSP, txtName.Text, ngaySX, ngayHH, int.Parse(txtPrice.Text), txtMCC.Text, imageBytes, int.Parse(txtQuantity.Text));
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!");
                    clear();
                }
            }
           
        }
    }
}
