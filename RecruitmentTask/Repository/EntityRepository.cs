using RecruitmentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentTask.Repository
{
    public class EntityRepository : IRepository
    {
        private List<Entity> Entities { get; set; }
        public EntityRepository()
        {
            Entities = new List<Entity>();
            //Define initial state of abstract repository for tests
            Entities.Add(new Entity()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                Name = "First",
                Price = 10
            });
            Entities.Add(new Entity()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Secound",
                Price = 20
            });
            Entities.Add(new Entity()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Third",
                Price = 30
            });
        }
        public void CreateEntity(Entity item)
        {
            Entities.Add(item);
        }

        public void DeleteEntity(Guid id)
        {
            var index = Entities.FindIndex(existingItem => existingItem.Id == id);
            Entities.RemoveAt(index);
        }

        public List<Entity> GetAll()
        {
            return Entities;
        }

        public void UpdateEntity(Entity item)
        {
            var index = Entities.FindIndex(existingItem => existingItem.Id == item.Id);
            Entities[index] = item;
        }

        public Entity GetById(Guid id)
        {
            return Entities.Where(x => x.Id == id).Single();
        }
    }
}
