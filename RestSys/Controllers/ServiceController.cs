using RestSys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestSys.Controllers
{
    public class ServiceController : ApiController
    {
        RSDbContext db = new RSDbContext();

        // GET api/<controller>
        public IEnumerable<object> GetNavigationItems()
        {
            return db.Navigation.Include("ProductLink").ToList().Select(n => new RSNavigationItem()
            {
                Id = n.Id,
                Title = n.Title,
                ChildrenOrder = n.ChildrenOrder,
                Image = n.Image,
                ProductLink = n.ProductLink != null ? new RSProduct()
                {
                    Id = n.ProductLink.Id,
                    Title = n.ProductLink.Title
                } : null,
                IsRoot = n.IsRoot,
                Description = n.Description,
                Color = n.Color,
            });
        }

        [HttpPost]
        public async Task<bool> SetOrderItemState(int id, [FromBody]int state)
        {
            RSOrderItem oi = await db.OrderItems.FindAsync(id);
            if (oi.State < 2)
            {
                oi.State = state;
                await db.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public IEnumerable<RSOrder> GetOrders()
        {
            return db.Orders.Where(o => o.Active).ToList().Select(order =>
            new RSOrder()
            {
                Id = order.Id,
                Active = order.Active,
                CreatedOn = order.CreatedOn,
                Notes = order.Notes,
                Title = order.Title,
                Items = order.Items != null ? order.Items.Select(oi => new RSOrderItem()
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    State = oi.State,
                }).ToList() : null
            });
        }

        public RSOrder GetOrder(int id)
        {
            RSOrder order = db.Orders.Include("Items.Product").SingleOrDefault(i => i.Id == id);
            if (order == null) return null;

            return new RSOrder()
            {
                Id = order.Id,
                Active = order.Active,
                CreatedOn = order.CreatedOn,
                Notes = order.Notes,
                Title = order.Title,
                Items = order.Items != null ? order.Items.Select(oi => new RSOrderItem()
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    State = oi.State,
                    Product = oi.Product != null ? new RSProduct()
                    {
                        Id = oi.Product.Id,
                        Title = oi.Product.Title
                    } : null
                }).ToList() : null
            };
        }

        [HttpGet]
        public async Task<bool> CloseOrder(int id)
        {
            RSOrder order = await db.Orders.FindAsync(id);

            foreach (RSOrderItem item in order.Items) //All items must be resolved
                if (item.State < (int)RSOrderItemState.Canceled)
                    return false;

            order.Active = false;
            await db.SaveChangesAsync();
            return true;
        }

        [HttpPost]
        public async Task<RSReceipt> CreateReceipt(int id, [FromBody]IEnumerable<int> orderItemIds)
        {
            RSReceipt receipt = new RSReceipt();
            RSOrder order = await db.Orders.FindAsync(id);
            receipt.Order = order;
            receipt.User = User.Identity as RSUser;

            foreach (int orderItemId in orderItemIds)
            {
                RSOrderItem orderItem = await db.OrderItems.FindAsync(orderItemId);
                receipt.PaidItems.Add(orderItem);
            }

            db.Receipts.Add(receipt);
            await db.SaveChangesAsync();
            return receipt;
        }

        [HttpPost]
        public async Task<RSOrderItem> AddOrderItem(int id, [FromBody]int productId)
        {
            RSOrder order = await db.Orders.FindAsync(id);
            RSProduct product = await db.Products.FindAsync(productId);
            if (order != null && product != null)
            {
                RSOrderItem oi = new RSOrderItem(product, order);
                await db.SaveChangesAsync();
                return new RSOrderItem()
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    State = oi.State,
                    Product = oi.Product != null ? new RSProduct()
                    {
                        Id = oi.Product.Id,
                        Title = oi.Product.Title
                    } : null
                };
            }
            else return null;
        }
    }
}