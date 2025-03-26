using System.Data.SQLite;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.DataAccess
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=QuanLyBanHang.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Kiểm tra xem bảng Users đã tồn tại chưa
                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Users'";
                bool tableExists = false;
                using (var command = new SQLiteCommand(checkTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tableExists = true;
                        }
                    }
                }

                // Nếu bảng Users chưa tồn tại, tạo bảng và thêm dữ liệu mặc định
                if (!tableExists)
                {
                    // Tạo bảng Users
                    string createTableQuery = @"
                        CREATE TABLE Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            HoTen TEXT NOT NULL,
                            TaiKhoan TEXT NOT NULL,
                            MatKhau TEXT NOT NULL,
                            VaiTro TEXT,
                            Email TEXT,
                            SoDienThoai TEXT,
                            DiaChi TEXT,
                            GioiTinh TEXT,
                            HinhAnhPath TEXT
                        )";
                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Thêm 2 người dùng mặc định
                    string insertUserQuery = @"
                        INSERT INTO Users (HoTen, TaiKhoan, MatKhau, VaiTro, Email, SoDienThoai, DiaChi, GioiTinh, HinhAnhPath)
                        VALUES (@HoTen, @TaiKhoan, @MatKhau, @VaiTro, @Email, @SoDienThoai, @DiaChi, @GioiTinh, @HinhAnhPath)";

                    // Người dùng 1: Quản trị viên
                    using (var command = new SQLiteCommand(insertUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@HoTen", "Nguyễn Văn Admin");
                        command.Parameters.AddWithValue("@TaiKhoan", "admin");
                        command.Parameters.AddWithValue("@MatKhau", "admin123");
                        command.Parameters.AddWithValue("@VaiTro", "Quản lý");
                        command.Parameters.AddWithValue("@Email", "admin@example.com");
                        command.Parameters.AddWithValue("@SoDienThoai", "0123456789");
                        command.Parameters.AddWithValue("@DiaChi", "Hà Nội");
                        command.Parameters.AddWithValue("@GioiTinh", "Nam");
                        command.Parameters.AddWithValue("@HinhAnhPath", "");
                        command.ExecuteNonQuery();
                    }

                    // Người dùng 2: Người dùng thông thường
                    using (var command = new SQLiteCommand(insertUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@HoTen", "Trần Thị User");
                        command.Parameters.AddWithValue("@TaiKhoan", "user");
                        command.Parameters.AddWithValue("@MatKhau", "user123");
                        command.Parameters.AddWithValue("@VaiTro", "Khách hàng");
                        command.Parameters.AddWithValue("@Email", "user@example.com");
                        command.Parameters.AddWithValue("@SoDienThoai", "0987654321");
                        command.Parameters.AddWithValue("@DiaChi", "TP. Hồ Chí Minh");
                        command.Parameters.AddWithValue("@GioiTinh", "Nữ");
                        command.Parameters.AddWithValue("@HinhAnhPath", "");
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}