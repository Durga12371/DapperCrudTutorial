using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController :ControllerBase
    {
        private readonly IConfiguration _config;

        public SuperHeroController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllSuperHeros()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<SuperHero> heros = await SelectAllHeroes(connection);
            return Ok(heros);
        }



        [HttpGet("heroId")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(int heroid)
        {
            var demo = "select * from SuperHeros where Id = @Id";
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var hero = await connection.QueryFirstAsync<SuperHero>(demo,
                new { Id = heroid });
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero hero)
        { 
            var demo = "insert into SuperHeros (Name,FirstName,LastName,Place) values(@Name,@FirstName,@LastName,@Place)";
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(demo, hero);
            return Ok(await SelectAllHeroes(connection));
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {   
            var demo = "update SuperHeros set Name=@Name,FirstName=@FirstName,LastName=@LastName,Place=@Place where Id=@Id";
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(demo, hero);
            return Ok(await SelectAllHeroes(connection));
        }
        [HttpDelete("{heroId}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int heroId)
        { 
            var demo = "delete from SuperHeros where Id=@Id";
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(demo, new { Id = heroId });
            return Ok(await SelectAllHeroes(connection));
        }
        private static async Task<IEnumerable<SuperHero>> SelectAllHeroes(SqlConnection connection)
        {
            var demo = "select * from SuperHeros";
            return await connection.QueryAsync<SuperHero>(demo);
        }
    }
}
