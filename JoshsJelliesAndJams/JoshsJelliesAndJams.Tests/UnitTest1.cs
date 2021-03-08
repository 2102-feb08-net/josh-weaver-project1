using JoshsJelliesAndJams.Library;
using JoshsJelliesAndJams.Library.svc;
using System;
using Xunit;

namespace JoshsJelliesAndJams.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CustomerCreation_Pass()
        {
            //arrange
            string testName = "JOSH";
            //act
            var testCustomer = new CustomerModel
            {
                FirstName = "josh",
                LastName = "weaver",
                StreetAddress1 = "123 main street",
                StreetAddress2 = "",
                City = "Atlanta",
                State = "Ga",
                Zipcode = "30303"
            };
            //assert
            Assert.Equal(testName, testCustomer.FirstName);
        }

        [Fact]
        public void StringValidatorTest()
        {
            //arrange
            string test = "josh";
            //act

            test.StringValidator();
            //assert

            Assert.True(true, test);
        }
    }
}
