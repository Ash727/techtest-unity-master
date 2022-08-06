using System;
using UnityEngine;

public static class LevelManager
{
    public static int GameToWinNextLevel { get; set; } = 1;
    public static int NumOfGamesWon { get; set; } = 0;
    public static void CheckForLevelUp(Result result, Player player)
    {
        {
            if(( Result.Won == result))
            {
                NumOfGamesWon++;
                Debug.Log($"Num of games {NumOfGamesWon}!!");

                if (NumOfGamesWon == GameToWinNextLevel)
                {
                    Debug.Log("LEVEL UP!!");
                    player.LvlUp();
                    GameToWinNextLevel *= 2;
                }
            }

        }
    }
}