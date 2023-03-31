# Melting Snowman

## Introduction

You are working in a team that is supposed to program the game [*Melting Snowman*](https://www.hanginghyena.com/snowman) (aka [*Hangman*](https://en.wikipedia.org/wiki/Hangman_(game))).

Your team has already programmed a `MeltingSnowmanGame` class that implements the game logic. You can find the code for the class [here](logic/MeltingSnowmanGame.cs) and copy it into your project. Code examples on how to use the class can be found in the [Unit Tests](logic.tests/MeltingSnowmanGameTests.cs).

Your task is to implement a RESTful Web API that allows you to play the game.

## Requirements

Please consider the following requirements when solving the task.

### Mandatory tasks

Mandatory tasks that must be solved correctly to receive points for the example:

* Implementation of the REST operation `GET /game`. It returns the current guessing status. The result includes the number of previous guesses and the current state of the word. Here is an example of a correct request and response:

```txt
GET http://localhost:8080/game  
Accept: application/json  
```  
  
```txt  
{  
  "wordToGuess": ".....t..t",  
  "numberOfGuesses": 1  
}  
```  
  
* Implementation of the REST operation `POST /game`. It receives the guessed letter as a JSON string in the request body (e.g. `"t"`). The result includes the number of occurrences of the guessed word, the number of previous guesses, and the current state of the word. Here is an example of a correct request and response:  
  
```txt  
POST http://localhost:8080/game  
Accept: application/json  
Content-Type: application/json  
  
"t"  
```  
  
```txt  
{  
  "occurences": 2,  
  "wordToGuess": ".....t..t",  
  "numberOfGuesses": 1  
}  
```  
  
* Add at least basic Open API Specification metadata to the project.  
  
### Optional tasks  
  
Optional tasks to receive the full score for the example:  
  
* Your REST API supports multiple games simultaneously:  
* A new game can be started with `POST /game`. This Web API method returns a `gameId` (can be generated arbitrarily).  
* The current guessing status can be queried with `GET /game/{gameId}`  
* You can guess with `POST /game/{gameId}`
