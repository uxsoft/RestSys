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
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Printing;
using RestSys.Client.Services.EntityService;
using Windows.UI.Core;
using Windows.UI.Popups;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace RestSys.Client.Views
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class Receipt : Page
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

        public Receipt()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            RegisterForPrinting();
        }

        private RSOrder order { get; set; }
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter is RSOrder)
                order = e.NavigationParameter as RSOrder;

            if (order != null)
            {
                grdOrderItems.ItemsSource = order.Items.Where(oi => oi.State < 2);
            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            grdOrderItems.SelectAll();
        }

        private async void btnCreateReceipt_Click(object sender, RoutedEventArgs e)
        {
            btnAcceptReceipt.IsEnabled = false;
            grdOrderItems.IsEnabled = false;

            if (grdOrderItems.SelectedItems.Count == 0)
            {
                MessageDialog md = new MessageDialog("Potvrzení účtenky nebylo možno provést. Musíte označit alespoň jednu položku.", "Potvrzení účtenky");
                await md.ShowAsync();
                btnAcceptReceipt.IsEnabled = true;
                grdOrderItems.IsEnabled = true;
                return;
            }

            RSReceipt receipt = await Service.CreateReceipt(order.Id, grdOrderItems.SelectedItems.OfType<RSOrderItem>().Select(oi => oi.Id).ToList());
            if (receipt != null)
            {
                string receiptHtml = await Service.GenerateReceipt(receipt.Id);
                webReceipt.NavigateToString(receiptHtml);
                await PrintManager.ShowPrintUIAsync();

            }
            else
            {
                MessageDialog md = new MessageDialog("Potvrzení účtenky nebylo možno provést. Je možné že jde konflikt mezi zařízeními. Prosíme zkuste vytvořit účtenku znovu s aktuálními daty, které pro Vás aplikace načte.", "Potvrzení účtenky");
                await md.ShowAsync();
                order = await Service.GetOrder(order.Id);
                grdOrderItems.ItemsSource = order.Items.Where(oi => oi.State < 2);
                btnAcceptReceipt.IsEnabled = true;
                grdOrderItems.IsEnabled = true;
            }
        }

        private void RegisterForPrinting()
        {
            PrintDocument pd = new PrintDocument();
            pd.Paginate += (a, b) =>
            {
                pd.SetPreviewPageCount(1, PreviewPageCountType.Final);
            };
            pd.AddPages += (a, b) =>
            {
                pd.AddPage(webReceipt);
                pd.AddPagesComplete();
            };
            pd.GetPreviewPage += (a, b) =>
            {
                pd.SetPreviewPage(b.PageNumber, webReceipt);
            };

            PrintManager.GetForCurrentView().PrintTaskRequested += (sender, e) => e.Request.CreatePrintTask("Účtenka RestSys", async args =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    args.SetSource(pd.DocumentSource);
                });
            });
        }

        private void grdOrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblReceiptSummary.Text = string.Join("\n", grdOrderItems.SelectedItems
                .OfType<RSOrderItem>()
                .GroupBy(oi => oi.Product.Id)
                .Select(g => string.Format("{0} {1}x: {2}Kč",
                    g.First().Product.Title,
                    g.Count(),
                    g.Sum(oi => oi.Price))));

            lblReceiptTotal.Text = string.Format("Celkem: {0}Kč", grdOrderItems.SelectedItems.OfType<RSOrderItem>().Sum(oi => oi.Price));
        }
    }
}
