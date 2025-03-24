using System;
using System.Windows;
using Microsoft.Win32;
using QuanLyBanHang.Models;
using QuanLyBanHang.ViewModels;

namespace QuanLyBanHang.Views
{
    public partial class UserFormView : Window
    {
        public UserModel UserData { get; private set; }

        public UserFormView(UserModel user)
        {
            InitializeComponent();

            var viewModel = new UserFormViewModel(user);
            viewModel.CloseAction = () =>
            {
                this.DialogResult = true; // Đặt DialogResult thành true khi lưu thành công
                this.Close();
            };

            this.DataContext = viewModel;
        }

        private void ChonAnh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                var vm = DataContext as UserFormViewModel;
                if (vm != null)
                {
                    vm.HinhAnh = openFileDialog.FileName;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserFormViewModel viewModel)
            {
                viewModel.User.MatKhau = PasswordBox.Password;
            }
        }

        private void Huy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Đặt DialogResult thành false khi hủy
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserFormViewModel viewModel)
            {
                // Gọi SaveUser thông qua Command
                viewModel.SaveCommand.Execute(null);
            }
        }
    }
}