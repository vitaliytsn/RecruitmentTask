using System;
using Xunit;
using Moq;
using RecruitmentTask.Repository;
using RecruitmentTask.Model;
using RecruitmentTask.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTask.DTOS;

namespace RecruitmentTaskTests
{
    public class EntityControllerTests
    {

        private readonly Mock<ILogger<EntityController>> _loggerMock = new Mock<ILogger<EntityController>>();
        private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();

        private Entity createTestEntity()
        {
            return new Entity()
            {
                Id = Guid.NewGuid(),
                Name = "TestName",
                Price = 1,
                CreateDate = DateTimeOffset.UtcNow
            };
        }

        [Fact]
        public void GetEntetyById_UnexistingEntity_NotFoundResponse()
        {
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Entity)null);
            var controller = new EntityController(_repositoryMock.Object, _loggerMock.Object);
            var response = controller.GetEntityById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void GetEntetyById_ExistingEntity_ReturnsEntity()
        {
            var entity = createTestEntity();

            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(entity);
            var controller = new EntityController(_repositoryMock.Object, _loggerMock.Object);
            var response = controller.GetEntityById(Guid.NewGuid());

            Assert.IsType<EntityDto>(response.Value);
            var dto = (response as ActionResult<EntityDto>).Value;
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Price, dto.Price);
            Assert.Equal(entity.CreateDate, dto.CreateDate);
        }

        [Fact]
        public void CreateEntity_WithEntityToCreate_EntityCreated()
        {
            var entityToCreate = new CreateEntityDto()
            {
                Name = "TestName",
                Price = 100
            };
            var controller = new EntityController(_repositoryMock.Object, _loggerMock.Object);
            var response = controller.CreateItem(entityToCreate);

            var createdDto = (response.Result as CreatedAtActionResult).Value as EntityDto;

            Assert.Equal(entityToCreate.Name, createdDto.Name);
            Assert.Equal(entityToCreate.Price, createdDto.Price);
        }

        [Fact]
        public void UpdateEntity_WithExistingEntity_EntityUpdated()
        {
            var entity = createTestEntity();
            var idToUpdate = entity.Id;
            var entityToUpdate = new UpadateEntityDto()
            {
                Name = "UpdatedName",
                Price = entity.Price+1
            };
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(entity);
            var controller = new EntityController(_repositoryMock.Object, _loggerMock.Object);
            var response = controller.UdateItem(idToUpdate, entityToUpdate);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void DeleteEntity_WithExistingEntity_EntityDeleted()
        {
            var entity = createTestEntity();
            var idToDelete = entity.Id;
   
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(entity);
            var controller = new EntityController(_repositoryMock.Object, _loggerMock.Object);
            var response = controller.DeleteEntity(idToDelete);

            Assert.IsType<NoContentResult>(response);
        }
    }
}
