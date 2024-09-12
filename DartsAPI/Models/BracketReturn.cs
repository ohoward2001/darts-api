using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DartsAPI.Models;

[BsonIgnoreExtraElements]
public class BracketReturn
{
    public ObjectId _id { get; set; }
    public string name { get; set; }
    public List<string> players { get; set; }
    public List<string>? scores { get; set; }
}