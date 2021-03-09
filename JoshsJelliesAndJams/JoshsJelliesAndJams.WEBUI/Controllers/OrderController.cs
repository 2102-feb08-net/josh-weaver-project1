using JoshsJelliesAndJams.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using JoshsJelliesAndJams.Library;
using System.Collections.Generic;
using JoshsJelliesAndJams.Library.IRepositories;

namespace JoshsJelliesAndJams.WEBUI.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("api/order/add")]
        public IActionResult AddOrder(OrderModel appOrder)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.AddOrder(appOrder);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("api/order/history/{id}")]
        public IActionResult OrderHistory(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(_orderRepository.PullHistory(id));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("api/order/detail/{orderId}")]
        public IActionResult DetailHistory(int orderId)
        {
            if (ModelState.IsValid)
            {
                return Ok(_orderRepository.SeeDetails(orderId));
            }
            else
            {
                return BadRequest();
            }    
        }
    }
}
