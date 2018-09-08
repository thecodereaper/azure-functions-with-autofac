using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Core.Models.Heroes;
using Demo.Core.Models.Heroes.Commands;

namespace Demo.Core.Services
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetAll();

        Task<Hero> GetOne(string id);

        Task<IEnumerable<Hero>> SearchByName(string name);

        Task<Hero> Create(CreateHeroCommand command);

        Task ChangeName(string id, ChangeHeroNameCommand command);

        Task Delete(string id);
    }
}