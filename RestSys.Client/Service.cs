using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSys.Client.Services.EntityService;
namespace RestSys.Client
{
    public static class Service
    {
        public async static Task<bool> SetOrderItemState(int id, int state)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/SetOrderItemState/" + id, state);
            return await response.Content.ReadAsAsync<bool>();
        }

        public async static Task<IEnumerable<RSOrder>> GetOrders()
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/GetOrders");
            return await response.Content.ReadAsAsync<IEnumerable<RSOrder>>();
        }

        public async static Task<RSOrder> GetOrder(int id)
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/GetOrder/" + id);
            return await response.Content.ReadAsAsync<RSOrder>();
        }

        public async static Task<bool> CloseOrder(int id)
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/CloseOrder/" + id);
            return await response.Content.ReadAsAsync<bool>();
        }

        public async static Task<RSReceipt> CreateReceipt(int orderId, IEnumerable<int> orderItemIds)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/CreateReceipt/" + orderId, orderItemIds);
            return await response.Content.ReadAsAsync<RSReceipt>();
        }

        public async static Task<IEnumerable<RSNavigationItem>> GetNavigationItems()
        {
            HttpResponseMessage response = await Global.Client.GetAsync("api/Service/GetNavigationItems");
            return await response.Content.ReadAsAsync<IEnumerable<RSNavigationItem>>();
        }

        public async static Task<RSOrderItem> AddOrderItem(int orderId, int productId)
        {
            HttpResponseMessage response = await Global.Client.PostAsJsonAsync("api/Service/AddOrderItem/" + orderId, productId);
            return await response.Content.ReadAsAsync<RSOrderItem>();
        }
    }
}
