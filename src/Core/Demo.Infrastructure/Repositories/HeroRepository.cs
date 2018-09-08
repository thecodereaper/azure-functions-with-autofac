using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Models.Heroes;
using Demo.Core.Repositories;
using Demo.Infrastructure.Framework;

namespace Demo.Infrastructure.Repositories
{
    internal sealed class HeroRepository : IHeroRepository
    {
        private readonly IRepository _repository;

        public HeroRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Hero>> FetchAll()
        {
            return _repository.Get<Hero>();
        }

        public Task<Hero> FindOneById(string id)
        {
            return _repository.Get<Hero>(id);
        }

        public Task<IEnumerable<Hero>> FindByName(string name)
        {
            return _repository.Get<Hero>(h => h.Name.ToLower().Contains(name));
        }

        public Task<Hero> Create(Hero hero)
        {
            return _repository.Create(hero);
        }

        public Task<Hero> Update(Hero hero)
        {
            return _repository.Update(hero);
        }

        public Task<bool> Delete(string id)
        {
            return _repository.Delete<Hero>(id);
        }

        public async Task<bool> IsDuplicate(Hero hero)
        {
            IEnumerable<Hero> countries = await _repository
                .Get<Hero>(h =>
                    h.Name == hero.Name &&
                    h.Id != hero.Id
                );

            return countries.Any();
        }
    }
}