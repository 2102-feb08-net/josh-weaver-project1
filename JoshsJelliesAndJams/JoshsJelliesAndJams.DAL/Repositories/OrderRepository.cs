using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JoshsJelliesAndJams.Library;
using JoshsJelliesAndJams.Library.IRepositories;
using JoshsJelliesAndJams.Library.svc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JoshsJelliesAndJams.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly JoshsJelliesAndJamsContext _context;

        public OrderRepository(JoshsJelliesAndJamsContext context)
        {
            _context = context;
        }

        public void AddOrder(OrderModel appOrder)
        {
            Order dbOrder = _context.Orders.OrderBy(x => x.OrderId).Last();

            DateTime dateTime = DateTime.Now;

            Order newOrder = new Order
            {
                CustomerId = appOrder.CustomerNumber,
                StoreId = appOrder.StoreID,
                NumberOfProducts = appOrder.Product.Sum(x => x.Quantity),
                OrderTotal = appOrder.Total,
                DatePlaced = dateTime
            };

            _context.Add(newOrder);
            _context.SaveChanges();

            Order dbOrderV2 = _context.Orders.OrderBy(x => x.OrderId).Last();
            OrderDetail dbOrderDetails = _context.OrderDetails.OrderBy(x => x.Id).Last();

            List<OrderDetail> orderDetailList = new List<OrderDetail>();

            foreach (var product in appOrder.Product)
            {
                OrderDetail newDetail = new OrderDetail
                {
                    OrderId = dbOrderV2.OrderId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    TotalCost = product.CostPerItem * product.Quantity
                };
                orderDetailList.Add(newDetail);
            }

            foreach (var lineItem in orderDetailList)
            {
                _context.Add(lineItem);
                _context.SaveChanges();
            }

            for (int prod = 0; prod < appOrder.Product.Count; prod++)
            {
                Inventory dbInventory = _context.Inventories
                    .Include(x => x.Product)
                    .Where(x => appOrder.StoreID == x.StoreId)
                    .Where(x =>  x.Product.ProductId == appOrder.Product[prod].ProductId )
                    .First();

                dbInventory.ProductId = appOrder.Product[prod].ProductId;
                dbInventory.Quantity -= appOrder.Product[prod].Quantity;

                _context.Update(dbInventory);
                _context.SaveChanges();
            }
        }

        public List<OrderModel> PullHistory(CustomerModel appCustomer)
        {
            List<Order> dbOrder = _context.Orders
                .Where(x => x.CustomerId.Equals(appCustomer.CustomerID))
                .ToList();

            List<OrderModel> appOrder = new List<OrderModel>();

            foreach (var item in dbOrder)
            {
                OrderModel lineItem = new OrderModel
                {
                    OrderNumber = item.OrderId,
                    OrderPlaced = (DateTime)item.DatePlaced,
                    NumberOfProducts = item.NumberOfProducts,
                    Total = item.OrderTotal
                };
                appOrder.Add(lineItem);
            }

            return appOrder;
        }

        public List<ProductModel> SeeDetails(int orderID)
        {
            List<OrderDetail> dbOrderDetails = _context.OrderDetails
                .Include(x => x.Product)
                .Where(x => x.OrderId.Equals(orderID))
                .ToList();

            List<ProductModel> results = new List<ProductModel>();


            foreach (var item in dbOrderDetails)
            {
                ProductModel itemResult = new ProductModel
                {
                    Name = item.Product.Name,
                    CostPerItem = item.Product.Price,
                    Quantity = item.Quantity,
                    TotalLine = item.TotalCost
                };
                results.Add(itemResult);
            }

            return results;
        }
    }
}
