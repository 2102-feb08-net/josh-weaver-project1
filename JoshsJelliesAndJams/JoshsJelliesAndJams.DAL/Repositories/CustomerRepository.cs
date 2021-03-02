﻿using System;
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

        private DbContextOptions<JoshsJelliesAndJamsContext> optionsBuilder;



        public void DBConnection(StreamWriter logStream)
        {
            string connectionString = File.ReadAllText("C:/Revature/JJJDb.txt");

            optionsBuilder = new DbContextOptionsBuilder<JoshsJelliesAndJamsContext>()
                .UseSqlServer(connectionString)
                .LogTo(logStream.WriteLine, minimumLevel: LogLevel.Information)
                .Options;

        }

        public CustomerModel AddCustomer(CustomerModel appCustomer)
        {
            using (var logStream = new StreamWriter("jjjdb-log.txt", append: true) { AutoFlush = true })
            {
                DBConnection(logStream);
                using (var context = new JoshsJelliesAndJamsContext(optionsBuilder))
                {
                    IQueryable<Customer> dbCustomer = context.Customers
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

                    context.Add(newCustomer);
                    context.SaveChanges();

                    Customer customerProfile = context.Customers
                        .Where(x => x.FirstName.Equals(appCustomer.FirstName) && x.LastName.Equals(appCustomer.LastName))
                        .First();

                    CustomerModel customerProfileWithId = new CustomerModel
                    {
                        CustomerID = customerProfile.CustomerId,
                        FirstName = customerProfile.FirstName,
                        LastName = customerProfile.LastName,
                        StreetAddress1 = customerProfile.StreetAddress1,
                        StreetAddress2 = customerProfile.StreetAddress2,
                        City = customerProfile.City,
                        State = customerProfile.State,
                        Zipcode = customerProfile.Zipcode,
                        DefaultStore = (int)customerProfile.DefaultStoreId

                    };
                    return customerProfileWithId;
                }
            }
        }

        public void UpdateDefaultStore(CustomerModel appCustomer, string appStore)
        {
            using (var logStream = new StreamWriter("jjjdb-log.txt", append: true) { AutoFlush = true })
            {
                DBConnection(logStream);
                using (var context = new JoshsJelliesAndJamsContext(optionsBuilder))
                {
                    Customer dbCustomer = context.Customers
                        .Where(c => (c.FirstName == appCustomer.FirstName) && (c.LastName == appCustomer.LastName))
                        .First();

                    dbCustomer.DefaultStoreId = appCustomer.DefaultStore;

                    context.SaveChanges();
                }
            }
        }

        public CustomerModel LookupCustomer(string fname, string lname)
        {
            using (var logStream = new StreamWriter("jjjdb-log.txt", append: true) { AutoFlush = true })
            {
                DBConnection(logStream);
                using (var context = new JoshsJelliesAndJamsContext(optionsBuilder))
                {

                    Customer dbCustomer = context.Customers
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
    }
}
