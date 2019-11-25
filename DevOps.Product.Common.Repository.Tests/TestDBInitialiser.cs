using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Product.Common.Repository.Tests
{
    public static class TestDBInitialiser
    {
        public static async Task SeedTestData(TestContext context)
        {
            if (context.TestEntities.Any())
            {
                return;
            }

            List<TestEntity> entities = new List<TestEntity>
            {
                new TestEntity
                {
                    ID = 1,
                    Text = "Text for entity 1.",
                    IsActive = true
                },
                new TestEntity
                {
                    ID = 2,
                    Text = "Text for entity 2.",
                    IsActive = true
                },
                new TestEntity
                {
                    ID = 3,
                    Text = "Text for entity 3.",
                    IsActive = false
                },
                new TestEntity
                {
                    ID = 4,
                    Text = "Text for entity 4.",
                    IsActive = true
                },
                new TestEntity
                {
                    ID = 5,
                    Text = "Text for entity 5.",
                    IsActive = false
                },
                new TestEntity
                {
                    ID = 6,
                    Text = "Text for entity 6.",
                    IsActive = true
                },
            };

            context.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
