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

        Entity GetById(Guid id);

        void CreateEntity(Entity item);

        void UpdateEntity(Entity item);

        void DeleteEntity(Guid id);
    }
}
