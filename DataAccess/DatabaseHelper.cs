using System;
using System.Data.SQLite;
using System.IO;

namespace QuanLyBanHang.DataAccess
{
    public static class DatabaseHelper
    {
        private static readonly string dbFile = "QuanLyBanHang.db";
        private static readonly string connectionString = $"Data Source={dbFile};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        // Hàm khởi tạo CSDL, được gọi trong App.xaml.cs
        public static void InitializeDatabase()
        {
            // Nếu chưa có file thì tạo file
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }

            // Tạo bảng nếu chưa có
            CreateUserTable();

            // Nếu cần thêm bảng khác, gọi thêm hàm tạo bảng ở đây
            // CreateProductTable();
            // CreateOrderTable();
        }

        private static void CreateUserTable()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        HoTen TEXT NULL,
                        GioiTinh TEXT,
                        TaiKhoan TEXT NOT NULL,
                        MatKhau TEXT NULL,
                        Email TEXT UNIQUE,
                        SoDienThoai TEXT UNIQUE,
                        VaiTro TEXT,
                        DiaChi TEXT,
                        HinhAnh TEXT
                    );";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
