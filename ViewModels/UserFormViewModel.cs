﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHang.Models;
using QuanLyBanHang.Repositories;
using QuanLyBanHang.Helpers;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QuanLyBanHang.ViewModels
{
    public class UserFormViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly UserRepository _userRepository;

        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private ObservableCollection<UserModel> _existingUsers;
        public ObservableCollection<UserModel> ExistingUsers
        {
            get => _existingUsers;
            set
            {
                _existingUsers = value;
                OnPropertyChanged(nameof(ExistingUsers));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public Action CloseAction { get; set; }

        private string _hinhAnh;
        public string HinhAnh
        {
            get => _hinhAnh;
            set
            {
                _hinhAnh = value;
                User.HinhAnhPath = value; // Sửa từ HinhAnh thành HinhAnhPath
                OnPropertyChanged(nameof(HinhAnh));
            }
        }

        public UserFormViewModel(UserModel user = null)
        {
            try
            {
                _userRepository = new UserRepository();
                User = user != null ? new UserModel
                {
                    Id = user.Id,
                    HoTen = user.HoTen,
                    Email = user.Email,
                    SoDienThoai = user.SoDienThoai,
                    TaiKhoan = user.TaiKhoan,
                    VaiTro = user.VaiTro,
                    GioiTinh = user.GioiTinh,
                    DiaChi = user.DiaChi,
                    HinhAnhPath = user.HinhAnhPath
                } : new UserModel();

                ExistingUsers = new ObservableCollection<UserModel>();
                var users = _userRepository.GetAllUsers();
                if (users != null)
                {
                    foreach (var u in users)
                    {
                        ExistingUsers.Add(u);
                    }
                }

                SaveCommand = new RelayCommand(SaveUser);
                CancelCommand = new RelayCommand(_ => CloseAction?.Invoke());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo UserFormViewModel: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        public List<string> GioiTinhList { get; set; } = new List<string> { "Nam", "Nữ" };

        public List<string> VaiTroList { get; set; } = new List<string>
        {
            "Quản lý",
            "Bán hàng",
            "Khách hàng",
            "Nhà cung cấp",
            "Quản lý kho"
        };


        private void SaveUser(object obj)
        {
            
            {
                // Kiểm tra dữ liệu bắt buộc
                //if (string.IsNullOrWhiteSpace(User.HoTen) || string.IsNullOrWhiteSpace(User.TaiKhoan) || string.IsNullOrWhiteSpace(User.MatKhau))
                //{
                //    MessageBox.Show("Vui lòng nhập đầy đủ Họ tên, Tên đăng nhập và Mật khẩu.");
                //    return;
                //}

                if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.SoDienThoai))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Email và Số điện thoại.");
                    return;
                }

                // Kiểm tra trùng Email
                if (IsDuplicateEmail(User.Email))
                {
                    MessageBox.Show("Email đã tồn tại. Vui lòng nhập email khác.");
                    return;
                }

                // Kiểm tra trùng SĐT
                if (IsDuplicatePhone(User.SoDienThoai))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập số khác.");
                    return;
                }

                // Lưu vào CSDL
                if (User.Id == 0)
                {
                    _userRepository.AddUser(User);
                }
                else
                {
                    _userRepository.UpdateUser(User);
                }

                MessageBox.Show("Lưu người dùng thành công!");
                CloseAction?.Invoke();
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool IsDuplicateEmail(string email)
        {
            var existingUser = _userRepository.GetAllUsers()
                                 .FirstOrDefault(u => u.Email == email && u.Id != User.Id);
            return existingUser != null;
        }

        private bool IsDuplicatePhone(string phone)
        {
            var existingUser = _userRepository.GetAllUsers()
                                 .FirstOrDefault(u => u.SoDienThoai == phone && u.Id != User.Id);
            return existingUser != null;
        }
    }
}