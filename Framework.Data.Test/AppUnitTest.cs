using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Data;
using Framework.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
//using Framework.Data.Test.DomainModel;
using Framework.Data.Test.DataModel;

namespace Framework.Data.Test
{
    [TestClass]
    public class AppUnitTest
    {
        private DbContextOptions<DatabaseContext> _options;

        public AppUnitTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase().Options;
            TestSetup();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
                
        private void TestSetup()
        {
            using (var context = new DatabaseContext(_options))
            {
                var repository = new EfRepository<Dealer>(context);
                var dealer = new Dealer
                {
                    Id = 1,
                    Name = "Dealer 1",
                    Code = "001"

                };
                repository.Insert(dealer);
                /*context.Add<Dealer>(
                    new Dealer
                    {
                        Id = 1,
                        Name = "Dealer 1",
                        Code = "001"

                    });

                context.SaveChanges();*/
            }
        }
    }
}
