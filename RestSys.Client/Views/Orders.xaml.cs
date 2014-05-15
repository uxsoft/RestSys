using RestSys.Client.Common;
using RestSys.Client.Services.EntityService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace RestSys.Client.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Orders : Page
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
            btnRefresh_Click(null, null);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        private List<RSOrder> orders { get; set; }

        public Orders()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            SettingsPane.GetForCurrentView().CommandsRequested += Orders_CommandsRequested;
        }

        void Orders_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("settings", "Nastavení", (handler) =>
            {
                new Settings().Show();
            }));
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                orders = (await Service.GetOrders()).ToList();
                grdOrders.ItemsSource = orders;
            }
            catch { }
        }

        private async void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            btnNewOrder.Flyout.Hide();
            RSOrder order = await Service.CreateOrder(txtNewOrderName.Text);
            if (order != null)
            {
                btnRefresh_Click(null, null);
            }
            else
            {
                MessageDialog md = new MessageDialog("Nebylo možno vytvořit objednávku.", "Vytvoření objednávky");
                await md.ShowAsync();
            }
            txtNewOrderName.Text = "";
        }

        private void btnSelectOrder_Click(dynamic sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OrderDetail), sender.DataContext);
        }
    }
}
