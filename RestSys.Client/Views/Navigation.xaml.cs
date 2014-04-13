using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RestSys.Client.Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RestSys.Client.Views
{
    public sealed partial class Navigation : UserControl
    {
        public Navigation()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var navigationItems = await Global.Db.Navigation.Get();
            DataContext = navigationItems.FirstOrDefault(ni => ni.IsRoot);
        }
    }
}
