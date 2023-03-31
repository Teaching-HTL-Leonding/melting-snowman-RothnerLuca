using MeltingSnowman.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new() {Title="Melting Snowman API", Version="v1"});
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var nextGameId = 0;
var gameDict = new ConcurrentDictionary<int, MeltingSnowmanGame>();
var numberOfGuessesDict = new ConcurrentDictionary<int, int>();

app.MapPost("/game", () => 
{
    var newId = Interlocked.Increment(ref nextGameId);

    if (!gameDict.TryAdd(newId, new MeltingSnowmanGame())) 
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

    if (!numberOfGuessesDict.TryAdd(newId, 0))
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

    return Results.Ok(new NewGame(newId));
})
    .WithName("New Game")
    .WithTags("Game")
    .Produces<NewGame>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status500InternalServerError)
    .WithOpenApi(o => 
    {
        o.Summary = "Creates a new game and returns game id";

        o.Responses[((int)StatusCodes.Status200OK).ToString()].Description = "Game ID";
        o.Responses[((int)StatusCodes.Status500InternalServerError).ToString()].Description = "Internal Server Error";

        return o;
    });

app.MapGet("/game/{gameId}", (int gameId) => 
{
    if (!gameDict.TryGetValue(gameId, out MeltingSnowmanGame? game))
    { 
        return Results.NotFound(); 
    }

    if (!numberOfGuessesDict.TryGetValue(gameId, out int numberOfGuesses))
    {
        return Results.NotFound();
    }

    return Results.Ok(new GetGame(game.Word, numberOfGuesses));
})
    .WithName("Get Guessing Status")
    .WithTags("Game")
    .Produces<GetGame>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithOpenApi(o =>
    {
        o.Summary = "Returns Guessing Status";

        o.Responses[((int)StatusCodes.Status200OK).ToString()].Description = "Guessing Status";
        o.Responses[((int)StatusCodes.Status404NotFound).ToString()].Description = "Game not found";

        return o;
    });

app.MapPost("/game/{gameId}", (int gameId, [FromBody]string letter) =>
{
    if (string.IsNullOrEmpty(letter) || letter.Length != 1)
    {
        return Results.BadRequest("Provide a letter");
    }
    
    if (!gameDict.TryGetValue(gameId, out MeltingSnowmanGame? game))
    { 
        return Results.NotFound(); 
    }

    if (!numberOfGuessesDict.TryGetValue(gameId, out int numberOfGuesses))
    {
        return Results.NotFound();
    }

    numberOfGuessesDict[gameId] = numberOfGuessesDict.GetValueOrDefault(gameId) + 1;

    return Results.Ok(new GuessRes(game.Guess(letter!), game.Word, numberOfGuessesDict[gameId]));
})
    .WithName("Make Guess")
    .WithTags("Game")
    .Produces<GuessRes>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status400BadRequest)
    .WithOpenApi(o => 
    {
        o.Summary = "Make a guess in a certain game";

        o.Responses[((int)StatusCodes.Status200OK).ToString()].Description = "Made guess";
        o.Responses[((int)StatusCodes.Status400BadRequest).ToString()].Description = "No valid letter provided";
        o.Responses[((int)StatusCodes.Status404NotFound).ToString()].Description = "Game not found";

        return o;
    });

app.Run();

record GuessRes(int Occurences, string WordToGuess, int NumberOfGuesses);
record GetGame(string WordToGuess, int NumberOfGuesses);
record NewGame(int GameId);
