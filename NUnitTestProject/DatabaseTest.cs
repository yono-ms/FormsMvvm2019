using FormsMvvm2019;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    class DatabaseTest
    {
        AppDatabase database;

        [SetUp]
        public void Setup()
        {
            if (database == null)
            {
                database = new AppDatabase();
            }
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            await database.DeleteAllAsync();
        }

        [Test]
        public async Task DeleteAllTestAsync()
        {
            await database.DeleteAllAsync();
        }

        [Test]
        public async Task SaveItemsTestAsync()
        {
            var items = new List<PrefItem>
            {
                new PrefItem { Code="01", Name="Name1" },
                new PrefItem { Code="02", Name="Name2" },
                new PrefItem { Code="03", Name="Name3" },
            };

            await database.SaveItemsAsync(items);
            var results = await database.GetPrefItemsAsync();
            Assert.AreEqual(items.Count, results.Count);
        }

        [Test]
        public async Task GetPrefItemsTestAsync()
        {
            var items = await database.GetPrefItemsAsync();
            var count = items.Count;
            Assert.AreEqual(0, count);
        }
    }
}
