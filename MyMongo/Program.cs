// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;
using MongoDB.Driver;

Console.WriteLine("Mongo DB Test");

var client = new MongoClient("mongodb://localhost:27017");

foreach (var database in client.ListDatabases().ToList())
{
    Console.WriteLine(database);
}

Console.WriteLine("\r\n");

var imdb = client.GetDatabase("IMDB");

foreach (var collection in imdb.ListCollections().ToList())
{
    Console.WriteLine(collection);
}

var principlesCollection = imdb.GetCollection<BsonDocument>("titleprincipals");

var principles = principlesCollection.Find("{category:\"self\"}").Limit(10).ToList();

foreach (var principle in principles)
{
    Console.WriteLine(principle);
}

var titles = imdb.GetCollection<BsonDocument>("TitlesBasic");

var searchResult = titles.Aggregate().Lookup("titleprincipals",
    "tconst", "tconst", "principles").Limit(10).ToList();

foreach (var title in searchResult)
{
    Console.WriteLine(title);
}