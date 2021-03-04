using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JoshsJelliesAndJams.Library;
using JoshsJelliesAndJams.Library.svc;
using JoshsJelliesAndJams.Library.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JoshsJelliesAndJams.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly JoshsJelliesAndJamsContext _context;

        public CustomerRepository(JoshsJelliesAndJamsContext context)
        {
            _context = context;
        }

        public CustomerModel AddCustomer(CustomerModel appCustomer)
        {
            IQueryable<Customer> dbCustomer = _context.Customers
                .OrderBy(x => x.CustomerId);

            DateTime dateTime = DateTime.Now;

            var newCustomer = new Customer
            {
                FirstName = appCustomer.FirstName,
                LastName = appCustomer.LastName,
                StreetAddress1 = appCustomer.StreetAddress1,
                StreetAddress2 = appCustomer.StreetAddress2,
                City = appCustomer.City,
                State = appCustomer.State,
                Zipcode = appCustomer.Zipcode,
                CustomerCreated = dateTime,
                DefaultStoreId = appCustomer.DefaultStore
            };

            _context.Add(newCustomer);
            _context.SaveChanges();

            appCustomer.CustomerID = newCustomer.CustomerId;
            return appCustomer;
        }

        public void UpdateDefaultStore(CustomerModel appCustomer, string appStore)
        {
            Customer dbCustomer = _context.Customers
                .Where(c => (c.FirstName == appCustomer.FirstName) && (c.LastName == appCustomer.LastName))
                .First();

            dbCustomer.DefaultStoreId = appCustomer.DefaultStore;

            _context.SaveChanges();
        }

        public CustomerModel LookupCustomer(string fname, string lname)
        {
            Customer dbCustomer = _context.Customers
                .Include(c => c.DefaultStore)
                .Where(c => (c.FirstName == @fname) && (c.LastName == lname))
                .First();

            CustomerModel appCustomer = new CustomerModel
            {
                CustomerID = dbCustomer.CustomerId,
                FirstName = dbCustomer.FirstName,
                LastName = dbCustomer.LastName,
                StreetAddress1 = dbCustomer.StreetAddress1,
                StreetAddress2 = dbCustomer.StreetAddress2,
                City = dbCustomer.City,
                State = dbCustomer.State,
                Zipcode = dbCustomer.Zipcode,
                DefaultStore = (int)dbCustomer.DefaultStoreId
            };

            return appCustomer;
        }
    }
}
