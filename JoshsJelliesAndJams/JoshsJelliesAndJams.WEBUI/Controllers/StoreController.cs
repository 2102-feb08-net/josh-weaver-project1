using JoshsJelliesAndJams.DAL.Repositories;
using JoshsJelliesAndJams.Library;
using JoshsJelliesAndJams.Library.IRepositories;
using JoshsJelliesAndJams.Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JoshsJelliesAndJams.WEBUI.Controllers
{
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        [HttpGet("api/storehistory/{id}")]
        public List<OrderModel> StoreHistory(int storeID)
        {
            return _storeRepository.StoreHistory(storeID);
        }
        [HttpGet("api/storehistory/{name}")]
        public List<OrderModel> StoreHistory(string storeName)
        {
            return _storeRepository.StoreHistory(storeName);
        }
        [HttpGet("api/inventory/{StoreID}")]
        public List<ProductModel> CheckInventory(int storeID)
        {
            return _storeRepository.CheckInventory(storeID);
        }
        //[HttpGet("api/inventory/{name}")]
        //public List<ProductModel> CheckInventory(string storeName)
        //{
        //    return _storeRepository.CheckInventory(storeName);
        //}
        [HttpGet("api/storelist")]
        public List<StoreModel> ListStores()
        {
            return _storeRepository.ListStores();
        }
    }
}
