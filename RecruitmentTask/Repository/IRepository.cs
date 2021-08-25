using RecruitmentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentTask.Repository
{
    public interface IRepository
    {
        List<Entity> GetAll();

        void CreateItem(Entity item);

        void UpdateItem(Entity item);

        void DeleteEntity(Guid id);
    }
}
