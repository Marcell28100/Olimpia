using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpiaAPI.Models;

namespace OlimpiaAPI.Controllers
{
    [Route("data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Data> Post(CreateDataDto createData)
        {
            var data = new Data
            {
                Id = Guid.NewGuid(),
                Country = createData.Country,
                County = createData.County,
                Description = createData.Description,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                PlayerId = createData.PlayerId
            };

            if (data != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Data.Add(data);
                    context.SaveChanges();
                    return StatusCode(201, data);
                }
            }

            return BadRequest();
        }
        [HttpGet]
        public ActionResult<Data> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Data.ToList());

            }
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetById(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(playerTable=> playerTable.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }
        public ActionResult<Data> Put(UpdateDataDto updateData, Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var existingData = context.Data.FirstOrDefault(data => data.Id == id);
                if (existingData != null)
                {
                    existingData.Country = updateData.Country;
                    existingData.County = updateData.County;
                    existingData.Description = updateData.Description;
                    existingData.UpdatedTime = DateTime.Now;

                    context.Data.Add(existingData);
                    context.SaveChanges();
                    return Ok(existingData);
                }
                return NotFound();
            }

        }
        [HttpDelete]

        public ActionResult<Data> Delete(Guid Id)
        {
            using (var context = new OlimpiaContext())
            {
                var data = context.Data.FirstOrDefault(data => data.Id == Id);

                if (data != null)
                {
                    context.Data.Remove(data);
                    context.SaveChanges();
                    return Ok(new { message = "Sikeres Törlés!" });
                }
                return NotFound();
            }

        }
    }
}
