using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Product.Common.Repository.Tests
{
    public static class FakeDBInitialiser
    {
        public static async Task SeedTestData(FakeContext context)
        {
            if (context.TestEntities.Any())
            {
                return;
            }

            List<FakeEntity> entities = new List<FakeEntity>
            {
                new FakeEntity
                {
                    ID = 1,
                    Text = "Text for entity 1.",
                    IsActive = true
                },
                new FakeEntity
                {
                    ID = 2,
                    Text = "Text for entity 2.",
                    IsActive = true
                },
                new FakeEntity
                {
                    ID = 3,
                    Text = "Text for entity 3.",
                    IsActive = false
                },
                new FakeEntity
                {
                    ID = 4,
                    Text = "Text for entity 4.",
                    IsActive = true
                },
                new FakeEntity
                {
                    ID = 5,
                    Text = "Text for entity 5.",
                    IsActive = false
                },
                new FakeEntity
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
