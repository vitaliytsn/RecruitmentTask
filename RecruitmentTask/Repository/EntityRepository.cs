using RecruitmentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentTask.Repository
{
    public class EntityRepository : IRepository
    {
        private List<Entity> Items { get; set; }
        public EntityRepository()
        {
            Items = new List<Entity>();
            //Define initial state of abstract repository for tests
            Items.Add(new Entity()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                Name = "First",
                Price = 10
            });
            Items.Add(new Entity()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Secound",
                Price = 20
            });
            Items.Add(new Entity()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Third",
                Price = 30
            });
        }
        public void CreateItem(Entity item)
        {
            Items.Add(item);
        }

        public void DeleteEntity(Guid id)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == id);
            Items.RemoveAt(index);
        }

        public List<Entity> GetAll()
        {
            return Items;
        }

        public void UpdateItem(Entity item)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == item.Id);
            Items[index] = item;
        }
    }
}
