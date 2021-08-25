using Microsoft.AspNetCore.Mvc;
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
        public EntityController(IRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public IEnumerable<EntityDto> GetItems()
        {
            var entities =  _repository.GetAll();
            return entities.Select(x => x.ConvertToDto());
        }

        [HttpGet("{id}")]
        public ActionResult<EntityDto> GetItem (Guid id)
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

            return CreatedAtAction(nameof(GetItem), new { id = entity.Id }, entity.ConvertToDto());
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
        public ActionResult DeleteItem(Guid id)
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
