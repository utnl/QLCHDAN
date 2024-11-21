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
        public bool AddProduct(string maSP, string loaiSP, string tenSP, string nsx, string nhh, int gia, string maNCC, byte[] hinhAnh, int soLuong)
        {
            try
            {
                openConnect();
                string sql = "INSERT INTO SanPham (maSP, loaiSP, tenSP, ngaySX, ngayHH, giaSP, maNCC, hinhAnh, soLuong) " +
                             "VALUES (@maSP, @loaiSP, @tenSP, @ngaySX, @ngayHH, @giaSP, @maNCC, @hinhAnh, @soLuong)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", maSP);
                    cmd.Parameters.AddWithValue("@loaiSP", loaiSP);
                    cmd.Parameters.AddWithValue("@tenSP", tenSP);
                    cmd.Parameters.AddWithValue("@ngaySX", nsx);
                    cmd.Parameters.AddWithValue("@ngayHH", nhh);
                    cmd.Parameters.AddWithValue("@giaSP", gia);
                    cmd.Parameters.AddWithValue("@maNCC", maNCC);
                    cmd.Parameters.AddWithValue("@hinhAnh", hinhAnh);
                    cmd.Parameters.AddWithValue("@soLuong", soLuong);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm sản phẩm: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }


        public bool UpdateProduct(string maSP, string loaiSP, string tenSP, string nsx, string nhh, int gia, string maNCC, byte[] hinhAnh, int soLuong)
        {
            try
            {
                openConnect();
                string sql = "UPDATE SanPham SET loaiSP = @loaiSP, tenSP = @tenSP, ngaySX = @ngaySX, ngayHH = @ngayHH, giaSP = @giaSP, " +
                             "maNCC = @maNCC, hinhAnh = @hinhAnh, soLuong = @soLuong WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", maSP);
                    cmd.Parameters.AddWithValue("@loaiSP", loaiSP);
                    cmd.Parameters.AddWithValue("@tenSP", tenSP);
                    cmd.Parameters.AddWithValue("@ngaySX", nsx);
                    cmd.Parameters.AddWithValue("@ngayHH", nhh);
                    cmd.Parameters.AddWithValue("@giaSP", gia);
                    cmd.Parameters.AddWithValue("@maNCC", maNCC);
                    cmd.Parameters.AddWithValue("@hinhAnh", hinhAnh);
                    cmd.Parameters.AddWithValue("@soLuong", soLuong);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật sản phẩm: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public bool DeleteProduct(string maSP)
        {
            try
            {
                openConnect();
                string sql = "DELETE FROM SanPham WHERE maSP = @maSP";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", maSP);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu xóa thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa sản phẩm: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public DataTable SeachProduct(string name)
        {
            DataTable bang = new DataTable();
            try
            {
                openConnect();
                string sql = "SELECT * FROM SanPham WHERE tenSP LIKE @name";
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
        public bool AddNCC(string maNCC, string tenNCC, string SDT, string diaChi)
        {
            try
            {
                openConnect();
                string sql = "INSERT INTO NhaCungUng (maNCC, tenNCC, sdtNCC, diaChi) VALUES (@maNCC, @tenNCC, @sdtNCC, @diaChi)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNCC", maNCC);
                    cmd.Parameters.AddWithValue("@tenNCC", tenNCC);
                    cmd.Parameters.AddWithValue("@sdtNCC", SDT); 
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm NCC: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }

        public bool UpdateNCC(string maNCC, string tenNCC, string SDT, string diaChi)
        {
            try
            {
                openConnect();
                string sql = "UPDATE NhaCungUng SET tenNCC = @tenNCC, sdtNCC = @SDT, diaChi = @diaChi WHERE maNCC = @maNCC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNCC", maNCC);
                    cmd.Parameters.AddWithValue("@tenNCC", tenNCC);
                    cmd.Parameters.AddWithValue("@sdtNCC", SDT);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật NCC: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }

        // Xóa nhà cung cấp (NCC)
        public bool DeleteNCC(string maNCC)
        {
            try
            {
                openConnect();
                string sql = "DELETE FROM NhaCungUng WHERE maNCC = @maNCC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNCC", maNCC);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa NCC: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }

        // Tìm kiếm nhà cung cấp (NCC)
        public DataTable SearchNCC(string name)
        {
            DataTable bang = new DataTable();
            try
            {
                openConnect();
                string sql = "SELECT * FROM NhaCungUng WHERE tenNCC LIKE @name";
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
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm NCC: " + ex.Message);
            }
            finally
            {
                closeConnect();
            }
            return bang;
        }

        public bool AddKhachHang(string maKH, string tenKH, string gioiTinh, string sdt, string diaChi, int solanmuahang, string kieuKH)
        {
            try
            {
                openConnect();
                string sql = "INSERT INTO KhachHang (maKH, tenKH, gioiTinh, sdt, diaChi, solanmuahang, kieuKH) VALUES (@maKH, @tenKH, @gioiTinh, @sdt, @diaChi, @solanmuahang, @kieuKH)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maKH", maKH);
                    cmd.Parameters.AddWithValue("@tenKH", tenKH);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);
                    cmd.Parameters.AddWithValue("@solanmuahang", solanmuahang);
                    cmd.Parameters.AddWithValue("@kieuKH", kieuKH);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm khách hàng: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public bool UpdateKhachHang(string maKH, string tenKH, string gioiTinh, string sdt, string diaChi, int solanmuahang, string kieuKH)
        {
            try
            {
                openConnect();
                string sql = "UPDATE KhachHang SET tenKH = @tenKH, gioiTinh = @gioiTinh, sdt = @sdt, diaChi = @diaChi, solanmuahang = @solanmuahang, kieuKH = @kieuKH WHERE maKH = @maKH";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maKH", maKH);
                    cmd.Parameters.AddWithValue("@tenKH", tenKH);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);
                    cmd.Parameters.AddWithValue("@solanmuahang", solanmuahang);
                    cmd.Parameters.AddWithValue("@kieuKH", kieuKH);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public bool DeleteKhachHang(string maKH)
        {
            try
            {
                openConnect();
                string sql = "DELETE FROM KhachHang WHERE maKH = @maKH";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maKH", maKH);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa khách hàng: " + ex.Message);
                return false;
            }
            finally
            {
                closeConnect();
            }
        }
        public DataTable SearchKhachHangByName(string name)
        {
            DataTable bang = new DataTable();
            try
            {
                openConnect();
                string sql = "SELECT * FROM KhachHang WHERE tenKH LIKE @name";
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
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm khách hàng: " + ex.Message);
            }
            finally
            {
                closeConnect();
            }
            return bang;
        }

    }
}
