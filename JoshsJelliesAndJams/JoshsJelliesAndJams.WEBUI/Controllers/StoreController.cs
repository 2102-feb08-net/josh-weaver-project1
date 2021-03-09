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
        public IActionResult StoreHistory(int storeID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_storeRepository.StoreHistory(storeID));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("api/store/inventory/{StoreID}")]
        public IActionResult CheckInventory(int storeID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_storeRepository.CheckInventory(storeID));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("api/store/list")]
        public IActionResult ListStores()
        {
            if (ModelState.IsValid)
            {
                return Ok(_storeRepository.ListStores());
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
