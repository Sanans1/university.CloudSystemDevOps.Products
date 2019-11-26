using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DevOps.Products.Common.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DevOps.Product.Common.Repository.Tests
{
    public class GenericRepositoryTests
    {
        private IGenericRepository<TestEntity, TestDTO> _testRepository;
        private IMapper _mapper;
        private TestContext _context;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddAutoMapper(typeof(GenericRepositoryTests));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<TestEntity, TestDTO>().ReverseMap();
            });

            _mapper = new Mapper(mapperConfiguration);
        }

        [SetUp]
        public async Task Setup()
        {
            DbContextOptions<TestContext> options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new TestContext(options);

            await TestDBInitialiser.SeedTestData(_context);

            _testRepository = new GenericRepository<TestContext, TestEntity, TestDTO>(_context, _mapper);
        }

        [Test]
        public async Task Get_GetsActiveEntitiesFromDatabaseAndConvertsToCollectionOfDTOs_Succeeds()
        {
            IEnumerable<TestDTO> expectedDTOs = _mapper.Map<IEnumerable<TestDTO>>(_context.Set<TestEntity>().Where(entity => entity.IsActive));

            string expectedSerializedDTOs = JsonConvert.SerializeObject(expectedDTOs);

            IEnumerable<TestDTO> actualDTOs = await _testRepository.Get();

            string actualSerializedDTOs = JsonConvert.SerializeObject(actualDTOs);

            Assert.AreEqual(expectedSerializedDTOs, actualSerializedDTOs);
        }

        [Test]
        public async Task Get_GetsActiveEntitiesFromDatabaseWithFilterAndConvertsToCollectionOfDTOs_Succeeds()
        {
            const string expectedString = "1";

            IEnumerable<TestDTO> actualDTOs = await _testRepository.Get(entity => entity.Text.Contains(expectedString));

            foreach (TestDTO actualDTO in actualDTOs)
            {
                Assert.IsTrue(actualDTO.Text.Contains(expectedString));
            }
        }

        [Test]
        public async Task GetByID_GetsEntityFromDatabaseAndConvertsToDTO_Succeeds()
        {
            const int entityToGetID = 1;

            TestDTO expectedDTO = new TestDTO() { ID = entityToGetID, Text = "Text for entity 1." };

            string expectedSerializedDTO = JsonConvert.SerializeObject(expectedDTO);
            
            TestDTO actualDTO = await _testRepository.GetByID(entityToGetID);

            string actualSerializedDTO = JsonConvert.SerializeObject(actualDTO);

            Assert.AreEqual(expectedSerializedDTO, actualSerializedDTO);
        }

        [Test]
        public async Task GetByID_TryToGetNonExistentEntity_ReturnsNull()
        {
            const int entityToGetID = 7;

            TestDTO actualDTO = await _testRepository.GetByID(entityToGetID);

            Assert.AreEqual(null, actualDTO);
        }

        [Test]
        public async Task Create_CreatedEntityInDatabase_Succeeds()
        {
            const int expectedID = 7;

            TestDTO expectedDTO = new TestDTO() {ID = expectedID, Text = $"Text for entity {expectedID}." };

            await _testRepository.Create(expectedDTO);

            TestEntity expectedEntity = _mapper.Map<TestEntity>(expectedDTO);
            expectedEntity.IsActive = true;

            string expectedSerializedEntity = JsonConvert.SerializeObject(expectedEntity);

            TestEntity actualEntity = await _context.FindAsync<TestEntity>(expectedID);

            string actualSerializedEntity = JsonConvert.SerializeObject(actualEntity);

            Assert.AreEqual(expectedSerializedEntity, actualSerializedEntity);
        }

        [Test]
        public void Create_TryToCreateEntityWithNull_ThrowsException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _testRepository.Create(null));
        }

        [Test]
        public async Task Delete_SetIsActiveOnEntityToFalse_Succeeds()
        {
            const int expectedID = 1;

            TestEntity entity = await _context.FindAsync<TestEntity>(expectedID);

            Assert.IsTrue(entity.IsActive);

            await _testRepository.Delete(expectedID);

            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Delete_TryToDeleteNonExistentEntity_ThrowsException()
        {
            const int expectedID = 7;

            Assert.ThrowsAsync<ArgumentException>(async () => await _testRepository.Delete(expectedID));
        }

        [Test]
        public async Task Update_UpdatesTextOfEntity_Succeeds()
        {
            const int expectedID = 1;

            TestEntity expectedEntity = await _context.FindAsync<TestEntity>(expectedID);

            Assert.AreEqual("Text for entity 1.", expectedEntity.Text);

            expectedEntity.Text = "This text has been updated";

            await _testRepository.Update(expectedID, _mapper.Map<TestDTO>(expectedEntity));

            TestEntity actualEntity = await _context.FindAsync<TestEntity>(expectedID);

            Assert.AreEqual(expectedEntity.Text, actualEntity.Text);
        }

        [Test]
        public async Task Update_TryToUpdateNonExistentEntity_ThrowsException()
        {
            const int entityToUseID = 1;
            const int entityToUpdateID = 7;

            TestDTO dto = _mapper.Map<TestDTO>(await _context.FindAsync<TestEntity>(entityToUseID));

            Assert.ThrowsAsync<ArgumentException>(async () => await _testRepository.Update(entityToUpdateID, dto));
        }

        [Test]
        public async Task Update_TryToUpdateDeletedEntity_ThrowsException()
        {
            const int entityToUpdateID = 3;

            TestDTO dto = _mapper.Map<TestDTO>(await _context.FindAsync<TestEntity>(entityToUpdateID));

            Assert.ThrowsAsync<DbUpdateException>(async () => await _testRepository.Update(entityToUpdateID, dto));
        }

        [Test]
        public async Task EntityExists_ChecksIfEntityExistsByID_ReturnsTrue()
        {
            const int entityToCheckID = 1;

            bool doesExist = await _testRepository.EntityExists(entityToCheckID);

            Assert.IsTrue(doesExist);
        }

        [Test]
        public async Task EntityExists_ChecksIfEntityExistsByID_ReturnsFalse()
        {
            const int entityToCheckID = 7;

            bool doesExist = await _testRepository.EntityExists(entityToCheckID);

            Assert.IsFalse(doesExist);
        }
    }
}