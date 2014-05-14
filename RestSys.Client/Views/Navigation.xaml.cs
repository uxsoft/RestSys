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
using Newtonsoft.Json.Linq;

namespace RestSys.Client.Views
{
    public sealed partial class Navigation : UserControl
    {
        public Navigation()
        {
            this.InitializeComponent();
            History = new Stack<int>();
        }

        private IEnumerable<RSNavigationItem> AllItems { get; set; }
        private Stack<int> History { get; set; }
        private int Home = 0;

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                AllItems = (await Global.Db.Navigation.ExecuteAsync()).ToList();
                Home = AllItems.FirstOrDefault(ni => ni.IsRoot).Id;
                btnHome_Click(null, null);
            });
        }

        public RSNavigationItem CurrentItem { get; set; }

        public void NavigateTo(int id)
        {
            RSNavigationItem me = AllItems.SingleOrDefault(i => i.Id == id);
            if (me != null)
            {
                History.Push(id);
                grdChildren.ItemsSource = JArray.Parse(me.ChildrenOrder).Values<int>().Select(ch => AllItems.SingleOrDefault(i => i.Id == ch));
            }
        }

        private void btnItem_Click(dynamic sender, RoutedEventArgs e)
        {
            if (sender.DataContext.ProductLink != null)
            {
                if (ProductSelected != null)
                    ProductSelected(sender.DataContext.ProductLink);
            }
            else
                NavigateTo(sender.DataContext.Id);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (History.Count > 0)
                NavigateTo(History.Pop());
        }

        public delegate void ProductHandler(RSProduct product);
        public event ProductHandler ProductSelected;

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(Home);
        }
    }
}
