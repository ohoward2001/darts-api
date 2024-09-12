using DartsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DartsAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BracketController : Controller
{
    [HttpGet]
    public async Task<List<Bracket>> GetBrackets()
    {
        const string connectionUri = "mongodb+srv://admin_user:Password10@golfapp.kphj1nd.mongodb.net/?retryWrites=true&w=majority&appName=GolfApp";

        var settings = MongoClientSettings.FromConnectionString(connectionUri);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(settings);

        var brackets = new List<Bracket>();

        try {
            var database = client.GetDatabase("DartsApp");
            var collection = database.GetCollection<Bracket>("brackets");

            brackets = collection.Find(b => true).ToListAsync().Result;
            
        } catch (Exception ex) {
            Console.WriteLine(ex);
        }
        Response.Headers.Append("Access-Control-Allow-Origin", "*");

        return brackets;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBracket([FromBody] Bracket bracket)
    {
        const string connectionUri = "mongodb+srv://admin_user:Password10@golfapp.kphj1nd.mongodb.net/?retryWrites=true&w=majority&appName=GolfApp";

        var settings = MongoClientSettings.FromConnectionString(connectionUri);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(settings);

        try {
            var database = client.GetDatabase("DartsApp");
            var collection = database.GetCollection<Bracket>("brackets");
            
            await collection.InsertOneAsync(bracket);
        } catch (Exception ex) {
            Console.WriteLine(ex);
            return BadRequest();
        }
        Response.Headers.Append("Access-Control-Allow-Origin", "*");
        return Ok();
    }
}