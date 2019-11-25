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
        public async Task GetByID_GetsEntityFromDatabaseAndConvertsToDTO_Succeeds([Values(1,2,3)] int id)
        {
            TestDTO expectedDTO = new TestDTO() { ID = id, Text = $"Text for entity {id}." };

            string expectedSerializedDTO = JsonConvert.SerializeObject(expectedDTO);
            
            TestDTO actualDTO = await _testRepository.GetByID(id);

            string actualSerializedDTO = JsonConvert.SerializeObject(actualDTO);

            Assert.AreEqual(expectedSerializedDTO, actualSerializedDTO);
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
        public async Task Delete_SetIsActiveOnEntityToFalse_Succeeds()
        {
            const int expectedID = 1;

            TestEntity entity = await _context.FindAsync<TestEntity>(expectedID);

            Assert.IsTrue(entity.IsActive);

            await _testRepository.Delete(expectedID);

            Assert.IsFalse(entity.IsActive);
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
        public async Task EntityExists_ChecksIfEntityExistsByID_ReturnsTrue([Values(1, 2, 3)] int id)
        {
            bool doesExist = await _testRepository.EntityExists(id);

            Assert.IsTrue(doesExist);
        }

        [Test]
        public async Task EntityExists_ChecksIfEntityExistsByID_ReturnsFalse([Values(-10, 0, 10)] int id)
        {
            bool doesExist = await _testRepository.EntityExists(id);

            Assert.IsFalse(doesExist);
        }
    }
}