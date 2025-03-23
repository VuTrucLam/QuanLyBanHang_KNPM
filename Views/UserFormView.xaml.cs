using System;
using System.Windows;
using Microsoft.Win32;
using QuanLyBanHang.Models;
using QuanLyBanHang.ViewModels;

namespace QuanLyBanHang.Views
{
    /// <summary>
    /// Interaction logic for UserFormView.xaml
    /// </summary>
    public partial class UserFormView : Window
    {
        //public UserFormView(UserModel user = null)
        //{
        //    InitializeComponent();
        //    var vm = new UserFormViewModel(user);
        //    vm.CloseAction = new Action(this.Close);  // ✅ Gán Action đúng cách
        //    this.DataContext = vm;
        //}
        public UserFormView(UserModel user)
        {
            InitializeComponent();

            var viewModel = new UserFormViewModel(user);
            viewModel.CloseAction = () =>
            {
                // Đóng form đúng cách
                this.DialogResult = true;
            };

            this.DataContext = viewModel;
        }

        private void ChonAnh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                var vm = DataContext as UserFormViewModel; // ✅ Sửa đúng tên ViewModel
                if (vm != null)
                {
                    vm.HinhAnh = openFileDialog.FileName;
                }
            }
        }

        private void Huy_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng form khi nhấn Huỷ
        }
    }
}
