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
using RestSys.Client.Services.EntityService;
using Windows.UI.Core;

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
            var navigationItems = await Global.Db.Navigation.ExecuteAsync();
            NavigateTo(navigationItems.FirstOrDefault(ni => ni.IsRoot));
        }

        public RSNavigationItem CurrentItem { get; set; }

        public async void NavigateTo(RSNavigationItem me)
        {
            await Global.Db.LoadPropertyAsync<object>(me, "Children");
            await Global.Db.LoadPropertyAsync<object>(me, "Parent");
            var items = await me.OrderedChildren();

            btnBack.IsEnabled = me.Parent != null;

            CurrentItem = me;
            grdChildren.ItemsSource = items;
        }

        private void btnItem_Click(dynamic sender, RoutedEventArgs e)
        {
            NavigateTo(sender.DataContext);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(CurrentItem.Parent);
        }
    }
}
