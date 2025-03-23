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
using QuanLyBanHang.ViewModels;

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
        private int _nextId = 1;

        public UserManagementViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _userRepository = new UserRepository();

            LoadDummyData();

            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        private void AddUser(object obj)
        {
            //var newUser = new UserModel();
            //var form = new UserFormView(newUser);
            //if (form.ShowDialog() == true)
            //{
            //    newUser.Id = _nextId++;
            //    Users.Add(newUser);
            //    _userRepository.AddUser(newUser);

            //}
            var newUser = new UserModel();
            var form = new UserFormView(newUser);
            form.Owner = Application.Current.MainWindow; // Gán Owner cho an toàn

            if (form.ShowDialog() == true)
            {
                // Reload danh sách sau khi thêm
                
                LoadUsers();
            }
        }

        private void EditUser(object obj)
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
                }
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

        private void LoadDummyData()
        {
            Users.Add(new UserModel { Id = 1, TaiKhoan = "admin", MatKhau = "123", HoTen = "Nguyễn Văn A", VaiTro = "Quản trị", Email = "a@email.com", SoDienThoai = "0123456789", DiaChi = "Hà Nội", GioiTinh = "Nam" });
            Users.Add(new UserModel { Id = 2, TaiKhoan = "user", MatKhau = "321", HoTen = "Trần Thị B", VaiTro = "Nhân viên", Email = "b@email.com", SoDienThoai = "0987654321", DiaChi = "HCM", GioiTinh = "Nữ" });
            _nextId = Users.Max(u => u.Id) + 1;
        }
        private void LoadUsers()
        {
            //Users.Clear();
            var userList = _userRepository.GetAllUsers();
            foreach (var user in userList)
            {
                Users.Add(user);
            }
        }



        private void DeleteUser(object obj)
        {
            if (SelectedUser != null)
            {
                Users.Remove(SelectedUser);
                _userRepository.DeleteUser(SelectedUser.Id);
            }
        }
    }
}