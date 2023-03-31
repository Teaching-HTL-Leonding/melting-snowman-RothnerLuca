public class MeltingSnowmanGameTests  
{  
    [Fact]  
    public void DetectsInvalidGuess()  
    {  
        var game = new MeltingSnowmanGame();  
        Assert.Throws<ArgumentException>(() => game.Guess(""));  
        Assert.Throws<ArgumentException>(() => game.Guess("aa"));  
    }  
  
    [Fact]  
    public void ChoosesRandomWord()  
    {  
        var game = new MeltingSnowmanGame();  
        Assert.True(game.Word.Length > 0);  
    }  
  
    [Fact]  
    public void SetsWordDots()  
    {  
        var game = new MeltingSnowmanGame("demo");  
        Assert.Equal("....", game.Word);  
    }  
  
    [Fact]  
    public void AcceptsCorrectGuess()  
    {  
        var game = new MeltingSnowmanGame("demomo");  
        int numberOfGuessedLetters = game.Guess("d");  
        Assert.Equal("d.....", game.Word);  
        Assert.Equal(1, numberOfGuessedLetters);  
  
        numberOfGuessedLetters = game.Guess("m");  
        Assert.Equal("d.m.m.", game.Word);  
        Assert.Equal(2, numberOfGuessedLetters);  
  
        numberOfGuessedLetters = game.Guess("o");  
        Assert.Equal("d.momo", game.Word);  
        Assert.Equal(2, numberOfGuessedLetters);  
    }  
  
    [Fact]  
    public void RecognizesIncorrectGuess()  
    {  
        var game = new MeltingSnowmanGame("demo");  
        int numberOfGuessedLetters = game.Guess("x");  
        Assert.Equal("....", game.Word);  
        Assert.Equal(0, numberOfGuessedLetters);  
    }  
}  
