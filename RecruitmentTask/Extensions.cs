using RecruitmentTask.DTOS;
using RecruitmentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentTask
{
    public static class Extensions
    {
        public static EntityDto ConvertToDto(this Entity item)
        {
            return new EntityDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreateDate = item.CreateDate
            };
        }
    }
}
