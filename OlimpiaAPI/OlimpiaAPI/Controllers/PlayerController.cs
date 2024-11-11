using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            if (player != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }

            return BadRequest();
        }

        [HttpGet]
        public ActionResult<Player> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Players.ToList());

            }
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetById(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(playerTable => playerTable.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }

        [HttpPut("{id}")]
         
        public ActionResult<Player> Put(UpdatePlayerDto updatePlayer, Guid id)
        {
            using(var context = new OlimpiaContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(player => player.Id == id);
                if (existingPlayer != null)
                {
                    existingPlayer.Name = updatePlayer.Name;
                    existingPlayer.Age = updatePlayer.Age;
                    existingPlayer.Height = updatePlayer.Height;
                    existingPlayer.Weight = updatePlayer.Height;
                    context.Players.Add(existingPlayer);
                    context.SaveChanges();
                    return Ok(existingPlayer);
                }
                return NotFound();
            }

        }

        [HttpDelete]

        public ActionResult<Player> Delete(Guid Id)
        {
            using(var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(player => player.Id == Id);

                if (player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();
                    return Ok(new{ message = "Sikeres Törlés!"});
                }
                return NotFound();
            }

        }
        [HttpGet("playerdata/{id}")]

        public ActionResult<Player> Get(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var player = context.Players.Include(x => x.Data).FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }

    }
        
}
