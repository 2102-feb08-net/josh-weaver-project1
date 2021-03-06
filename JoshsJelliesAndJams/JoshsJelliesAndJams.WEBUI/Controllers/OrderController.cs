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
        public void AddOrder(OrderModel appOrder)
        {
            _orderRepository.AddOrder(appOrder);
        }

        [HttpGet("api/order/history/{id}")]
        public List<OrderModel> OrderHistory(int id)
        {
            return _orderRepository.PullHistory(id);
        }

        [HttpGet("api/orderdetail")]
        public List<ProductModel> DetailHistory(int orderID)
        {
            return _orderRepository.SeeDetails(orderID);
        }
    }
}
