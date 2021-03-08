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

        [HttpGet("api/store/{storeID}")]
        public List<OrderModel> StoreHistory(int storeID)
        {
            return _storeRepository.StoreHistory(storeID);
        }

        [HttpGet("api/store/inventory/{StoreID}")]
        public List<ProductModel> CheckInventory(int storeID)
        {
            return _storeRepository.CheckInventory(storeID);
        }

        [HttpGet("api/store/list")]
        public List<StoreModel> ListStores()
        {
            return _storeRepository.ListStores();
        }
    }
}
