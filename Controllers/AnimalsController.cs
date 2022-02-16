using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zadanie4.Models;
using Zadanie4.Services;

namespace Zadanie4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnimalsController : ControllerBase
    {
        private IDatabaseService _database;
        public AnimalsController(IDatabaseService database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals(string orderCategory)
        {
            return Ok(await _database.GetAnimals(orderCategory));
        }

        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            _database.AddAnimal(animal);
            return Ok("Animal has been added");
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteAnimal(int index)
        {
            _database.DeleteAnimal(index);
            return Ok("Animal on " + index + " has been deleted");
        }

        [HttpPut("{index}")]
        public IActionResult UpdateAnimal(Animal animal, int index) {
            _database.UpdateAnimal(animal, index);
            return Ok("Animal on " + index + " has been updated");
        }
    }
}
