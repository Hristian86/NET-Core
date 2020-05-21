using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp.Models;
using RestSharp;

namespace ReactApp.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class SeedController : ControllerBase
    {

        private static List<Seed> seed = new List<Seed>();
        //<IEnumerable<Seed>>
        // GET: /Seed/Seeds
        
        [HttpGet]
        public List<Seed> Seeds()
        {
            if (seed.Count() == 0)
            {

                for (int i = 0; i < 8; i++)
                {
                    Seed seeds = new Seed
                    {
                        Id = i,
                        age = i + 10,
                        Name = "go6o",
                        LastName = "pesho"
                    };
                    seed.Add(seeds);
                }
            }

            return seed;
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Seed/5
        [HttpGet]
        public IActionResult Get(int id)
        {
            var searchedSeed = seed.Where(x => x.Id == id).FirstOrDefault();

            return Ok(searchedSeed);
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetCards()
        {
            var client = new RestClient("https://omgvamp-hearthstone-v1.p.rapidapi.com/cards");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "omgvamp-hearthstone-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "f2d5faffa5msh59f636881a5e715p1558e3jsn424dea256306");
            IRestResponse response = client.Execute(request);
            string x = response.Content;

            return Ok(x);
        }

        // POST: api/Seed
        [HttpPost]
        [Authorize]
        public Seed Post([FromBody]Seed model)
        {

            Seed searchedSeed = seed.Where(x => x.Id == model.Id).FirstOrDefault();

            return searchedSeed;
        }

        // PUT: api/Seed/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {



        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}