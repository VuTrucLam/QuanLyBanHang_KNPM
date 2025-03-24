using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using QuanLyBanHang.Views;
using QuanLyBanHang.Repositories;
using System.Windows;

namespace QuanLyBanHang.ViewModels
{
    public class UserManagementViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public UserModel SelectedUser { get; set; }

        public ICommand AddUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        private UserRepository _userRepository;

        public UserManagementViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _userRepository = new UserRepository();

            // Load dữ liệu từ cơ sở dữ liệu
            LoadUsers();

            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        private void AddUser(object obj)
        {
            
                var newUser = new UserModel();
                var form = new UserFormView(newUser);
                form.Owner = Application.Current.MainWindow;

                if (form.ShowDialog() == true)
                {
                    //_userRepository.AddUser(newUser);
                    // Làm mới danh sách sau khi thêm
                    LoadUsers();
                }
            
        }

        private void EditUser(object obj)
        {
            try
            {
                if (SelectedUser != null)
                {
                    var tempUser = new UserModel
                    {
                        Id = SelectedUser.Id,
                        HoTen = SelectedUser.HoTen,
                        TaiKhoan = SelectedUser.TaiKhoan,
                        MatKhau = SelectedUser.MatKhau,
                        VaiTro = SelectedUser.VaiTro,
                        Email = SelectedUser.Email,
                        SoDienThoai = SelectedUser.SoDienThoai,
                        DiaChi = SelectedUser.DiaChi,
                        GioiTinh = SelectedUser.GioiTinh,
                        HinhAnhPath = SelectedUser.HinhAnhPath
                    };

                    var form = new UserFormView(tempUser);
                    if (form.ShowDialog() == true)
                    {
                        SelectedUser.HoTen = tempUser.HoTen;
                        SelectedUser.TaiKhoan = tempUser.TaiKhoan;
                        SelectedUser.MatKhau = tempUser.MatKhau;
                        SelectedUser.VaiTro = tempUser.VaiTro;
                        SelectedUser.Email = tempUser.Email;
                        SelectedUser.SoDienThoai = tempUser.SoDienThoai;
                        SelectedUser.DiaChi = tempUser.DiaChi;
                        SelectedUser.GioiTinh = tempUser.GioiTinh;
                        SelectedUser.HinhAnhPath = tempUser.HinhAnhPath;

                        _userRepository.UpdateUser(SelectedUser);
                        // Làm mới danh sách sau khi sửa
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form sửa người dùng: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteUser(object obj)
        {
            try
            {
                if (SelectedUser != null)
                {
                    _userRepository.DeleteUser(SelectedUser.Id);
                    // Làm mới danh sách sau khi xóa
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                // Xóa danh sách hiện tại để tránh trùng lặp
                Users.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và thêm vào danh sách
                var userList = _userRepository.GetAllUsers();
                foreach (var user in userList)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách người dùng: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsDuplicateEmail(string email, int userId)
        {
            var existingUser = _userRepository.GetAllUsers()
                                 .FirstOrDefault(u => u.Email == email && u.Id != userId);
            return existingUser != null;
        }

        private bool IsDuplicatePhone(string phone, int userId)
        {
            var existingUser = _userRepository.GetAllUsers()
                                 .FirstOrDefault(u => u.SoDienThoai == phone && u.Id != userId);
            return existingUser != null;
        }
    }
}