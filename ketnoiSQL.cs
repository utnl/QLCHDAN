using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNGK
{
    internal class ketnoiSQL
    {
        public SqlConnection conn;
        public void openConnect()
        {
            conn = new SqlConnection("Server=LAPTOP-5I8I5M5T\\SQLEXPRESS; Database=QLDAN;Integrated Security=True");
            conn.Open();
        }
        public void closeConnect()
        {
            conn.Close();
        }
        public SqlConnection getConnection()
        {
            return conn;
        }
        public DataTable GetTable(string table)
        {
            DataTable bang = new DataTable();
            try
            {
                openConnect();
                string sql = $"SELECT * FROM {table}"; 
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bang.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                closeConnect();
            }
            return bang; 
        }
        public bool CheckMaNVExists(string maNV)
        {
            try
            {
                openConnect();
                string sql = "SELECT COUNT(*) FROM NhanVien WHERE maNV = @maNV";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi kiểm tra mã nhân viên: " + ex.Message);
                return true; 
            }
            finally
            {
                closeConnect();
            }
        }
        public bool AddNhanVien(string maNV, string tenNV, int tuoi, string gioiTinh, string sdt, string diaChi)
        {
            try
            {
                openConnect();
                string sql = "INSERT INTO NhanVien (maNV, tenNV, tuoi, gioiTinh, sdt, diaChi) VALUES (@maNV, @tenNV, @tuoi, @gioiTinh, @sdt, @diaChi)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@tenNV", tenNV);
                    cmd.Parameters.AddWithValue("@tuoi", tuoi);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu thêm thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm nhân viên: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }

        public bool UpdateNhanVien(string maNV, string tenNV, int tuoi, string gioiTinh, string sdt, string diaChi)
        {
            try
            {
                openConnect();
                string sql = "UPDATE NhanVien SET tenNV = @tenNV, tuoi = @tuoi, gioiTinh = @gioiTinh, sdt = @sdt, diaChi = @diaChi WHERE maNV = @maNV";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    cmd.Parameters.AddWithValue("@tenNV", tenNV);
                    cmd.Parameters.AddWithValue("@tuoi", tuoi);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu cập nhật thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật nhân viên: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public bool DeleteNhanVien(string maNV)
        {
            try
            {
                openConnect();
                string sql = "DELETE FROM NhanVien WHERE maNV = @maNV";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", maNV);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa nhân viên: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public DataTable SearchNhanVienByName(string name)
        {
            DataTable bang = new DataTable();
            try
            {
                openConnect();
                string sql = "SELECT * FROM NhanVien WHERE tenNV LIKE @name";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bang.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm nhân viên: " + ex.Message);
            }
            finally
            {
                closeConnect();
            }
            return bang;
        }


    }
}
