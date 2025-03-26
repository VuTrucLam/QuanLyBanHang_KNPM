using System.Windows;
using QuanLyBanHang.DataAccess;

namespace QuanLyBanHang
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseHelper.InitializeDatabase(); // Khởi tạo cơ sở dữ liệu khi ứng dụng chạy
        }
    }
}