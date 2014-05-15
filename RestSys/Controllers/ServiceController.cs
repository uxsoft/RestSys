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
    [Authorize]
    public class ServiceController : ApiController
    {
        RSDbContext db = new RSDbContext();

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
            if (oi != null)
                if (oi.State < 2)
                {
                    oi.State = state;
                    await db.SaveChangesAsync();
                    return true;
                }
            return false;
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
        public RSReceipt CreateReceipt(int id, [FromBody]IEnumerable<int> orderItemIds)
        {
            try
            {
                RSReceipt receipt = new RSReceipt();
                RSOrder order = db.Orders.Find(id);
                receipt.Order = order;
                receipt.User = User.Identity as RSUser;

                var orderItems = orderItemIds.Select(oiid => db.OrderItems.Find(oiid));
                if (orderItems.All(oi => oi != null ? oi.State < 2 : false))
                    foreach (var orderItem in orderItems)
                    {
                        orderItem.Receipt = receipt;
                        orderItem.State = (int)RSOrderItemState.Paid;
                    }
                else return null;

                db.Receipts.Add(receipt);

                db.SaveChanges();
                return receipt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<RSOrderItem> AddOrderItem(int id, [FromBody]int productId)
        {
            RSOrder order = await db.Orders.FindAsync(id);
            
            if (!order.Active) //Disallow editing closed orders
                return null;

            RSProduct product = await db.Products.FindAsync(productId);
            if (order != null && product != null)
            {
                RSOrderItem oi = new RSOrderItem(product, order);
                db.OrderItems.Add(oi);
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
        [HttpPost]
        public async Task<RSOrder> CreateOrder([FromBody]string title)
        {
            RSOrder order = new RSOrder();
            order.Title = title;
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return order;
        }
    }
}