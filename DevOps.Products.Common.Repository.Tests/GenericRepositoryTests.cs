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
        private IGenericRepository<FakeEntity, FakeDTO> _testRepository;
        private IMapper _mapper;
        private FakeContext _context;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddAutoMapper(typeof(GenericRepositoryTests));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<FakeEntity, FakeDTO>().ReverseMap();
            });

            _mapper = new Mapper(mapperConfiguration);
        }

        [SetUp]
        public async Task Setup()
        {
            DbContextOptions<FakeContext> options = new DbContextOptionsBuilder<FakeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new FakeContext(options);

            await FakeDBInitialiser.SeedTestData(_context);

            _testRepository = new GenericRepository<FakeContext, FakeEntity, FakeDTO>(_context, _mapper);
        }

        [Test]
        public async Task Get_GetsActiveEntitiesFromDatabaseAndConvertsToCollectionOfDTOs_Succeeds()
        {
            IEnumerable<FakeDTO> expectedDTOs = _mapper.Map<IEnumerable<FakeDTO>>(_context.Set<FakeEntity>().Where(entity => entity.IsActive));

            string expectedSerializedDTOs = JsonConvert.SerializeObject(expectedDTOs);

            IEnumerable<FakeDTO> actualDTOs = await _testRepository.Get();

            string actualSerializedDTOs = JsonConvert.SerializeObject(actualDTOs);

            Assert.AreEqual(expectedSerializedDTOs, actualSerializedDTOs);
        }

        [Test]
        public async Task Get_GetsActiveEntitiesFromDatabaseWithFilterAndConvertsToCollectionOfDTOs_Succeeds()
        {
            const string expectedString = "1";

            IEnumerable<FakeDTO> actualDTOs = await _testRepository.Get(entity => entity.Text.Contains(expectedString));

            foreach (FakeDTO actualDTO in actualDTOs)
            {
                Assert.IsTrue(actualDTO.Text.Contains(expectedString));
            }
        }

        [Test]
        public async Task GetByID_GetsEntityFromDatabaseAndConvertsToDTO_Succeeds()
        {
            const int entityToGetID = 1;

            FakeDTO expectedDTO = new FakeDTO() { ID = entityToGetID, Text = "Text for entity 1." };

            string expectedSerializedDTO = JsonConvert.SerializeObject(expectedDTO);
            
            FakeDTO actualDTO = await _testRepository.GetByID(entityToGetID);

            string actualSerializedDTO = JsonConvert.SerializeObject(actualDTO);

            Assert.AreEqual(expectedSerializedDTO, actualSerializedDTO);
        }

        [Test]
        public async Task GetByID_TryToGetNonExistentEntity_ReturnsNull()
        {
            const int entityToGetID = 7;

            FakeDTO actualDTO = await _testRepository.GetByID(entityToGetID);

            Assert.AreEqual(null, actualDTO);
        }

        [Test]
        public async Task Create_CreatedEntityInDatabase_Succeeds()
        {
            const int expectedID = 7;

            FakeDTO expectedDTO = new FakeDTO() {ID = expectedID, Text = $"Text for entity {expectedID}." };

            await _testRepository.Create(expectedDTO);

            FakeEntity expectedEntity = _mapper.Map<FakeEntity>(expectedDTO);
            expectedEntity.IsActive = true;

            string expectedSerializedEntity = JsonConvert.SerializeObject(expectedEntity);

            FakeEntity actualEntity = await _context.FindAsync<FakeEntity>(expectedID);

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

            FakeEntity entity = await _context.FindAsync<FakeEntity>(expectedID);

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

            FakeEntity expectedEntity = await _context.FindAsync<FakeEntity>(expectedID);

            Assert.AreEqual("Text for entity 1.", expectedEntity.Text);

            expectedEntity.Text = "This text has been updated";

            await _testRepository.Update(expectedID, _mapper.Map<FakeDTO>(expectedEntity));

            FakeEntity actualEntity = await _context.FindAsync<FakeEntity>(expectedID);

            Assert.AreEqual(expectedEntity.Text, actualEntity.Text);
        }

        [Test]
        public async Task Update_TryToUpdateNonExistentEntity_ThrowsException()
        {
            const int entityToUseID = 1;
            const int entityToUpdateID = 7;

            FakeDTO dto = _mapper.Map<FakeDTO>(await _context.FindAsync<FakeEntity>(entityToUseID));

            Assert.ThrowsAsync<ArgumentException>(async () => await _testRepository.Update(entityToUpdateID, dto));
        }

        [Test]
        public async Task Update_TryToUpdateDeletedEntity_ThrowsException()
        {
            const int entityToUpdateID = 3;

            FakeDTO dto = _mapper.Map<FakeDTO>(await _context.FindAsync<FakeEntity>(entityToUpdateID));

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
