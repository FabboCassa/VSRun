using UnityEngine;

public class Data
{

    
    private int highScore;

    public Data()
    {
        this.highScore = 0;
    }

    public int GetHighScore()
    {
        return highScore;
    }
    public void SetHighScore(int score)
    {
        highScore = score;
    }
    public void ResetHighScore()
    {
        highScore = 0;
    }
}
