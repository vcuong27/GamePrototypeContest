using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Instance = this;
    }

    void AddScore(int score)
    {
        this.score += score;
    }

    int GetScore()
    {
        return score;
    }

    void ResetScoreTo(int score)
    {
        this.score = score;
    }

}
