using QuanLyBanHang.DataAccess;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace QuanLyBanHang.Repositories
{
    public class UserRepository
    {
        public void AddUser(UserModel user)
        {
            
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO Users (HoTen, TaiKhoan, MatKhau, VaiTro, Email, SoDienThoai, DiaChi, GioiTinh, HinhAnhPath)
                        VALUES (@HoTen, @TaiKhoan, @MatKhau, @VaiTro, @Email, @SoDienThoai, @DiaChi, @GioiTinh, @HinhAnhPath)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HoTen", user.HoTen);
                        command.Parameters.AddWithValue("@TaiKhoan", user.TaiKhoan);
                        command.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                        command.Parameters.AddWithValue("@VaiTro", user.VaiTro);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@SoDienThoai", user.SoDienThoai);
                        command.Parameters.AddWithValue("@DiaChi", user.DiaChi);
                        command.Parameters.AddWithValue("@GioiTinh", user.GioiTinh);
                        command.Parameters.AddWithValue("@HinhAnhPath", user.HinhAnhPath);
                        command.ExecuteNonQuery();
                    }
                }
            
           
        }

        public List<UserModel> GetAllUsers()
        {
            try
            {
                var users = new List<UserModel>();
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Users";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new UserModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    HoTen = reader["HoTen"].ToString(),
                                    TaiKhoan = reader["TaiKhoan"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString(),
                                    VaiTro = reader["VaiTro"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    SoDienThoai = reader["SoDienThoai"].ToString(),
                                    DiaChi = reader["DiaChi"].ToString(),
                                    GioiTinh = reader["GioiTinh"].ToString(),
                                    HinhAnhPath = reader["HinhAnhPath"].ToString() // Sửa từ HinhAnh thành HinhAnhPath
                                });
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách người dùng từ cơ sở dữ liệu: {ex.Message}", ex);
            }
        }

        public void UpdateUser(UserModel user)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE Users
                        SET HoTen = @HoTen, TaiKhoan = @TaiKhoan, MatKhau = @MatKhau, VaiTro = @VaiTro,
                            Email = @Email, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, GioiTinh = @GioiTinh,
                            HinhAnhPath = @HinhAnhPath
                        WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@HoTen", user.HoTen);
                        command.Parameters.AddWithValue("@TaiKhoan", user.TaiKhoan);
                        command.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                        command.Parameters.AddWithValue("@VaiTro", user.VaiTro);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@SoDienThoai", user.SoDienThoai);
                        command.Parameters.AddWithValue("@DiaChi", user.DiaChi);
                        command.Parameters.AddWithValue("@GioiTinh", user.GioiTinh);
                        command.Parameters.AddWithValue("@HinhAnhPath", user.HinhAnhPath);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật người dùng trong cơ sở dữ liệu: {ex.Message}", ex);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa người dùng từ cơ sở dữ liệu: {ex.Message}", ex);
            }
        }
    }
}