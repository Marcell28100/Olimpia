using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpiaAPI.Models;

namespace OlimpiaAPI.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayer)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = createPlayer.Name,
                Age = createPlayer.Age,
                Height = createPlayer.Height,
                Weight = createPlayer.Weight,
                CreatedTime = DateTime.Now
            };

            if(player != null)
            {
                using(var context = new OlimpiaContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }

            return BadRequest();
        }
    }
}
