# Melting Snowman

## Einleitung

Sie arbeiten in einem Team, das das Spiel [*Melting Snowman*](https://www.hanginghyena.com/snowman) (aka [*Hangman*](https://en.wikipedia.org/wiki/Hangman_(game))) programmieren soll.

Ihr Team hat bereits eine Klasse `MeltingSnowmanGame` programmiert, die die Spiellogik implementiert. Sie finden den Code der Klasse [hier](logic/MeltingSnowmanGame.cs) und können ihn in ihr Projekt kopieren. Codebeispiele, wie man die Klasse verwendet, finden Sie in den [Unit Tests](logic.tests/MeltingSnowmanGameTests.cs).

Ihre Aufgabe ist die Implementierung einer RESTful Web API, mit der man das Spiel spielen kann.

## Anforderungen

Beachten Sie beim Lösen der Aufgabe folgende Anforderungen.

### Pflichtaufgaben

Pflichtaufgaben, die alle korrekt gelöst werden müssen, um Punkte für das Beispiel zu erhalten:

* Implementation der REST-Operation `GET /game`. Sie gibt den aktuellen Ratestand zurück. Das Ergebnis enthält die Anzahl der bisherigen Rateversuche und den aktuellen Stand des Wortes. Hier ein Beispiel für einen korrekten Request und Response:

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

* Implementation der REST-Operation `POST /game`. Sie bekommt als Request Body den geratenen Buchstaben als JSON String (z.B. `"t"`). Das Ergebnis enthält die Anzahl der Vorkommen des geratenen Wortes, die Anzahl der bisherigen Rateversuche und den aktuellen Stand des Wortes. Hier ein Beispiel für einen korrekten Request und Response:

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

* Fügen Sie zumindest grundlegende Open API Specification-Metadaten zum Projekt hinzu.

### Optionale Aufgaben

Optionale Aufgaben, um die volle Punktzahl für das Beispiel zu erhalten:

* Ihre REST-API unterstützt mehrere Spiele gleichzeitig:
* Mit `POST /game` kann ein neues Spiel gestartet werden. Diese Web API methode gibt eine `gameId` zurück (kann beliebig generiert werden).
* Der aktuelle Ratestand kann mit `GET /game/{gameId}` abgefragt werden
* Raten kann man mit `POST /game/{gameId}`
