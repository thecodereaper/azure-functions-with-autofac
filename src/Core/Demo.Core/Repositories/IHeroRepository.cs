using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Core.Models.Heroes;

namespace Demo.Core.Repositories
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Hero>> FetchAll();

        Task<Hero> FindOneById(string id);

        Task<IEnumerable<Hero>> FindByName(string name);

        Task<Hero> Create(Hero hero);

        Task<Hero> Update(Hero hero);

        Task<bool> Delete(string id);

        Task<bool> IsDuplicate(Hero hero);
    }
}