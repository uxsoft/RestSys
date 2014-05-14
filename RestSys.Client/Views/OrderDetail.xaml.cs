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
using RestSys.Client.Services.EntityService;
using System.Threading.Tasks;

namespace RestSys.Client.Views
{
    public sealed partial class OrderDetail : Page
    {
        #region NavigationHelper registration
        private NavigationHelper navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public OrderDetail()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        private RSOrder order { get; set; }

        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter is int)
                order = (await Global.Db.Orders.ExecuteAsync()).SingleOrDefault(o => o.Id == (int)e.NavigationParameter);

            if (e.NavigationParameter is RSOrder)
                order = (RSOrder)e.NavigationParameter;

            if (order != null)
            {
                await Global.Db.LoadPropertyAsync<object>(order, "Items");

                await Task.WhenAll(order.Items.Select(i => Global.Db.LoadPropertyAsync<object>(i, "Product")));

                lstOrderItems.ItemsSource = order.Items;
                this.DataContext = order;
            }
        }
    }
}
