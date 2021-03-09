using JoshsJelliesAndJams.DAL.Repositories;
using JoshsJelliesAndJams.Library;
using JoshsJelliesAndJams.Library.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace JoshsJelliesAndJams.WEBUI.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("api/customer/add")]
        public IActionResult AddCustomer(CustomerModel appCustomer)
        {
            if (ModelState.IsValid)
            {
                return Ok(_customerRepository.AddCustomer(appCustomer));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("api/customer/{fname}/{lname}")]
        public IActionResult LookupCustomer(string fname, string lname)
        {
            if(ModelState.IsValid)
            {
                return Ok(_customerRepository.LookupCustomer(fname, lname));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("api/adddefaultstore")]
        public void AddDefaultStore(CustomerModel appCustomer, string storeId)
        {
            _customerRepository.UpdateDefaultStore(appCustomer, storeId);
        }
    }
}
