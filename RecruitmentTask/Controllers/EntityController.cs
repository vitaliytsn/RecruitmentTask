using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentTask.DTOS;
using RecruitmentTask.Model;
using RecruitmentTask.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentTask.Controllers
{
    [ApiController]
    [Route("Entities")]
    public class EntityController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<EntityController> _logger;
        public EntityController(IRepository repository,ILogger<EntityController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public IEnumerable<EntityDto> GetEntities()
        {
            var entities =  _repository.GetAll();
            _logger.LogInformation($"{DateTime.UtcNow.ToString()}: Found {entities.Count()} entities");
            return entities.Select(x => x.ConvertToDto());
        }

        [HttpGet("{id}")]
        public ActionResult<EntityDto> GetEntityById (Guid id)
        {

            var entity = _repository.GetById(id);

            if (entity is null)
            {
                return NotFound();
            }
            return entity.ConvertToDto();
        }

        [HttpPost]
        public ActionResult<EntityDto> CreateItem (CreateEntityDto entityDto)
        {
            Entity entity = new Entity()
            {
                Id = Guid.NewGuid(),
                Name = entityDto.Name,
                Price = entityDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };
            _repository.CreateEntity (entity);

            return CreatedAtAction(nameof(GetEntityById), new { id = entity.Id }, entity.ConvertToDto());
        }

        [HttpPut("{id}")]
        public  ActionResult UdateItem(Guid id, UpadateEntityDto entityDto)
        {
            var existingItem = _repository.GetById(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            Entity updatedItem = new Entity()
            {
                Id = existingItem.Id,
                Name = entityDto.Name,
                Price = entityDto.Price,
                CreateDate = existingItem.CreateDate
            };

            _repository.UpdateEntity (updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEntity(Guid id)
        {
            var existingItem = _repository.GetById(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            _repository.DeleteEntity(id);
            return NoContent();
        }

    }
}
