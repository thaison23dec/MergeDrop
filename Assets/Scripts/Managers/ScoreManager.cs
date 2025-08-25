using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int currentScore = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
    }


    public void Save(ref ScoreSaveData data)
    {
        data.score = currentScore;
    }

    public void Load(ScoreSaveData data)
    {
        currentScore = data.score;
    }

    public void ClearData(ref ScoreSaveData data)
    {
        data.score = 0;
    }

}

[System.Serializable]

public struct ScoreSaveData
{
    public int score;
}