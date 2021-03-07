using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JoshsJelliesAndJams.Library;
using JoshsJelliesAndJams.Library.IRepositories;
using JoshsJelliesAndJams.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JoshsJelliesAndJams.DAL.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly JoshsJelliesAndJamsContext _context;

        public StoreRepository(JoshsJelliesAndJamsContext context)
        {
            _context = context;
        }
        public List<ProductModel> CheckInventory(int storeID)
        {
            List<Inventory> dbInventory = _context.Inventories
                .Include(x => x.Product)
                .Where(x => x.StoreId.Equals(storeID))
                .ToList();

            List<ProductModel> appInventory = new List<ProductModel>();


            foreach (var item in dbInventory)
            {
                ProductModel listItem = new ProductModel
                {
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Name = item.Product.Name,
                    CostPerItem = item.Product.Price
                };
                appInventory.Add(listItem);
            }
            return appInventory;
        }

        public List<ProductModel> CheckInventory(string storeName)
        {
            List<Inventory> dbInventory = _context.Inventories
                .Include(x => x.Product)
                .Where(x => x.StoreId.Equals(storeName))
                .ToList();

            List<ProductModel> appInventory = new List<ProductModel>();


            foreach (var item in dbInventory)
            {
                ProductModel listItem = new ProductModel
                {
                    ProductId = item.Product.ProductId,
                    Name = item.Product.Name,
                    CostPerItem = item.Product.Price
                };
                appInventory.Add(listItem);
            }

            return appInventory;
           
        }

        public List<OrderModel> StoreHistory(int storeId)
        {
            List<Order> dbOrder = _context.Orders
                .Where(x => x.StoreId.Equals(storeId))
                .ToList();

            List<OrderModel> appOrder = new List<OrderModel>();

            foreach (var item in dbOrder)
            {
                OrderModel lineItem = new OrderModel
                {
                    OrderNumber = item.OrderId,
                    OrderPlaced = (DateTime)item.DatePlaced,
                    Total = item.OrderTotal,
                    NumberOfProducts = item.NumberOfProducts
                };
                appOrder.Add(lineItem);
            }

            return appOrder;
        }

        public List<OrderModel> StoreHistory(string storeName)
        {
            List<Order> dbOrder = _context.Orders
                .Where(x => x.StoreId.Equals(storeName))
                .ToList();

            List<OrderModel> appOrder = new List<OrderModel>();

            foreach (var item in dbOrder)
            {
                OrderModel lineItem = new OrderModel
                {
                    OrderNumber = item.OrderId,
                    OrderPlaced = (DateTime)item.DatePlaced,
                    Total = item.OrderTotal
                };
                appOrder.Add(lineItem);
            }

            return appOrder;
        }
        public List<StoreModel> ListStores()
        {
            List<Store> dbStore = _context.Stores
                .ToList();

            List<StoreModel> appStoreList = new List<StoreModel>();

            foreach (var store in dbStore)
            {
                StoreModel lineItem = new StoreModel
                {
                    StoreID = store.StoreId,
                    StoreName = store.Name,
                    StoreCity = store.City,
                    StoreState = store.State
                };
                appStoreList.Add(lineItem);
            }
            return appStoreList;
        }
    }
}
