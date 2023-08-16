// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using UiDesktopApp2.ViewModels.Windows;
using Wpf.Ui.Controls;
using System.Windows;


namespace UiDesktopApp2.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
        )
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }







        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("您想要关闭应用程序还是仅隐藏到托盘？", "关闭确认", System.Windows.MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                // 执行应用程序关闭操作
                Application.Current.Shutdown();
            }
            else if (result == System.Windows.MessageBoxResult.No)
            {
                // 仅隐藏到托盘
                e.Cancel = true;
                ((Window)sender).Hide();
            }
            else
            {
                // 取消关闭操作，保持应用程序打开
                e.Cancel = true;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("信息", "你点击了帮助", System.Windows.MessageBoxButton.OKCancel);
        }

        private void myNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }


    }
}
