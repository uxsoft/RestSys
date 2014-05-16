using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSys.Models;
namespace RestSys.Client
{
    public static class Service
    {
        public async static Task<bool> SetOrderItemState(int id, int state)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/SetOrderItemState/" + id, state);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<bool>();
            else return false;
        }

        public async static Task<IEnumerable<RSOrder>> GetOrders()
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/GetOrders");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<IEnumerable<RSOrder>>();
            else return null;
        }

        public async static Task<RSOrder> GetOrder(int id)
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/GetOrder/" + id);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<RSOrder>();
            else return null;
        }

        public async static Task<bool> CloseOrder(int id)
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/CloseOrder/" + id);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<bool>();
            else return false;
        }

        public async static Task<int> CreateReceipt(int orderId, IEnumerable<int> orderItemIds)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/CreateReceipt/" + orderId, orderItemIds);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<int>();
            else return -1;
        }

        public async static Task<IEnumerable<RSNavigationItem>> GetNavigationItems()
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/GetNavigationItems");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<IEnumerable<RSNavigationItem>>();
            else return null;
        }

        public async static Task<RSOrderItem> AddOrderItem(int orderId, int productId)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/AddOrderItem/" + orderId, productId);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<RSOrderItem>();
            else return null;
        }

        public static async Task<string> GenerateReceipt(int id)
        {
            HttpResponseMessage response = await Global.Client.GetAsync("Styles/Receipt/" + id);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else return "";
        }

        public static async Task<RSOrder> CreateOrder(string title)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/CreateOrder", title);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<RSOrder>();
            else return null;
        }
    }
}
