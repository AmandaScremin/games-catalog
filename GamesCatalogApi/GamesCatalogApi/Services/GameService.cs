using GamesCatalogApi.Entities;
using GamesCatalogApi.Exceptions;
using GamesCatalogApi.InputModel;
using GamesCatalogApi.Repositories;
using GamesCatalogApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GamesCatalogApi.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Obtain(int page, int quantity)
        {
            var games = await _gameRepository.Obtain(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Obtain(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if(game == null)
            {
                return null;
            }

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entityGame = await _gameRepository.Obtain(game.Name, game.Producer);

            if(entityGame.Count > 0)
            {
                throw new GameAlreadyRegisteredException();
            }

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price

            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = gameInsert.Name,
                Producer = gameInsert.Producer,
                Price = gameInsert.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.Obtain(id);

            if (entityGame == null)
            {
                throw new GameNotRegisteredException();
            }

            entityGame.Name = game.Name;
            entityGame.Producer = game.Producer;
            entityGame.Price = game.Price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if(game == null)
            {
                throw new GameNotRegisteredException();
            }

            await _gameRepository.Remove(id);

        }

        public async Task Update(Guid id, double price)
        {
            var entityGame = await _gameRepository.Obtain(id);

            if(entityGame == null)
            {
                throw new GameNotRegisteredException();
            }

            entityGame.Price = price;

            await _gameRepository.Update(entityGame);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
