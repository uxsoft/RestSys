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
        }

        private RSOrder order { get; set; }
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter is RSOrder)
                order = e.NavigationParameter as RSOrder;

            if (order != null)
            {
                grdOrderItems.ItemsSource = order.Items;
            }
        }



        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            grdOrderItems.SelectAll();
        }

        private async void btnCreateReceipt_Click(object sender, RoutedEventArgs e)
        {
            printurl("http://seznam.cz");
        }

        private async void printurl(string url)
        {
            PrintManager.GetForCurrentView().PrintTaskRequested += (sender, e) => e.Request.CreatePrintTask("Účtenka RestSys", args =>
            {
                WebView doc = new WebView();
                doc.NavigateToString(url);

                PrintDocument pd = new PrintDocument();
                pd.AddPage(doc);

                args.SetSource(pd.DocumentSource);
            });
            await PrintManager.ShowPrintUIAsync();
        }
    }
}
