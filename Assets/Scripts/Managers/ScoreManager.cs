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

}
