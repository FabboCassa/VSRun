using UnityEngine;

[System.Serializable]
public class DataSerializer
{
    public int highScore;

    public DataSerializer(Data data)
    {
        this.highScore = data.GetHighScore();
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public Data GetData()
    {
        Data data = new Data();
        data.SetHighScore(highScore);
        return data;
    }
}
