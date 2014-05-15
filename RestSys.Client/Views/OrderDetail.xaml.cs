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
using Windows.UI.Popups;

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
                order = await Service.GetOrder((int)e.NavigationParameter);

            if (e.NavigationParameter is RSOrder)
                order = await Service.GetOrder((e.NavigationParameter as RSOrder).Id);

            lstOrderItems.ItemsSource = order.Items.Where(oi => oi.State < 2);
            this.DataContext = order;
        }

        private async void navProductMenu_ProductSelected(RSProduct product)
        {
            RSOrderItem orderItem = await Service.AddOrderItem(order.Id, product.Id);
            if (orderItem != null)
            {
                order.Items.Add(orderItem);
                lstOrderItems.ItemsSource = order.Items.Where(oi => oi.State < 2);
            }
            else
            {
                MessageDialog md = new MessageDialog("Položku nelze přidat.", "Přidání položky");
                await md.ShowAsync();
            }
        }

        private async void btnReload_Click(object sender, RoutedEventArgs e)
        {
            if (order != null)
            {
                order = await Service.GetOrder(order.Id);

                lstOrderItems.ItemsSource = order.Items.Where(oi => oi.State < 2);
                this.DataContext = order;
            }
        }

        private async void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            RSOrderItem currentItem = lstOrderItems.SelectedItem as RSOrderItem;
            await Service.SetOrderItemState(currentItem.Id, 2);

            currentItem.State = 2;
            lstOrderItems.ItemsSource = order.Items.Where(oi => oi.State < 2);
        }

        private void lstOrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnItemRemoval.IsEnabled = lstOrderItems.SelectedIndex > 0;
        }

        private void btnReceipt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Receipt), order);
        }

        private void lstOrderItems_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {

        }

        private async void chcItemDispatched_Click(dynamic sender, RoutedEventArgs e)
        {
            await Service.SetOrderItemState(sender.DataContext.Id, sender.DataContext.State);
        }

        private async void btnCloseOrder_Click(object sender, RoutedEventArgs e)
        {
            if (await Service.CloseOrder(order.Id))
                this.Frame.GoBack();
            else
            {
                MessageDialog md = new MessageDialog("Nelze uzavřít objednávku. Zkontrolujte, zda-li byly všechny položky vyřízené.", "Uzavření objednávky");
                await md.ShowAsync();
            }
        }
    }
}
