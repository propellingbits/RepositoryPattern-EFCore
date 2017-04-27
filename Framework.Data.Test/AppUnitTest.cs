using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Data;
using Framework.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
//using Framework.Data.Test.DomainModel;
using Framework.Data.Test.DataModel;
using System.Linq;

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
        public void CanRetrieveADealer()
        {
            using (var context = new DatabaseContext(_options))
            {
                var repository = new EfRepository<Dealer>(context);
                var dealers = repository.Find(d => d.Name == "Dealer 1");                
                Assert.AreEqual(1, dealers.Count());
                repository.Dispose();
            }
        }

        [TestMethod]
        public void CanRetrieveTotalNumberOfDealers()
        {
            using (var context = new DatabaseContext(_options))
            {
                var repository = new EfRepository<Dealer>(context);
                var dealerTask = repository.GetCountAsync();
                dealerTask.Wait();
                Assert.AreEqual(2, dealerTask.Result);
                repository.Dispose();
            }
        }

        private void TestSetup()
        {
            using (var context = new DatabaseContext(_options))
            {
                var repository = new EfRepository<Dealer>(context);

                if (repository.Find(d => d.Id > 0).Count() == 0)
                {
                    var dealer = new Dealer
                    {
                        Id = 1,
                        Name = "Dealer 1",
                        Code = "001"

                    };
                    repository.Insert(dealer);

                    var dealer2 = new Dealer
                    {
                        Id = 2,
                        Name = "Dealer 2",
                        Code = "002"

                    };
                    repository.Insert(dealer2);
                    repository.Dispose();
                }
            }
        }
    }
}
