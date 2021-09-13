using GamesCatalogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogApi.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> Games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 21", Producer = "EA", Price = 200} },
            {Guid.Parse("0cb314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{Id = Guid.Parse("0cb314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 20", Producer = "EA", Price = 220} },
            {Guid.Parse("0cc314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{Id = Guid.Parse("0cc314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 19", Producer = "EA", Price = 230} },
            {Guid.Parse("0cd314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{Id = Guid.Parse("0cd314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 18", Producer = "EA", Price = 170} },
            {Guid.Parse("0ce314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{Id = Guid.Parse("0ce314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 17", Producer = "EA", Price = 210} }
        };
        public Task<List<Game>> Obtain(int pages, int quantity)
        {
            return Task.FromResult(Games.Values.Skip((pages - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> Obtain(Guid id)
        {
            if (!Games.ContainsKey(id))
            {
                return null;
            }

            return Task.FromResult(Games[id]);
        }

        public Task<List<Game>> Obtain(string name, string producer)
        {
            return Task.FromResult(Games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task Insert(Game game)
        {
            Games.Add(game.Id, game);
            return Task.CompletedTask;
        }        

        public Task Update(Game game)
        {
            Games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            Games.Remove(id);
            return Task.CompletedTask;
        }
        
        public void Dispose()
        {
            //fechar conexão com o banco
        }
    }
}
