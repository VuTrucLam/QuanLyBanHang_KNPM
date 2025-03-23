using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using QuanLyBanHang.Helpers; // nhớ đúng namespace chứa RelayCommand
using QuanLyBanHang.Views;
using QuanLyBanHang.Models;
using QuanLyBanHang.Repositories;
using System.Collections.ObjectModel;





namespace QuanLyBanHang.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowUserManagementCommand { get; }

        public ObservableCollection<UserModel> Users { get; set; }

        private UserManagementView userManagementView;
        private UserManagementViewModel userManagementViewModel;

        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();

            ShowHomeCommand = new RelayCommand(o =>
            {
                CurrentView = new WelcomeView();
            });

            ShowUserManagementCommand = new RelayCommand(o =>
            {
                // ⚠ Khởi tạo 1 lần, sau đó tái sử dụng
                if (userManagementView == null)
                {
                    userManagementViewModel = new UserManagementViewModel();
                    userManagementView = new UserManagementView
                    {
                        DataContext = userManagementViewModel
                    };
                }
                CurrentView = userManagementView;
            });

            CurrentView = new WelcomeView();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
