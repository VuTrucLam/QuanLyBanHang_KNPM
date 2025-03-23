using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; } // Nam/Nữ
        public string TaiKhoan { get; set; } // Tên đăng nhập
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string VaiTro { get; set; } // Quản lý, bán hàng, khách hàng, nhà cung cấp, quản lý kho
        public string DiaChi { get; set; }
        public string HinhAnhPath { get; set; } // Đường dẫn file ảnh
    }
}
