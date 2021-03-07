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
        public CustomerModel AddCustomer(CustomerModel appCustomer)
        {
            return _customerRepository.AddCustomer(appCustomer);
        }

        [HttpGet("api/customer/{fname}/{lname}")]
        public CustomerModel LookupCustomer(string fname, string lname)
        {
            return _customerRepository.LookupCustomer(fname, lname);
        }

        [HttpPost("api/adddefaultstore")]
        public void AddDefaultStore(CustomerModel appCustomer, string storeId)
        {
            _customerRepository.UpdateDefaultStore(appCustomer, storeId);
        }
    }
}
