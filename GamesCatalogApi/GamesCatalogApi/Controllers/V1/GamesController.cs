using GamesCatalogApi.Exceptions;
using GamesCatalogApi.InputModel;
using GamesCatalogApi.Services;
using GamesCatalogApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogApi.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obtain([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity=5)
        {
            var games = await _gameService.Obtain(page, quantity);

            if (games.Count() == 0)
            {
                return NoContent();
            }

            return Ok(games); 
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Obtain([FromRoute] Guid idGame)
        {
            var game = await _gameService.Obtain(idGame);

            if(game == null)
            {
                return NoContent();
            }
            return Ok(game);
        }

        [HttpPost]

        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch (GameAlreadyRegisteredException)
            {
                return UnprocessableEntity("There's already a game with this name for this producer");
            }
        }

        [HttpPut("{idGame:guid}")]

        public async Task<ActionResult>UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("This game doesn't exist");
            }
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]

        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);

                return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("This game doesn't exist");
            }
        }

        [HttpDelete("idGame:guid")]

        public async Task<ActionResult> DeleteGame(Guid idGame)
        {
            try
            {
                await _gameService.Remove(idGame);

                return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("This game doesn't exist");
            }
        }

    }
}
