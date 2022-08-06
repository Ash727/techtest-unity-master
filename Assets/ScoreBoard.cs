using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text EnemyScore;
    public Text PlayerScore;
    private  int PlayerScoreValue;
    private int EnemyScoreValue ;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void GetScore(Result score)
    {
        switch (score)
        {
            case Result.Won:
                PlayerScoreValue++;
                Debug.Log($"{score} Added Player");
                PlayerScore.text = $"Player: {PlayerScoreValue.ToString()}";
                break;
            case Result.Lost:
                EnemyScoreValue++;
                EnemyScore.text = $"Enemy: {EnemyScoreValue.ToString()}";
                break;
        }
    }
}
