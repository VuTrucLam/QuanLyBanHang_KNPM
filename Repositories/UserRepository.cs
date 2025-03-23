using QuanLyBanHang.DataAccess;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Repositories
{
    public class UserRepository
    {
        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Users";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            HoTen = reader["HoTen"].ToString(),
                            TaiKhoan = reader["TaiKhoan"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            Email = reader["Email"]?.ToString(),
                            SoDienThoai = reader["SoDienThoai"]?.ToString(),
                            VaiTro = reader["VaiTro"]?.ToString(),
                            DiaChi = reader["DiaChi"]?.ToString(),
                            GioiTinh = reader["GioiTinh"]?.ToString(),
                            HinhAnhPath = reader["HinhAnh"]?.ToString()
                        });
                    }
                }
            }

            return users;
        }

        public void AddUser(UserModel user)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO Users (HoTen, GioiTinh, TaiKhoan, MatKhau, Email, SoDienThoai, VaiTro, DiaChi, HinhAnh) 
                               VALUES (@HoTen, @GioiTinh, @TaiKhoan, @MatKhau, @Email, @SoDienThoai, @VaiTro, @DiaChi, @HinhAnh)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", user.HoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", user.GioiTinh);
                    cmd.Parameters.AddWithValue("@TaiKhoan", user.TaiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@SoDienThoai", user.SoDienThoai);
                    cmd.Parameters.AddWithValue("@VaiTro", user.VaiTro);
                    cmd.Parameters.AddWithValue("@DiaChi", user.DiaChi);
                    cmd.Parameters.AddWithValue("@HinhAnh", user.HinhAnhPath);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(UserModel user)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE Users SET 
                                HoTen = @HoTen,
                                GioiTinh = @GioiTinh,
                                TaiKhoan = @TaiKhoan,
                                MatKhau = @MatKhau,
                                Email = @Email,
                                SoDienThoai = @SoDienThoai,
                                VaiTro = @VaiTro,
                                DiaChi = @DiaChi,
                                HinhAnh = @HinhAnh
                               WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", user.HoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", user.GioiTinh);
                    cmd.Parameters.AddWithValue("@TaiKhoan", user.TaiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@SoDienThoai", user.SoDienThoai);
                    cmd.Parameters.AddWithValue("@VaiTro", user.VaiTro);
                    cmd.Parameters.AddWithValue("@DiaChi", user.DiaChi);
                    cmd.Parameters.AddWithValue("@HinhAnh", user.HinhAnhPath);
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Users WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
