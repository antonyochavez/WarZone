using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    int score = 0;

    private void Awake()
    {
        scoreSingleton();
    }

    private void scoreSingleton()
    {
        int numberOfScoreSessions = FindObjectsOfType<ScoreTracker>().Length;
        if (numberOfScoreSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int killScore)
    {
        score += killScore;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }

}
