using AutoMapper;
using Calabonga.UnitOfWork;
using DataCoreModule.Application.Interfaces.Data;
using DataCoreModule.Application.Services.Base;
using DataCoreModule.Core.Models.Entities.Base;
using Moq;
using System.Reflection;

namespace DataCore.Tests.Application
{
    public class TestEntity : Entity
    {

    }

    public class ServiceTests
    {
        private readonly Mock<IUnitOfWorkAdapter> _unitOfWorkMock;
        private readonly Mock<IRepository<TestEntity>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Service<TestEntity> _service;

        public ServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWorkAdapter>();
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<TestEntity>>();
            _unitOfWorkMock.Setup(x => x.GetRepository<TestEntity>()).Returns(_repositoryMock.Object);
            _service = new Service<TestEntity>(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_Should_InsertEntity_And_SaveChanges()
        {
            //arrange 
            var entity = CreateEntityWithId();

            //act
            var result = await _service.Create(entity);

            //assert
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<TestEntity>(), CancellationToken.None), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Delete_Should_RemoveEntity_And_SaveChanges()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal(id, result);
        }

        [Fact]
        public async Task Update_Should_UpdateEntity_And_SaveChanges()
        {
            // Arrange
            var entity = new TestEntity();
            SetId(entity, Guid.NewGuid());

            // Act
            var result = await _service.Update(entity);

            // Assert
            _repositoryMock.Verify(r => r.Update(entity), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task GetById_Should_ReturnEntity()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new TestEntity();
            SetId(entity, id);
            _repositoryMock.Setup(r => r.FindAsync(id)).ReturnsAsync(entity);

            // Act
            var result = await _service.GetById(id);

            // Assert
            _repositoryMock.Verify(r => r.FindAsync(id), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task GetAll_Should_ReturnAllEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                CreateEntityWithId(),
                CreateEntityWithId()
            };
            _repositoryMock.Setup(r => r.GetAllAsync(true)).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAll();

            // Assert
            _repositoryMock.Verify(r => r.GetAllAsync(true), Times.Once);
            Assert.Equal(entities, result);
        }

        [Fact]
        public async Task CreateRange_Should_InsertEntities_And_SaveChanges()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                CreateEntityWithId(),
                CreateEntityWithId()
            };

            // Act
            var result = await _service.CreateRange(entities);

            // Assert
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<List<TestEntity>>(), CancellationToken.None), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal(entities, result);
        }

        private static void SetId(Entity entity, Guid id)
        {
            var idProperty = typeof(Entity).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            idProperty?.SetValue(entity, id);
        }

        private static TestEntity CreateEntityWithId()
        {
            var entity = new TestEntity();
            SetId(entity, Guid.NewGuid());
            return entity;
        }
    }
}
