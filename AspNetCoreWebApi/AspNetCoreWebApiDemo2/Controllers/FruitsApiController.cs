using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiDemo2.Controllers
{
    //URL => api/FruitsApi
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsApiController : ControllerBase
    {
        //Creating List of fruits
        public List<string> fruits = new List<string>()
        {
            "Apple",
            "Banana",
            "Mango",
            "Grapes",
            "Orange"
        };

        //Creating method to return Fruits => HttpGet Attribute
        //GET: GetFruits() => api/FruitsApi

        [HttpGet]
        public List<string> GetFruits()
        {
            return fruits;
        }

        //creating method to return fruit data based on specific id
        //GET: GetFruitById(int id) api/FruitsApi/2
        //[HttpGet("{id}")]
        //public string GetFruitById(int id)
        //{
        //    return fruits.ElementAt(id);
        //}

        [HttpGet("{id}")]
        public IActionResult GetFruitById(int id)
        {
            try
            {
                // Check if the id is within the valid range
                if (id < 0 || id >= fruits.Count)
                {
                    return NotFound(new { Message = $"Fruit with ID {id} not found." });
                }

                // Return the fruit if the id is valid
                return Ok(fruits[id]);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
            }
        }

    }
}
