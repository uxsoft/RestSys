using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using RestSys.Client.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage;

namespace RestSys.Client.Views
{
    public sealed partial class Settings : SettingsFlyout
    {
        public Settings()
        {
            this.InitializeComponent();
            this.Loaded += Settings_Loaded;
        }

        void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ApplicationData.Current.LocalSettings;
            visibility();

        }

        private void visibility()
        {
            if (Global.IsAuthenticated)
            {
                areaLogin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                areaLogOut.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                areaLogin.Visibility = Windows.UI.Xaml.Visibility.Visible;
                areaLogOut.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            if (Global.IsConnected)
            {
                areaConnect.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                areaDisconnect.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                areaConnect.Visibility = Windows.UI.Xaml.Visibility.Visible;
                areaDisconnect.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (await Global.Connect(Global.ConnectionUrl))
            {
                visibility();
            }
            else
            {
                MessageDialog md = new MessageDialog("Nelze nalézt RestSys Server na dané adrese", "Spojení");
                await md.ShowAsync();
            }
        }

        private void txtServerAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnConnect.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (await Global.Login(txtUsername.Text, txtPassword.Password))
            {
                visibility();
            }
            else
            {
                MessageDialog md = new MessageDialog("Přihlášení selhalo", "Přihlášení");
                await md.ShowAsync();
            }
            txtPassword.Password = "";
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Global.LogOut();
            visibility();
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Global.Disconnect();
            visibility();
        }
    }
}
